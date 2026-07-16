using MediatR;

namespace MovieReservationSystemAPI.Application;

public record GetOneGenreQuery(int Id) : IRequest<ReadGenreDto>;