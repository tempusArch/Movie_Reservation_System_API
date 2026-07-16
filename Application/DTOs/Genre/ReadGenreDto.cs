namespace MovieReservationSystemAPI.Application;

public class ReadGenreDto {
    public int GenreId {get; set;}
    public string GenreName {get; set;}
    public List<ReadMovieDto> MovieRisuto {get; set;} = new();
}