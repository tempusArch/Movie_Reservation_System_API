using MediatR;

namespace MovieReservationSystemAPI.Application;

public record GetManyGenresQuery(string? GenreName, string? MovieName, int Page, int Limit) : IRequest<GenreListResponse>;