using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record GetOneSeatQuery(int Id) : IRequest<ReadSeatDtoAdmin>;