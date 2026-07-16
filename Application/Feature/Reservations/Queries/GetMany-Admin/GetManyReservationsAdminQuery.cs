using MediatR;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public record GetManyReservationsAdminQuery(
    int? UserId, 
    int? ShowtimeId, 
    int? MovieId, 
    int? HallId, 
    ReservationStatus? ReservationStatus, 
    DateOnly? Start, 
    DateOnly? End, 
    int Page, 
    int Limit) 
    : IRequest<ReservationListResponseAdmin>;