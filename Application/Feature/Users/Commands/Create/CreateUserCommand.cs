using MediatR;

namespace MovieReservationSystemAPI.Application;

public record CreateUserCommand(RegisterUserDto RegisterUserDto) : IRequest<UserResponse>;