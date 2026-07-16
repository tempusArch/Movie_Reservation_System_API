using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record CreateShowtimeCommand(CreateShowtimeDto CreateShowtimeDto) : IRequest<Showtime>;