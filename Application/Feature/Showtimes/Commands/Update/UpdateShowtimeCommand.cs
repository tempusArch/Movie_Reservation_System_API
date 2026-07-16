using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record UpdateShowtimeCommand(int ShowtimeId, UpdateShowtimeDto UpdateShowtimeDto) : IRequest<Showtime>;