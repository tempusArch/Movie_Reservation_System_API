using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetAllHallsHandler : IRequestHandler<GetAllHallsQuery, HallListResponse> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetAllHallsHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HallListResponse> Handle(GetAllHallsQuery query, CancellationToken cancellationToken) {
        var result = _context.HallTable
            .AsNoTracking()
            .Select(x => new ReadHallDto {
                HallId = x.Id,
                HallName = x.Name,

                SeatRisuto = x.SeatRisuto
                    .Select(z => new ReadSeatDtoAdmin {
                        SeatId = z.Id,
                        Row = z.Row,
                        LineNumber = z.LineNumber
                    })
                    .ToList()
            });

        var limit = Math.Min(query.Limit, 100);

        var arranged = await result
            .OrderBy(h => h.HallName)
            .Skip((query.Page - 1) * limit)
            .Take(limit)
            .ToListAsync(cancellationToken);

        return new HallListResponse {Items = arranged};
    }
}


