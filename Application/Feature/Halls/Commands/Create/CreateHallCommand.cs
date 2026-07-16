using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record CreateHallCommand(string Name) : IRequest<Hall>;