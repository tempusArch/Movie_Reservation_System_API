namespace MovieReservationSystemAPI.Domain;

public class Reservation {
    public int Id {get; set;}

    public int UserId {get; set;}
    public User User {get; set;}

    public int ShowtimeId {get; set;}
    public Showtime Showtime {get; set;}


    public int SeatQuantity => ReservedSeatRisuto.Count();
    public List<ReservedSeat> ReservedSeatRisuto {get; set;} = new();

    
    public decimal TotalPrice => SeatQuantity * Showtime.Price;
    public ReservationStatus ReservationStatus {get; set;} = ReservationStatus.Processing;

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
}