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
public class GenreController : ControllerBase {
    private readonly IMediator _mediator;
    private readonly IHttpContextService _httpContextService;

    public GenreController(IMediator mediator, IHttpContextService httpContextService) { 
        _mediator = mediator;
        _httpContextService = httpContextService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ReadGenreDto>> CreateGenre([FromQuery] string genreName) {
        _httpContextService.CheckUserIdClaim();

        var result = await _mediator.Send(new CreateGenreCommand(genreName));

        return Created(string.Empty, result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{genreId}")]
    public async Task<ActionResult<ReadGenreDto>> UpdateGenre(int genreId, [FromQuery] string genreName) {
        _httpContextService.CheckUserIdClaim();

        return Ok(await _mediator.Send(new UpdateGenreCommand(genreId, genreName)));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{genreId}")]
    public async Task<IActionResult> DeleteGenre(int genreId) {
        _httpContextService.CheckUserIdClaim();
                    
        await _mediator.Send(new DeleteGenreCommand(genreId));

        return NoContent();
    }

    [HttpGet("{genreId}")]
    [AllowAnonymous]
    public async Task<ActionResult<ReadGenreDto>> GetOneGenre(int genreId) {
        return Ok(await _mediator.Send(new GetOneGenreQuery(genreId)));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<GenreListResponse>> GetManyGenres(
        [FromQuery] string? genreName,
        [FromQuery] string? movieName,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10
    ) {
        
        return Ok(await _mediator.Send(new GetManyGenresQuery(genreName, movieName, page, limit)));
    }
}