using MediatR;

namespace MovieReservationSystemAPI.Application;

public record GetAllUsersQuery(int Page, int Limit) : IRequest<UserListResponseAdmin>;