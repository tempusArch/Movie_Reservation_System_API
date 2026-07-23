using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class CreateShowtimeHandler : IRequestHandler<CreateShowtimeCommand, Showtime> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public CreateShowtimeHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Showtime> Handle(CreateShowtimeCommand command, CancellationToken cancellationToken) {
        var isExisted = await _context.ShowtimeTable
            .AnyAsync(st => st.StartTime == command.CreateShowtimeDto.StartTime);

        if (isExisted)
            throw new InvalidOperationException("StartTime already has a Showtime");

        var theHall = await _context.HallTable
            .AnyAsync(x => x.Id == command.CreateShowtimeDto.HallId, cancellationToken);

        if (!theHall)
            throw new NotFoundException("Hall not found");

        var theMovie = await _context.MovieTable
            .AsNoTracking()
            .Where(x => x.Id == command.CreateShowtimeDto.MovieId)
            .Select(x => new {
                x.Duration
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (theMovie == null)
            throw new NotFoundException("Movie not found");

        var newOne = _mapper.Map<Showtime>(command.CreateShowtimeDto);

        newOne.Duration = theMovie.Duration;
        newOne.EndTime = newOne.StartTime.Add(newOne.Duration);

        var hasOverlap = await _context.ShowtimeTable
            .AnyAsync(x =>
                x.HallId == command.CreateShowtimeDto.HallId &&
                command.CreateShowtimeDto.StartTime < x.EndTime &&
                newOne.EndTime > x.StartTime);

        if (hasOverlap)
            throw new InvalidOperationException("Same hall overlapping showtime");
       
        _context.ShowtimeTable.Add(newOne);
        await _context.SaveChangesAsync(cancellationToken);

        return newOne;
    }
}