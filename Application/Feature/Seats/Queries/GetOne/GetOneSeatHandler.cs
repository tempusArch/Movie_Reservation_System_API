using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class GetOneSeatHandler : IRequestHandler<GetOneSeatQuery, ReadSeatDtoAdmin> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IMapper _mapper;
    public GetOneSeatHandler(MovieReservationSystemApiDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReadSeatDtoAdmin> Handle(GetOneSeatQuery query, CancellationToken cancellationToken) {
        var theOne = await _context.SeatTable
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Select(x => new ReadSeatDtoAdmin {
                SeatId = x.Id,
                Row = x.Row,
                LineNumber = x.LineNumber,
                HallId = x.HallId,
                HallName = x.Hall.Name
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (theOne == null)
            throw new NotFoundException("Seat not found");

        return theOne;

    }
}
