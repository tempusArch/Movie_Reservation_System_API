using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetManyMoviesHandler : IRequestHandler<GetManyMoviesQuery, MovieListResponse> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetManyMoviesHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MovieListResponse> Handle(GetManyMoviesQuery query, CancellationToken cancellationToken) {
        IQueryable<Movie> source = _context.MovieTable.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.MovieName)) {
            var nameFiltered = source.Where(m => m.Title.Contains(query.MovieName));
            var descriptionFiltered = source.Where(m => m.Description.Contains(query.MovieName));

            source = nameFiltered.Union(descriptionFiltered);
        }

        if (!string.IsNullOrWhiteSpace(query.GenreName))
            source = source.Where(m => m.GenreRisuto.Any(g => g.Name.Contains(query.GenreName)));

        var limit = Math.Min(query.Limit, 100);

        var result = await source
            .Select(x => new ReadMovieDto {
                MovieId = x.Id,
                Title = x.Title,
                Description = x.Description,
                Duration = x.Duration,

                GenreRisuto = x.GenreRisuto
                    .Select(z => new ReadGenreDto {
                        GenreId = z.Id,
                        GenreName = z.Name
                    })
                    .ToList(),

                ShowtimeRisuto = x.ShowtimeRisuto
                    .Select(s => new ReadShowtimeDto {
                        ShowtimeId = s.Id,
                        StartTime = s.StartTime,
                        Duration = s.Duration,
                        EndTime = s.EndTime,
                        Price = s.Price,
                        HallId = s.HallId,
                        HallName = s.Hall.Name
                    })
                    .ToList()
            })
            .OrderBy(p => p.Title)
            .ThenBy(p => p.Duration)
            .Skip((query.Page - 1) * limit)
            .Take(limit)
            .ToListAsync(cancellationToken);

        return new MovieListResponse {Items = result};
    }
}


