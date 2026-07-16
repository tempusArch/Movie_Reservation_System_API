using MediatR;

namespace MovieReservationSystemAPI.Application;

public record GetAllHallsQuery(int Page, int Limit) : IRequest<HallListResponse>;