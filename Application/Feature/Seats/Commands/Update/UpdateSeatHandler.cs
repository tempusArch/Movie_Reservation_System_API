using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class UpdateSeatHandler : IRequestHandler<UpdateSeatCommand, Seat> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public UpdateSeatHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Seat> Handle(UpdateSeatCommand command, CancellationToken cancellationToken) {
        var theOne = await _context.SeatTable
            .FirstOrDefaultAsync(s => s.Id == command.SeatId);

        if (theOne == null)
            throw new NotFoundException("Seat not found");
        
        var theHall = await _context.HallTable
            .FirstOrDefaultAsync(x => x.Id == command.UpdateSeatDto.HallId, cancellationToken);

        if (theHall == null)
            throw new NotFoundException("Hall not found");
        
        var isExisted = await _context.SeatTable
            .AnyAsync(s => s.Row == command.UpdateSeatDto.Row 
                && s.LineNumber == command.UpdateSeatDto.LineNumber 
                && s.HallId == command.UpdateSeatDto.HallId, cancellationToken
            );

        if (isExisted)
            throw new InvalidOperationException("Seat already existed");

        _mapper.Map(command.UpdateSeatDto, theOne);
        
        await _context.SaveChangesAsync(cancellationToken);

        return theOne;
    }
}