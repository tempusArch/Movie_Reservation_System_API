using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public class SeatListResponseUser {
    public IEnumerable<ReadSeatDto> Items {get; set;} = new List<ReadSeatDto>();
    public int TotalCount => Items.Count();
}

