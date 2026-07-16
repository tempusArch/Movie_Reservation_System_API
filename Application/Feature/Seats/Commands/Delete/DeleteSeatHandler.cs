using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class DeleteSeatHandler : IRequestHandler<DeleteSeatCommand, Unit> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public DeleteSeatHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteSeatCommand command, CancellationToken cancellationToken) {
        var theOne = await _context.SeatTable
            .FirstOrDefaultAsync(s => s.Id == command.Id, cancellationToken);

        if (theOne == null)
            throw new NotFoundException("Seat not found");
        
        _context.SeatTable.Remove(theOne);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}