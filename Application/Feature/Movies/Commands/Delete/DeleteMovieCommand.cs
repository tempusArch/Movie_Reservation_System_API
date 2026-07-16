using MediatR;

namespace MovieReservationSystemAPI.Application;

public record DeleteMovieCommand(int Id) : IRequest<Unit>;