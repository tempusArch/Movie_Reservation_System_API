using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetOneReservationAdminHandler : IRequestHandler<GetOneReservationAdminQuery, ReadReservationDtoAdmin> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetOneReservationAdminHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReadReservationDtoAdmin> Handle(GetOneReservationAdminQuery query, CancellationToken cancellationToken) {
        var result = await _context.ReservationTable
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Select(x => new ReadReservationDtoAdmin {
                ReservationId = x.Id,

                UserId = x.UserId,
                ShowtimeId = x.ShowtimeId,
                SeatQuantity = x.ReservedSeatRisuto.Count(),
                
                MovieName = x.Showtime.Movie.Title,
                StartTime = x.Showtime.StartTime,
                Duration = x.Showtime.Duration,

                TotalPrice = x.ReservedSeatRisuto.Count() * x.Showtime.Price,
                ReservationStatus = x.ReservationStatus,

                HallName = x.Showtime.Hall.Name,
                SeatLabelRisuto = x.ReservedSeatRisuto
                    .Select(z => $"{z.Seat.Row}{z.Seat.LineNumber}")
                    .ToList(),
                CreatedAt = x.CreatedAt,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (result == null)
            throw new NotFoundException("Reservation not found");

        return result;
        
    }
}
