using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public interface IJwtService {
    string Generate_JWT(User um);
    RefreshToken Generate_RefreshToken(string UserId);
}