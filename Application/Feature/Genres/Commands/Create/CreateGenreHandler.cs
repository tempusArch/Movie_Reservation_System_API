using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;


namespace MovieReservationSystemAPI.Application;

public class CreateGenreHandler : IRequestHandler<CreateGenreCommand, Genre> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public CreateGenreHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Genre> Handle(CreateGenreCommand command, CancellationToken cancellationToken) {
        var isExisted = await _context.GenreTable.AnyAsync(g => g.Name == command.Name, cancellationToken);

        if (isExisted)
            throw new InvalidOperationException("Genre already existed");

        var newOne = new Genre {
            Name = command.Name
        };

        _context.GenreTable.Add(newOne);
        await _context.SaveChangesAsync(cancellationToken);

        return newOne;
    }
}