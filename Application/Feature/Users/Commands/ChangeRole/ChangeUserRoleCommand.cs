using MediatR;

namespace MovieReservationSystemAPI.Application;

public record ChangeUserRoleCommand(int CurrentUserId, ChangeUserRoleDto ChangeUserRoleDto) : IRequest<Unit>;