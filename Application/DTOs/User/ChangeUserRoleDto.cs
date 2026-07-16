using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public class ChangeUserRoleDto {
    public int TargetUserId {get; set;}
    public UserRole UserRole {get; set;}
}