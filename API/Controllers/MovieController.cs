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
public class MovieController : ControllerBase {
    private readonly IMediator _mediator;
    private readonly IHttpContextService _httpContextService;

    public MovieController(IMediator mediator, IHttpContextService httpContextService) { 
        _mediator = mediator;
        _httpContextService = httpContextService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ReadMovieDto>> CreateMovie(CreateMovieDto dto) {
        _httpContextService.CheckUserIdClaim();

        var result = await _mediator.Send(new CreateMovieCommand(dto));

        return Created(string.Empty, result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{movieId}")]
    public async Task<ActionResult<ReadMovieDto>> UpdateMovie(int movieId, UpdateMovieDto dto) {
        _httpContextService.CheckUserIdClaim();
        
        return Ok(await _mediator.Send(new UpdateMovieCommand(movieId, dto)));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{movieId}")]
    public async Task<IActionResult> DeleteMovie(int movieId) {
        _httpContextService.CheckUserIdClaim();
                    
        await _mediator.Send(new DeleteMovieCommand(movieId));

        return NoContent();
    }

    [HttpGet("{movieId}")]
    [AllowAnonymous]
    public async Task<ActionResult<ReadMovieDto>> GetOneMovie(int movieId) {
        return Ok(await _mediator.Send(new GetOneMovieQuery(movieId)));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<MovieListResponse>> GetManyMovies(
        [FromQuery] string? genreName,
        [FromQuery] string? movieName,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10
    ) {
        
        return Ok(await _mediator.Send(new GetManyMoviesQuery(genreName, movieName, page, limit)));
    }
}