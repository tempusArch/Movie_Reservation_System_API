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
public class ShowtimeController : ControllerBase {
    private readonly IMediator _mediator;
    private readonly IHttpContextService _httpContextService;

    public ShowtimeController(IMediator mediator, IHttpContextService httpContextService) { 
        _mediator = mediator;
        _httpContextService = httpContextService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ReadShowtimeDto>> CreateShowtime(CreateShowtimeDto dto) {
        _httpContextService.CheckUserIdClaim();

        var result = await _mediator.Send(new CreateShowtimeCommand(dto));

        return Created(string.Empty, result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{showtimeId}")]
    public async Task<ActionResult<ReadShowtimeDto>> UpdateShowtime(int showtimeId, UpdateShowtimeDto dto) {
        _httpContextService.CheckUserIdClaim();

        return Ok(await _mediator.Send(new UpdateShowtimeCommand(showtimeId, dto)));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{showtimeId}")]
    public async Task<IActionResult> DeleteShowtime(int showtimeId) {
        _httpContextService.CheckUserIdClaim();
                    
        await _mediator.Send(new DeleteShowtimeCommand(showtimeId));

        return NoContent();
    }

    [HttpGet("{showtimeId}")]
    [AllowAnonymous]
    public async Task<ActionResult<ReadShowtimeDto>> GetOneShowtime(int showtimeId) {
        return Ok(await _mediator.Send(new GetOneShowtimeQuery(showtimeId)));
    }
        
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ShowtimeListResponse>> GetManyShowtimes(
        [FromQuery] int? movieId,
        [FromQuery] int? hallId,
        [FromQuery] DateOnly? dateOnly,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10
    ) {
        
        return Ok(await _mediator.Send(new GetManyShowtimesQuery(movieId, hallId, dateOnly, page, limit)));
    }
}