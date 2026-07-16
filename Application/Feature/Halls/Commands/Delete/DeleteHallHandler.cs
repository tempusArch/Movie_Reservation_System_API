using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class DeleteHallHandler : IRequestHandler<DeleteHallCommand, Unit> {
    private readonly MovieReservationSystemApiDbContext _context;
    public DeleteHallHandler(MovieReservationSystemApiDbContext context) {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteHallCommand command, CancellationToken cancellationToken) {
        var theOne = await _context.HallTable
            .FirstOrDefaultAsync(h => h.Id == command.Id, cancellationToken);

        if (theOne == null)
            throw new NotFoundException("Hall not found");

        _context.HallTable.Remove(theOne);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}