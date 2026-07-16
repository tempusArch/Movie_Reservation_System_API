namespace MovieReservationSystemAPI.Domain;

public class Seat {
    public int Id {get; set;}
    
    public string Row {get; set;} 
    public int LineNumber {get; set;}

    public int HallId {get; set;}
    public Hall Hall {get; set;}
}