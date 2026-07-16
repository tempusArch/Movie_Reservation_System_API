namespace MovieReservationSystemAPI.Application;

public class GenreListResponse {
    public IEnumerable<ReadGenreDto> Items {get; set;} = new List<ReadGenreDto>();
    public int TotalCount => Items.Count();
}