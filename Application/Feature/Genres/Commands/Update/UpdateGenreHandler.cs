using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class UpdateGenreHandler : IRequestHandler<UpdateGenreCommand, Genre> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public UpdateGenreHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Genre> Handle(UpdateGenreCommand command, CancellationToken cancellationToken) {
        var isExisted = await _context.GenreTable.AnyAsync(g => g.Name == command.GenreName, cancellationToken);

        if (isExisted)
            throw new InvalidOperationException("Genre already existed");

        var theOne = await _context.GenreTable
            .FirstOrDefaultAsync(g => g.Id == command.GenreId);

        if (theOne == null)
            throw new NotFoundException("Genre not found");

        theOne.Name = command.GenreName;
        await _context.SaveChangesAsync(cancellationToken);

        return theOne;
    }
}
