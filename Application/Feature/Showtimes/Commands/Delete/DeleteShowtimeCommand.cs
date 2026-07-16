using MediatR;

namespace MovieReservationSystemAPI.Application;

public record DeleteShowtimeCommand(int Id) : IRequest<Unit>;