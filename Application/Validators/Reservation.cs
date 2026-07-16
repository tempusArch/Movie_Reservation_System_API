using FluentValidation;

namespace MovieReservationSystemAPI.Application;

public class CreateReservationDtoValidator : AbstractValidator<CreateReservationDto> {
    public CreateReservationDtoValidator() {       
        //RuleFor(x => x.UserId).NotEmpty().GreaterThan(0).WithMessage("User Id must be greater than 0");

        RuleFor(x => x.ShowtimeId).NotEmpty().GreaterThan(0).WithMessage("Showtime Id must be greater than 0");

        RuleFor(x => x.SeatIdRisuto).NotEmpty().WithMessage("Seat Id can not be empty");
     
    }
}

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand> {
    public CreateReservationCommandValidator() {
        RuleFor(x => x.CreateReservationDto).NotNull().SetValidator(new CreateReservationDtoValidator());
    }
}


public class CancelReservationCommandValidator : AbstractValidator<CancelReservationCommand> {
    public CancelReservationCommandValidator() {
        RuleFor(x => x.UserId).NotEmpty().GreaterThan(0).WithMessage("User Id must be greater than 0");

        RuleFor(x => x.ReservationId).NotEmpty().GreaterThan(0).WithMessage("Reservation Id must be greater than 0");
    }
}

public class GetOneReservationUserQueryValidator : AbstractValidator<GetOneReservationUserQuery> {
    public GetOneReservationUserQueryValidator() {
        RuleFor(x => x.UserId).NotEmpty().GreaterThan(0).WithMessage("User Id must be greater than 0");

        RuleFor(x => x.ReservationId).NotEmpty().GreaterThan(0).WithMessage("Reservation Id must be greater than 0");
    }
}

public class GetAllReservationsUserQueryValidator : AbstractValidator<GetAllReservationsUserQuery> {
    public GetAllReservationsUserQueryValidator() { 
        RuleFor(x => x.UserId).NotEmpty().GreaterThan(0).WithMessage("User Id must be greater than 0");

    }
}

public class GetOneReservationAdminQueryValidator : AbstractValidator<GetOneReservationAdminQuery> {
    public GetOneReservationAdminQueryValidator() {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Reservation Id must be greater than 0");
    }
}

/*public class ReservationAdminQueryDtoValidator : AbstractValidator<ReservationAdminQueryDto> {
    public ReservationAdminQueryDtoValidator() {
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("User Id must be greater than 0");

        RuleFor(x => x.ShowtimeId).GreaterThan(0).WithMessage("Showtime Id must be greater than 0");

        RuleFor(x => x.MovieId).NotEmpty().GreaterThan(0).WithMessage("Movie Id must be greater than 0");
        
        RuleFor(x => x.HallId).NotEmpty().GreaterThan(0).WithMessage("Hall Id must be greater than 0");
        
    }
}

public class GetManyReservationsAdminQueryValidator : AbstractValidator<GetManyReservationsAdminQuery> {
    public GetManyReservationsAdminQueryValidator() { 
        //RuleFor(x => x.ReservationAdminQueryDto).NotNull().SetValidator(new ReservationAdminQueryDtoValidator());

    }
}*/
