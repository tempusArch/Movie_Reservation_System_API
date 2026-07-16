using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record GetOneHallsAllSeatsQuery(int HallId) : IRequest<SeatListResponseAdmin>;