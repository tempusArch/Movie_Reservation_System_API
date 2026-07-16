using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetAllSeatsHandler : IRequestHandler<GetAllSeatsQuery, SeatListResponseAdmin> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetAllSeatsHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SeatListResponseAdmin> Handle(GetAllSeatsQuery query, CancellationToken cancellationToken) {
        var result = _context.SeatTable
            .AsNoTracking()
            .Select(x => new ReadSeatDtoAdmin {
                SeatId = x.Id,
                Row = x.Row,
                LineNumber = x.LineNumber,
                HallId = x.HallId,
                HallName = x.Hall.Name
            });

        var arranged = await result
            .OrderBy(s => s.HallName)
            .ThenBy(s => s.Row)
            .ThenBy(s => s.LineNumber)
            .ToListAsync(cancellationToken);

        return new SeatListResponseAdmin {Items = arranged};
    }
}


