using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetAllReservationsUserHandler : IRequestHandler<GetAllReservationsUserQuery, ReservationListResponseUser> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetAllReservationsUserHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReservationListResponseUser> Handle(GetAllReservationsUserQuery query, CancellationToken cancellationToken) {
        var result = await _context.ReservationTable
            .AsNoTracking()
            .Where(x => x.UserId == query.UserId)
            .Select(x => new ReadReservationDtoUser {
                ReservationId = x.Id,
                
                MovieName = x.Showtime.Movie.Title,
                StartTime = x.Showtime.StartTime,
                Duration = x.Showtime.Movie.Duration,
                TotalPrice = x.ReservedSeatRisuto.Count() * x.Showtime.Price,
                ReservationStatus = x.ReservationStatus,
                HallName = x.Showtime.Hall.Name,
                SeatLabelRisuto = x.ReservedSeatRisuto
                    .Select(rs => $"{rs.Seat.Row}{rs.Seat.LineNumber}")
                    .ToList(),
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync(cancellationToken);

        return new ReservationListResponseUser {Items = result};
    }
}
