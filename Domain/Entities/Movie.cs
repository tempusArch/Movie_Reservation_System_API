namespace MovieReservationSystemAPI.Domain;

public class Movie {
    public int Id {get; set;}
    public string Title {get; set;}
    public string Description {get; set;}
    public TimeSpan Duration {get; set;}

    public List<Genre> GenreRisuto {get; set;} = new();
    public List<Showtime> ShowtimeRisuto {get; set;} = new();
}