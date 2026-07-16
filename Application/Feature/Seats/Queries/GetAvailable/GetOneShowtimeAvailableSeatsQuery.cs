using MediatR;

namespace MovieReservationSystemAPI.Application;

public record GetOneShowtimeAvailableSeatsQuery(int ShowtimeId) : IRequest<SeatListResponseUser>;