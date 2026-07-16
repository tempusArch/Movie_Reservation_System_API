namespace MovieReservationSystemAPI.Application;

public class MovieListResponse {
    public IEnumerable<ReadMovieDto> Items {get; set;} = new List<ReadMovieDto>();
    public int TotalCount => Items.Count();
}