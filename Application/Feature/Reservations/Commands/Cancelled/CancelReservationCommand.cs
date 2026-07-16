using MediatR;

namespace MovieReservationSystemAPI.Application;

public record CancelReservationCommand(int UserId, int ReservationId) : IRequest<Unit>;