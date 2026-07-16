using MediatR;

namespace MovieReservationSystemAPI.Application;

public record CreateReservationCommand(int UserId, CreateReservationDto CreateReservationDto) : IRequest<ReadReservationDtoUser>;