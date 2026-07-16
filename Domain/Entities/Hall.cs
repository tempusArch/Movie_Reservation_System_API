namespace MovieReservationSystemAPI.Domain;

public class Hall {
    public int Id {get; set;}
    public string Name {get; set;}

    public List<Seat> SeatRisuto {get; set;} = new();
}