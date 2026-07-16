using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record GetAllReservationsUserQuery(int UserId) : IRequest<ReservationListResponseUser>;