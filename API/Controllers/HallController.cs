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
public class HallController : ControllerBase {
    private readonly IMediator _mediator;
    private readonly IHttpContextService _httpContextService;

    public HallController(IMediator mediator, IHttpContextService httpContextService) { 
        _mediator = mediator;
        _httpContextService = httpContextService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ReadHallDto>> CreateHall([FromQuery] string hallName) {
        _httpContextService.CheckUserIdClaim();

        var result = await _mediator.Send(new CreateHallCommand(hallName));

        return Created(string.Empty, result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{hallId}")]
    public async Task<ActionResult<ReadHallDto>> UpdateHall(int hallId, [FromQuery] string hallName) {
        _httpContextService.CheckUserIdClaim();
        
        return Ok(await _mediator.Send(new UpdateHallCommand(hallId, hallName)));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{hallId}")]
    public async Task<IActionResult> DeleteHall(int hallId) {
        _httpContextService.CheckUserIdClaim();
                    
        await _mediator.Send(new DeleteHallCommand(hallId));

        return NoContent();
    }

    [HttpGet("{hallId}")]
    [AllowAnonymous]
    public async Task<ActionResult<ReadHallDto>> GetOneHall(int hallId) {
        return Ok(await _mediator.Send(new GetOneHallQuery(hallId)));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<HallListResponse>> GetAllHalls(
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10
    ) {
        
        return Ok(await _mediator.Send(new GetAllHallsQuery(page, limit)));
    }
}