using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public class ReadReservationDtoAdmin {
    public int ReservationId {get; set;}

    public int UserId {get; set;}
    public int ShowtimeId {get; set;}
    public int SeatQuantity {get; set;}
    
    public string MovieName {get; set;}
    public DateTime StartTime {get; set;}
    public TimeSpan Duration {get; set;}

    public decimal TotalPrice;
    public ReservationStatus ReservationStatus {get; set;}
   
    public string HallName {get; set;}
    public List<string> SeatLabelRisuto {get; set;}

    public DateTime CreatedAt {get; set;}


}