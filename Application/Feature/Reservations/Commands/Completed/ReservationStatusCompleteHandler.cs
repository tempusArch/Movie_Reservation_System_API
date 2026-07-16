/*using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class ReservationStatusCompleteHandler : IRequestHandler<ReservationStatusCompleteCommand, Unit> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public ReservationStatusCompleteHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(ReservationStatusCompleteCommand command, CancellationToken cancellationToken) {
        await _context.ReservationTable
            .Where(r =>
                r.ReservationStatus == ReservationStatus.Processing &&
                r.Showtime.EndTime <= DateTime.UtcNow)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(
                    r => r.ReservationStatus,
                    ReservationStatus.Completed
                ),
                cancellationToken
            );

        return Unit.Value;
    }
}*/
