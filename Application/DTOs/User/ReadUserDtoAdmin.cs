namespace MovieReservationSystemAPI.Application;

public class ReadUserDtoAdmin {
    public int UserId {get; set;}

    public string UserName {get; set;}
    public string Email {get; set;}
   
    public string Role {get; set;} 

    public List<ReadReservationDtoAdmin> ReservationRisuto {get; set;} = new();
}