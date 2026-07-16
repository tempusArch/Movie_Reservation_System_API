using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetOneHallHandler : IRequestHandler<GetOneHallQuery, ReadHallDto> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetOneHallHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReadHallDto> Handle(GetOneHallQuery query, CancellationToken cancellationToken) {
        var theOne = await _context.HallTable
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
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
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (theOne == null)
            throw new NotFoundException("Hall not found");

        return theOne;
    }
}
