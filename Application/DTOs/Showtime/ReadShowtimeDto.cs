namespace MovieReservationSystemAPI.Application;

public class ReadShowtimeDto {
    public int ShowtimeId {get; set;}

    public DateTime StartTime {get; set;}
    public TimeSpan Duration {get; set;}
    public DateTime EndTime {get; set;}
    
    public decimal Price {get; set;}

    public int HallId {get; set;}
    public string HallName {get; set;}
    
    public int MovieId {get; set;}
    public string MovieName {get; set;}

}