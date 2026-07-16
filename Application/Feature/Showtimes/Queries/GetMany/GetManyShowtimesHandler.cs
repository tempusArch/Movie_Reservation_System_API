using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetManyShowtimesHandler : IRequestHandler<GetManyShowtimesQuery, ShowtimeListResponse> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetManyShowtimesHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ShowtimeListResponse> Handle(GetManyShowtimesQuery query, CancellationToken cancellationToken) {
        IQueryable<Showtime> source = _context.ShowtimeTable.AsNoTracking();

        if (query.MovieId != null) 
            source = source.Where(s => s.MovieId == query.MovieId);

        if (query.HallId != null)
            source = source.Where(s => s.HallId == query.HallId);

        if (query.DateOnly != null)
            source = source.Where(s => DateOnly.FromDateTime(s.StartTime) >= query.DateOnly);

        var result = source
            .Select(x => new ReadShowtimeDto {
                ShowtimeId = x.Id,

                StartTime = x.StartTime,
                Duration = x.Duration,
                EndTime = x.EndTime,

                Price = x.Price,

                HallId = x.HallId,
                HallName = x.Hall.Name,

                MovieId = x.MovieId,
                MovieName = x.Movie.Title,
            });
                 
        var limit = Math.Min(query.Limit, 100);
            
        var arranged = await result
            .OrderBy(p => p.MovieName)
            .ThenBy(p => p.StartTime)
            .Skip((query.Page - 1) * limit)
            .Take(limit)
            .ToListAsync(cancellationToken);

        return new ShowtimeListResponse {Items = arranged};
    }
}

