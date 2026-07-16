using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetOneUserHandler : IRequestHandler<GetOneUserQuery, ReadUserDtoAdmin> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetOneUserHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReadUserDtoAdmin> Handle(GetOneUserQuery query, CancellationToken cancellationToken) {
        var theOne = await _context.UserTable
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
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
            .FirstOrDefaultAsync(cancellationToken);

        if (theOne == null)
            throw new NotFoundException("User not found");

        return theOne;
    }
}
