using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetManyGenresHandler : IRequestHandler<GetManyGenresQuery, GenreListResponse> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetManyGenresHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GenreListResponse> Handle(GetManyGenresQuery query, CancellationToken cancellationToken) {
        IQueryable<Genre> source = _context.GenreTable.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.GenreName)) 
            source = source.Where(g => g.Name.Contains(query.GenreName));

        if (!string.IsNullOrWhiteSpace(query.MovieName))
            source = source.Where(g => g.MovieRisuto.Any(m => m.Title.Contains(query.MovieName)));

        var limit = Math.Min(query.Limit, 100);

        var result = await source
            .Select(x => new ReadGenreDto {
                GenreId = x.Id,
                GenreName = x.Name,
                MovieRisuto = x.MovieRisuto
                    .Select(z => new ReadMovieDto {
                        MovieId = z.Id,
                        Title = z.Title,
                        Description = z.Description,
                        Duration = z.Duration
                    })
                    .ToList()
            })
            .OrderBy(p => p.GenreName)
            .Skip((query.Page - 1) * limit)
            .Take(limit)
            .ToListAsync(cancellationToken);

        return new GenreListResponse {Items = result};
    }
}

