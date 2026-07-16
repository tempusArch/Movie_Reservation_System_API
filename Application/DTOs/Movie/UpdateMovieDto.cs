namespace MovieReservationSystemAPI.Application;

public class UpdateMovieDto {
    //public int Id {get; set;}
    
    public string Title {get; set;}
    public string Description {get; set;}
    public TimeSpan Duration {get; set;}

    public List<int> GenreIdRisuto {get; set;} = new();
    //public List<int> ShowtimeIdRisuto {get; set;} = new();
}