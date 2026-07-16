using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Application;
using MovieReservationSystemAPI.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MovieReservationSystemAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase {
    private readonly IMediator _mediator;
    private readonly IHttpContextService _httpContextService;

    public ReservationController(IMediator mediator, IHttpContextService httpContextService) { 
        _mediator = mediator;
        _httpContextService = httpContextService;
    }

    [HttpPost]
    public async Task<ActionResult<ReadReservationDtoUser>> CreateReservation(CreateReservationDto dto) {
        var userId = _httpContextService.GetCurrentUserId();

        var result = await _mediator.Send(new CreateReservationCommand(userId, dto));

        return Created(string.Empty, result);
    }

    [HttpPut("{reservationId}")]
    public async Task<ActionResult<ReadReservationDtoUser>> CancelReservation(int reservationId) {
        var userId = _httpContextService.GetCurrentUserId();

        await _mediator.Send(new CancelReservationCommand(userId, reservationId));

        return NoContent();
    }

    [HttpGet("{reservationId}")]
    public async Task<ActionResult<ReadReservationDtoUser>> GetOneReservationUser(int reservationId) {
        var userId = _httpContextService.GetCurrentUserId();

        return Ok(await _mediator.Send(new GetOneReservationUserQuery(userId, reservationId)));
    }

    [HttpGet]
    public async Task<ActionResult<ReservationListResponseUser>> GetAllReservationsUser() {
        var userId = _httpContextService.GetCurrentUserId();

        return Ok(await _mediator.Send(new GetAllReservationsUserQuery(userId)));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("Admin/{reservationId}")]
    public async Task<ActionResult<ReadReservationDtoAdmin>> GetOneReservationAdmin(int reservationId) {
        _httpContextService.CheckUserIdClaim();

        return Ok(await _mediator.Send(new GetOneReservationAdminQuery(reservationId)));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("Admin")]  
    public async Task<ActionResult<ReservationListResponseAdmin>> GetManyReservationsAdmin(
        [FromQuery] int? userId,
        [FromQuery] int? showtimeId,
        [FromQuery] int? movieId,
        [FromQuery] int? hallId,
        [FromQuery] ReservationStatus? reservationStatus,
        [FromQuery] DateOnly? start,
        [FromQuery] DateOnly? end,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10
        
    ) {
        _httpContextService.CheckUserIdClaim();

        return Ok(await _mediator.Send(
            new GetManyReservationsAdminQuery(
                userId, showtimeId, movieId, hallId, reservationStatus, start, end, page, limit
            )
        ));
    }
}