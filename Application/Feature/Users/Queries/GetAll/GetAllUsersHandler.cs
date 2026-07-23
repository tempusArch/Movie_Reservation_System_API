using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, UserListResponseAdmin> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetAllUsersHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserListResponseAdmin> Handle(GetAllUsersQuery query, CancellationToken cancellationToken) {
        var limit = Math.Min(query.Limit, 100);
        
        var result = await _context.UserTable
            .AsNoTracking()
            .Select(x => new ReadUserDtoAdmin {
                UserId = x.Id,
                UserName = x.Name,
                Email = x.Email,
                Role = x.Role,
                ReservationRisuto = x.ReservationRisuto
                    .Select(z => new ReadReservationDtoAdmin {
                        ReservationId = z.Id,

                        UserId = z.UserId,
                        ShowtimeId = z.ShowtimeId,
                        SeatQuantity = z.ReservedSeatRisuto.Count(),
                
                        MovieName = z.Showtime.Movie.Title,
                        StartTime = z.Showtime.StartTime,
                        Duration = z.Showtime.Duration,

                        TotalPrice = z.ReservedSeatRisuto.Count() * z.Showtime.Price,
                        ReservationStatus = z.ReservationStatus,

                        HallName = z.Showtime.Hall.Name,
                        SeatLabelRisuto = z.ReservedSeatRisuto
                            .Select(s => $"{s.Seat.Row}{s.Seat.LineNumber}")
                            .ToList(),
                        CreatedAt = z.CreatedAt,
                    })
                    .ToList()
            })
            .OrderBy(x => x.Role)
            .ThenBy(x => x.UserId)
            .Skip((query.Page - 1) * limit)
            .Take(limit)
            .ToListAsync(cancellationToken);

        return new UserListResponseAdmin {Items = result};
    }
}


