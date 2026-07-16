using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public class ReadHallDto {
    public int HallId {get; set;}
    public string HallName {get; set;}

    public List<ReadSeatDtoAdmin> SeatRisuto {get; set;} = new();
}