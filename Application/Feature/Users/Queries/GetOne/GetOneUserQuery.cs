using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record GetOneUserQuery(int Id) : IRequest<ReadUserDtoAdmin>;