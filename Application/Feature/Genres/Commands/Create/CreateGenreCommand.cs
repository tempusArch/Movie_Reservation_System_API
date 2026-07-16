using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record CreateGenreCommand(string Name) : IRequest<Genre>;