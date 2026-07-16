using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public class SeatListResponseAdmin {
    public IEnumerable<ReadSeatDtoAdmin> Items {get; set;} = new List<ReadSeatDtoAdmin>();
    public int TotalCount => Items.Count();
}