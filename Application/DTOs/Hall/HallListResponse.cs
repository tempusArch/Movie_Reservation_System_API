namespace MovieReservationSystemAPI.Application;

public class HallListResponse {
    public IEnumerable<ReadHallDto> Items {get; set;} = new List<ReadHallDto>();
    public int TotalCount => Items.Count();
}