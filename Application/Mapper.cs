using AutoMapper;
using MovieReservationSystemAPI.Domain;

namespace MovieReservationSystemAPI.Application;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<CreateMovieDto, Movie>();
        CreateMap<UpdateMovieDto, Movie>();      

        CreateMap<CreateReservationDto, Reservation>();

        CreateMap<CreateSeatDto, Seat>();
        CreateMap<UpdateSeatDto, Seat>();
     

        CreateMap<CreateShowtimeDto, Showtime>();
        CreateMap<UpdateShowtimeDto, Showtime>();

        CreateMap<RegisterUserDto, User>();
    }
}