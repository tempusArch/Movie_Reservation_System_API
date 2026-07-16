using MediatR;

namespace MovieReservationSystemAPI.Application;

public record RefreshUserCommand : IRequest<string>;