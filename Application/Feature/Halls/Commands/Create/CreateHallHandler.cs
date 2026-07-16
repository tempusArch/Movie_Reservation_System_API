using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;


namespace MovieReservationSystemAPI.Application;

public class CreateHallHandler : IRequestHandler<CreateHallCommand, Hall> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public CreateHallHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Hall> Handle(CreateHallCommand command, CancellationToken cancellationToken) {
        var isExisted = await _context.HallTable.AnyAsync(h => h.Name == command.Name, cancellationToken);

        if (isExisted)
            throw new InvalidOperationException("Hall already existed");

        var newOne = new Hall {
            Name = command.Name
        };

        _context.HallTable.Add(newOne);
        await _context.SaveChangesAsync(cancellationToken);

        return newOne;
    }
}