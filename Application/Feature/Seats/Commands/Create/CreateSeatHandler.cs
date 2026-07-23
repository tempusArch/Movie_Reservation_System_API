using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class CreateSeatHandler : IRequestHandler<CreateSeatCommand, Seat> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public CreateSeatHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Seat> Handle(CreateSeatCommand command, CancellationToken cancellationToken) {
        var theHall = await _context.HallTable
            .AnyAsync(x => x.Id == command.CreateSeatDto.HallId, cancellationToken);

        if (!theHall)
            throw new NotFoundException("Hall not found");
        
        var isExisted = await _context.SeatTable
            .AnyAsync(s => s.Row == command.CreateSeatDto.Row 
                && s.LineNumber == command.CreateSeatDto.LineNumber 
                && s.HallId == command.CreateSeatDto.HallId, cancellationToken
            );

        if (isExisted)
            throw new InvalidOperationException("Seat already existed");

        var newOne = _mapper.Map<Seat>(command.CreateSeatDto);

        _context.SeatTable.Add(newOne);
        await _context.SaveChangesAsync(cancellationToken);

        return newOne;
    }
}