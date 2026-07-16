using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public class UserListResponseAdmin {
    public IEnumerable<ReadUserDtoAdmin> Items {get; set;} = new List<ReadUserDtoAdmin>();
    public int TotalCount => Items.Count();
}