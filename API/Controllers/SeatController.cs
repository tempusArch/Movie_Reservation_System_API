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
public class SeatController : ControllerBase {
    private readonly IMediator _mediator;
    private readonly IHttpContextService _httpContextService;

    public SeatController(IMediator mediator, IHttpContextService httpContextService) { 
        _mediator = mediator;
        _httpContextService = httpContextService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ReadSeatDto>> CreateSeat(CreateSeatDto dto) {
        _httpContextService.CheckUserIdClaim();

        var result = await _mediator.Send(new CreateSeatCommand(dto));

        return Created(string.Empty, result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{seatId}")]
    public async Task<ActionResult<ReadSeatDto>> UpdateSeat(int seatId, UpdateSeatDto dto) {
        _httpContextService.CheckUserIdClaim();

        return Ok(await _mediator.Send(new UpdateSeatCommand(seatId, dto)));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{seatId}")]
    public async Task<IActionResult> DeleteSeat(int seatId) {
        _httpContextService.CheckUserIdClaim();
                    
        await _mediator.Send(new DeleteSeatCommand(seatId));

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{seatId}")]
    public async Task<ActionResult<ReadSeatDto>> GetOneSeat(int seatId) {
        _httpContextService.CheckUserIdClaim();

        return Ok(await _mediator.Send(new GetOneSeatQuery(seatId)));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("Hall/{hallId}")]
    public async Task<ActionResult<ReadSeatDto>> GetOneHallsAllSeats(int hallId) {
        _httpContextService.CheckUserIdClaim();

        return Ok(await _mediator.Send(new GetOneHallsAllSeatsQuery(hallId)));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<ReadSeatDto>> GetAllSeats() {
        _httpContextService.CheckUserIdClaim();

        return Ok(await _mediator.Send(new GetAllSeatsQuery()));
    }
    
    [HttpGet("Available/{showtimeId}")]
    [AllowAnonymous]
    public async Task<ActionResult<ReadSeatDto>> GetOneShowtimeAvailableSeats(int showtimeId) {
            
        return Ok(await _mediator.Send(new GetOneShowtimeAvailableSeatsQuery(showtimeId)));
    }
}