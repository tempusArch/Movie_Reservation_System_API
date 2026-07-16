using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetOneGenreHandler : IRequestHandler<GetOneGenreQuery, ReadGenreDto> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetOneGenreHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReadGenreDto> Handle(GetOneGenreQuery query, CancellationToken cancellationToken) {
        var theOne = await _context.GenreTable
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Select(x => new ReadGenreDto {
                GenreId = x.Id,
                GenreName = x.Name,
                MovieRisuto = x.MovieRisuto
                    .Select(z => new ReadMovieDto {
                        MovieId = z.Id,
                        Title = z.Title,
                        Description = z.Description,
                        Duration = z.Duration
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);


        if (theOne == null)
            throw new NotFoundException("Genre not found");

        return theOne;
    }
}
