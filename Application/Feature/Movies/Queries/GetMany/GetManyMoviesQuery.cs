using MediatR;

namespace MovieReservationSystemAPI.Application;

public record GetManyMoviesQuery(string? GenreName, string? MovieName, int Page, int Limit) : IRequest<MovieListResponse>;