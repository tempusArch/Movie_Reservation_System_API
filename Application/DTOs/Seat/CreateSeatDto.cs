namespace MovieReservationSystemAPI.Application;

public class CreateSeatDto {
    public string Row {get; set;} 
    public int LineNumber {get; set;}

    public int HallId {get; set;}
}