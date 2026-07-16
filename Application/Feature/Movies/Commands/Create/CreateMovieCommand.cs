using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record CreateMovieCommand(CreateMovieDto CreateMovieDto) : IRequest<Movie>;