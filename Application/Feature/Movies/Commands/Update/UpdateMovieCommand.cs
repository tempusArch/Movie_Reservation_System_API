using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record UpdateMovieCommand(int MovieId, UpdateMovieDto UpdateMovieDto) : IRequest<Movie>;