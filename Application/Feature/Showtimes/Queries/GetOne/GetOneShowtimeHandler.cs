using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetOneShowtimeHandler : IRequestHandler<GetOneShowtimeQuery, ReadShowtimeDto> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetOneShowtimeHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReadShowtimeDto> Handle(GetOneShowtimeQuery query, CancellationToken cancellationToken) {
        var theOne = await _context.ShowtimeTable
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
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
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (theOne == null)
            throw new NotFoundException("Showtime not found");

        return theOne;
    }
}
