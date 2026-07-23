using MediatR;

namespace MovieReservationSystemAPI.Application;

public record RegisterUserCommand(RegisterUserDto RegisterUserDto) : IRequest<UserResponse>;