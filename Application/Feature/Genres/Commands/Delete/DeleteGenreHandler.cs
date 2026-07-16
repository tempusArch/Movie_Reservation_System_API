using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class DeleteGenreHandler : IRequestHandler<DeleteGenreCommand, Unit> {
    private readonly MovieReservationSystemApiDbContext _context;
    public DeleteGenreHandler(MovieReservationSystemApiDbContext context) {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteGenreCommand command, CancellationToken cancellationToken) {
        var theOne = await _context.GenreTable
            .FirstOrDefaultAsync(g => g.Id == command.Id, cancellationToken);

        if (theOne == null)
            throw new NotFoundException("Genre not found");

        _context.GenreTable.Remove(theOne);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}