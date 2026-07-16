using MediatR;

namespace MovieReservationSystemAPI.Application;

public record GetOneHallQuery(int Id) : IRequest<ReadHallDto>;