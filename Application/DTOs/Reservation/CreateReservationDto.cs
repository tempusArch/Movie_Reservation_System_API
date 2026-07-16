using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public class CreateReservationDto {
    //public int UserId {get; set;}
    public int ShowtimeId {get; set;}

    public List<int> SeatIdRisuto {get; set;} = new();
}