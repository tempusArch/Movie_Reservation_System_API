using MediatR;

namespace MovieReservationSystemAPI.Application;

public record GetOneMovieQuery(int Id) : IRequest<ReadMovieDto>;