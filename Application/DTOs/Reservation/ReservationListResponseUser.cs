using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public class ReservationListResponseUser {
    public IEnumerable<ReadReservationDtoUser> Items {get; set;} = new List<ReadReservationDtoUser>();
    public int TotalCount => Items.Count();
}

