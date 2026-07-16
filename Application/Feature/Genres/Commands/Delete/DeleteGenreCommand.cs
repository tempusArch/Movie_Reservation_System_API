using MediatR;

namespace MovieReservationSystemAPI.Application;

public record DeleteGenreCommand(int Id) : IRequest<Unit>;