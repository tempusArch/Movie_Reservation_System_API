using MediatR;

namespace MovieReservationSystemAPI.Application;

public record GetManyShowtimesQuery(int? MovieId, int? HallId, DateOnly? DateOnly, int Page, int Limit) : IRequest<ShowtimeListResponse>;