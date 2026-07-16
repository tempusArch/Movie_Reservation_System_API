using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieReservationSystemAPI.Domain;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class LogoutUserHandler : IRequestHandler<LogoutUserCommand, Unit> {
    private readonly MovieReservationSystemApiDbContext _context;
    private readonly IHttpContextService _httpContextService;
    public LogoutUserHandler(MovieReservationSystemApiDbContext context, IHttpContextService httpContextService) {
        _context = context;
        _httpContextService = httpContextService;
    }

    public async Task<Unit> Handle(LogoutUserCommand command, CancellationToken cancellationToken) {
        var valueOfRefreshToken = _httpContextService.GetRefreshToken();

        if (!string.IsNullOrEmpty(valueOfRefreshToken)) {
            var kyuuRefreshToken = await _context.RefreshTokenTable
                .SingleOrDefaultAsync(n => n.Token == valueOfRefreshToken);

            if (kyuuRefreshToken != null) {
                kyuuRefreshToken.RevokedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        _httpContextService.DeleteRefreshToken();

        return Unit.Value;
    }
}