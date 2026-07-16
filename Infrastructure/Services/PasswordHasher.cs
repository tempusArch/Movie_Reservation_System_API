using MovieReservationSystemAPI.Application;

namespace MovieReservationSystemAPI.Infrastructure;

public class PasswordHasher : IPasswordHasher {
    public string HashPassword(string m) {
        return BCrypt.Net.BCrypt.HashPassword(m);
    }

    public bool VerifyPassword(string enteredPassword, string hashedPassword) {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
    }
}