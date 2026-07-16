using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public class ReservationListResponseAdmin {
    public IEnumerable<ReadReservationDtoAdmin> Items {get; set;} = new List<ReadReservationDtoAdmin>();
    public int TotalCount {get; set;}
    public decimal TotalPrice {get; set;}
}