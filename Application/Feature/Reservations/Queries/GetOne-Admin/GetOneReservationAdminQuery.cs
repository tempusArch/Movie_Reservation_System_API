using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record GetOneReservationAdminQuery(int Id) : IRequest<ReadReservationDtoAdmin>;