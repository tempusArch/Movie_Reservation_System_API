using MediatR;

namespace MovieReservationSystemAPI.Application;

public record GetOneShowtimeQuery(int Id) : IRequest<ReadShowtimeDto>;