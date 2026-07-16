namespace MovieReservationSystemAPI.Application;

public class ShowtimeListResponse {
    public IEnumerable<ReadShowtimeDto> Items {get; set;} = new List<ReadShowtimeDto>();
    public int TotalCount => Items.Count();
}