using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public class ReservationAdminQueryDto {
    public int? UserId {get; set;}
    public int? ShowtimeId {get; set;}
    public int? MovieId {get; set;}
    public int? HallId {get; set;}

    public ReservationStatus? ReservationStatus {get; set;}
    
    public DateTime? Start {get; set;}
    public DateTime? End {get; set;}
}