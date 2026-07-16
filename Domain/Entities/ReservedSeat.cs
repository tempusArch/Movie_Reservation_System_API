namespace MovieReservationSystemAPI.Domain;

public class ReservedSeat {
    public int SeatId {get; set;}
    public Seat Seat {get; set;}

    public int ReservationId {get; set;}
    public Reservation Reservation {get; set;}

    public int ShowtimeId {get; set;}
    public int HallId {get; set;}
}