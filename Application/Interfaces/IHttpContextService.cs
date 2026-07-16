using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public interface IHttpContextService {
    string GetCurrentUserEmail();
    void CheckUserIdClaim();
    int GetCurrentUserId();
    string? GetRefreshToken();
    void SetRefreshToken(RefreshToken newRefreshToken);
    void DeleteRefreshToken();
}