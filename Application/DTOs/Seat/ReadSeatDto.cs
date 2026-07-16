namespace MovieReservationSystemAPI.Application;

public class ReadSeatDto {
    public int SeatId {get; set;}
    
    public string HallName {get; set;}
    public string Label {get; set;}
    
    public bool IsAvailable {get; set;}
}

public class ReadSeatDtoAdmin {
    public int SeatId {get; set;}
    
    public string Row {get; set;} 
    public int LineNumber {get; set;}

    public int HallId {get; set;}
    public string HallName {get; set;}
}