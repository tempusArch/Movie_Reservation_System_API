using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record GetOneReservationUserQuery(int UserId, int ReservationId) : IRequest<ReadReservationDtoUser>;