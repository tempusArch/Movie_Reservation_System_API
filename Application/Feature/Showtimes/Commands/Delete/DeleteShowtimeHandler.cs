using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class DeleteShowtimeHandler : IRequestHandler<DeleteShowtimeCommand, Unit> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public DeleteShowtimeHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteShowtimeCommand command, CancellationToken cancellationToken) {
        var theOne = await _context.ShowtimeTable
            .FirstOrDefaultAsync(st => st.Id == command.Id, cancellationToken);

        if (theOne == null)
            throw new NotFoundException("Showtime not found");

        var isReservationExisted = await _context.ReservationTable
            .AnyAsync(x => 
                x.ShowtimeId == command.Id &&
                x.ReservationStatus == ReservationStatus.Processing
            );
        
        if (isReservationExisted)
            throw new InvalidOperationException("There is processing reservation in this showtime");
       
        _context.ShowtimeTable.Remove(theOne);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}