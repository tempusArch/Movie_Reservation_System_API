using MediatR;

namespace MovieReservationSystemAPI.Application;

public record DeleteSeatCommand(int Id) : IRequest<Unit>;