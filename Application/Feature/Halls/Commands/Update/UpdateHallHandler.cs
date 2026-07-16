using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class UpdateHallHandler : IRequestHandler<UpdateHallCommand, Hall> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public UpdateHallHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Hall> Handle(UpdateHallCommand command, CancellationToken cancellationToken) {
        var isExisted = await _context.HallTable.AnyAsync(h => h.Name == command.HallName, cancellationToken);

        if (isExisted)
            throw new InvalidOperationException("Hall already existed");

        var theOne = await _context.HallTable
            .FirstOrDefaultAsync(g => g.Id == command.HallId);

        if (theOne == null)
            throw new NotFoundException("Hall not found");

        theOne.Name = command.HallName;
        await _context.SaveChangesAsync(cancellationToken);

        return theOne;
    }
}
