using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record UpdateHallCommand(int HallId, string HallName) : IRequest<Hall>;