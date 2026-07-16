using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class UpdateMovieHandler : IRequestHandler<UpdateMovieCommand, Movie> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public UpdateMovieHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Movie> Handle(UpdateMovieCommand command, CancellationToken cancellationToken) {
        var theOne = await _context.MovieTable
            .Include(m => m.GenreRisuto)
            .FirstOrDefaultAsync(m => m.Id == command.MovieId, cancellationToken);

        if (theOne == null)
            throw new NotFoundException("Movie not found");

        var genreRisuto = await _context.GenreTable
            .Where(g => command.UpdateMovieDto.GenreIdRisuto.Contains(g.Id))
            .ToListAsync(cancellationToken);

        if (!genreRisuto.Any())
            throw new NotFoundException("Genre not found");

        theOne.GenreRisuto.Clear();
        _mapper.Map(command.UpdateMovieDto, theOne);
        theOne.GenreRisuto = genreRisuto;
            
        await _context.SaveChangesAsync(cancellationToken);

        return theOne;
    }
}