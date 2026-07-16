using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record UpdateSeatCommand(int SeatId, UpdateSeatDto UpdateSeatDto) : IRequest<Seat>;