using MediatR;

namespace MovieReservationSystemAPI.Application;

public record DeleteHallCommand(int Id) : IRequest<Unit>;