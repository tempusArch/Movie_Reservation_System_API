using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class DeleteMovieHandler : IRequestHandler<DeleteMovieCommand, Unit> {
    private readonly MovieReservationSystemApiDbContext _context;
    public DeleteMovieHandler(MovieReservationSystemApiDbContext context) {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteMovieCommand command, CancellationToken cancellationToken) {
        var theOne = await _context.MovieTable
            .FirstOrDefaultAsync(m => m.Id == command.Id, cancellationToken);

        if (theOne == null)
            throw new NotFoundException("Movie not found");

        _context.MovieTable.Remove(theOne);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}