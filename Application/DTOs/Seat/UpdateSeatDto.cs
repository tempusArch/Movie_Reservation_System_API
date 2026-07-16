namespace MovieReservationSystemAPI.Application;

public class UpdateSeatDto {
    //public int Id {get; set;}
    
    public string Row {get; set;} 
    public int LineNumber {get; set;}

    public int HallId {get; set;}
}