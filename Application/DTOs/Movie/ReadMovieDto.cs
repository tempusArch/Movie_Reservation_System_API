namespace MovieReservationSystemAPI.Application;

public class ReadMovieDto {
    public int MovieId {get; set;}

    public string Title {get; set;}
    public string Description {get; set;}
    public TimeSpan Duration {get; set;}

    public List<ReadGenreDto> GenreRisuto {get; set;} = new();
    public List<ReadShowtimeDto> ShowtimeRisuto {get; set;} = new();

}