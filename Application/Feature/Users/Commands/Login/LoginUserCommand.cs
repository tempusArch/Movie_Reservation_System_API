using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record LoginUserCommand(LoginUserDto LoginUserDto) : IRequest<string>;