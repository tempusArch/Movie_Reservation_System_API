using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record GetAllSeatsQuery : IRequest<SeatListResponseAdmin>;