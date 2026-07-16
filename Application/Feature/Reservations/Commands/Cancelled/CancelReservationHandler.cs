using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class CancelReservationHandler : IRequestHandler<CancelReservationCommand, Unit> {
    private readonly MovieReservationSystemApiDbContext _context;
    public CancelReservationHandler(MovieReservationSystemApiDbContext context) {
        _context = context;
    }

    public async Task<Unit> Handle(CancelReservationCommand command, CancellationToken cancellationToken) {
        var theOne = await _context.ReservationTable
            .Include(r => r.Showtime)
            .FirstOrDefaultAsync(r =>
                r.Id == command.ReservationId &&
                r.UserId == command.UserId,
                cancellationToken
            );

        if (theOne == null)
            throw new NotFoundException("Reservation not found");
        
        if (theOne.Showtime.StartTime <= DateTime.UtcNow)
            throw new InvalidOperationException("Can not cancel due to movie already started");

        theOne.ReservationStatus = ReservationStatus.Cancelled;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}