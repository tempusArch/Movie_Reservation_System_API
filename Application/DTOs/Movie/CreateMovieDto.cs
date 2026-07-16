namespace MovieReservationSystemAPI.Application;

public class CreateMovieDto {
    public string Title {get; set;}
    public string Description {get; set;}
    public TimeSpan Duration {get; set;}

    public List<int> GenreIdRisuto {get; set;} = new();
    //public List<int> ShowtimeIdRisuto {get; set;} = new();
}