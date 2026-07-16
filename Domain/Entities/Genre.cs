namespace MovieReservationSystemAPI.Domain;

public class Genre {
    public int Id {get; set;}
    public string Name {get; set;}

    public List<Movie> MovieRisuto {get; set;} = new();
}