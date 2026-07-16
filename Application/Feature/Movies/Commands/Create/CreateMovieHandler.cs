using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class CreateMovieHandler : IRequestHandler<CreateMovieCommand, Movie> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public CreateMovieHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Movie> Handle(CreateMovieCommand command, CancellationToken cancellationToken) {
        var isExisted = await _context.MovieTable.AnyAsync(m => m.Title == command.CreateMovieDto.Title, cancellationToken);

        if (isExisted)
            throw new InvalidOperationException("Movie already existed");
            
        var genreRisuto = await _context.GenreTable
            .Where(g => command.CreateMovieDto.GenreIdRisuto.Contains(g.Id))
            .ToListAsync(cancellationToken);

        if (!genreRisuto.Any())
            throw new NotFoundException("Genre not found");

        var newMovie = _mapper.Map<Movie>(command.CreateMovieDto);
        newMovie.GenreRisuto = genreRisuto;

        _context.MovieTable.Add(newMovie);
        await _context.SaveChangesAsync(cancellationToken);

        return newMovie;
    }
} 