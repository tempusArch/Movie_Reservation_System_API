using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetManyReservationsAdminHandler : IRequestHandler<GetManyReservationsAdminQuery, ReservationListResponseAdmin> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetManyReservationsAdminHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReservationListResponseAdmin> Handle(GetManyReservationsAdminQuery query, CancellationToken cancellationToken) {
        IQueryable<Reservation> source = _context.ReservationTable.AsNoTracking();

        if (query.UserId != null)
            source = source.Where(s => s.UserId == query.UserId);

        if (query.ShowtimeId != null)
            source = source.Where(s => s.ShowtimeId == query.ShowtimeId);

        if (query.MovieId != null)
            source = source.Where(s => s.Showtime.MovieId == query.MovieId);

        if (query.HallId != null)
            source = source.Where(s => s.Showtime.HallId == query.HallId);

        if (query.ReservationStatus != null)
            source = source.Where(s => s.ReservationStatus == query.ReservationStatus);

        var start = query.Start;
        var end = query.End;

        if (query.Start != null && query.End != null && query.Start > query.End) {
            (start, end) = (end, start);
            source = source
                .Where(s => 
                    DateOnly.FromDateTime(s.CreatedAt) > start && 
                    DateOnly.FromDateTime(s.CreatedAt) < end);
        }

        if (query.Start != null)
            source = source.Where(s => DateOnly.FromDateTime(s.CreatedAt) > query.Start);

        if (query.End != null)
            source = source.Where(s => DateOnly.FromDateTime(s.CreatedAt) < query.End);

        var totalCount = await source.CountAsync(cancellationToken);
        var totalPrice = (await source
            .Select(x => new {
                tempPrice = x.Showtime.Price,
                SeatCount = x.ReservedSeatRisuto.Count()
            })
            .ToListAsync(cancellationToken))
            .Sum(x => x.tempPrice * x.SeatCount);

        var result = source
            .Select(r => new ReadReservationDtoAdmin {
                ReservationId = r.Id,
                
                UserId = r.UserId,
                ShowtimeId = r.ShowtimeId,
                SeatQuantity = r.ReservedSeatRisuto.Count(),
                
                MovieName = r.Showtime.Movie.Title,
                StartTime = r.Showtime.StartTime,
                Duration = r.Showtime.Movie.Duration,
                TotalPrice = r.ReservedSeatRisuto.Count() * r.Showtime.Price,
                ReservationStatus = r.ReservationStatus,
                HallName = r.Showtime.Hall.Name,
                SeatLabelRisuto = r.ReservedSeatRisuto
                    .Select(rs => $"{rs.Seat.Row}{rs.Seat.LineNumber}")
                    .ToList(),
                CreatedAt = r.CreatedAt,
            });

        var limit = Math.Min(query.Limit, 100);
            
        var arranged = await result
            .OrderBy(p => p.MovieName)
            .ThenBy(p => p.CreatedAt)
            .Skip((query.Page - 1) * limit)
            .Take(limit)
            .ToListAsync(cancellationToken);

        return new ReservationListResponseAdmin {
            Items = arranged,
            TotalCount = totalCount,
            TotalPrice = totalPrice,           
        };
    }
}


