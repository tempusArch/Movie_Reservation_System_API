using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record CreateSeatCommand(CreateSeatDto CreateSeatDto) : IRequest<Seat>;