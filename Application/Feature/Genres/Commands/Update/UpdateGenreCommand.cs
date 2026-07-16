using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record UpdateGenreCommand(int GenreId, string GenreName) : IRequest<Genre>;