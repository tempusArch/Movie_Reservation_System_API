using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetOneShowtimeAvailableSeatsHandler : IRequestHandler<GetOneShowtimeAvailableSeatsQuery, SeatListResponseUser> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetOneShowtimeAvailableSeatsHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SeatListResponseUser> Handle(GetOneShowtimeAvailableSeatsQuery query, CancellationToken cancellationToken) {
        var theShowtime = await _context.ShowtimeTable
            .FirstOrDefaultAsync(x => x.Id == query.ShowtimeId, cancellationToken);

        if (theShowtime == null)
            throw new NotFoundException("Showtime not found");
        
        var reservedSeatIdRisuto = await _context.ReservedSeatTable
            .AsNoTracking()
            .Where(rs => 
                rs.ShowtimeId == query.ShowtimeId &&
                rs.Reservation.ReservationStatus == ReservationStatus.Processing)
            .Select(rs => rs.SeatId)
            .ToListAsync(cancellationToken);

        var result = await _context.SeatTable
            .AsNoTracking()
            .Where(s => s.HallId == theShowtime.HallId)
            .Select(s => new ReadSeatDto {
                SeatId = s.Id,
                HallName = s.Hall.Name,
                Label = $"{s.Row}{s.LineNumber}",
                IsAvailable = !reservedSeatIdRisuto.Contains(s.Id)
            })
            .ToListAsync(cancellationToken);

        return new SeatListResponseUser {Items = result};
    }
}


