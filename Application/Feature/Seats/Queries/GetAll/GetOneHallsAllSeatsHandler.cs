using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetOneHallsAllSeatsHandler : IRequestHandler<GetOneHallsAllSeatsQuery, SeatListResponseAdmin> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetOneHallsAllSeatsHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SeatListResponseAdmin> Handle(GetOneHallsAllSeatsQuery query, CancellationToken cancellationToken) {     
        var result = await _context.SeatTable
            .AsNoTracking()
            .Where(s => s.HallId == query.HallId)
            .Select(x => new ReadSeatDtoAdmin {
                SeatId = x.Id,
                Row = x.Row,
                LineNumber = x.LineNumber,
                HallId = x.HallId,
                HallName = x.Hall.Name
            })
            .OrderBy(s => s.Row)
            .ThenBy(s => s.LineNumber)
            .ToListAsync(cancellationToken);

        return new SeatListResponseAdmin {Items = result};
    }
}


