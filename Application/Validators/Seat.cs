using FluentValidation;

namespace MovieReservationSystemAPI.Application;

public class CreateSeatDtoValidator : AbstractValidator<CreateSeatDto> {
    public CreateSeatDtoValidator() {
        RuleFor(x => x.Row).NotEmpty().WithMessage("Row can not be empty")
            .Matches("^[A-Z]+$").WithMessage("Row must contain only uppercase letters A-Z.");

        RuleFor(x => x.LineNumber).NotEmpty().WithMessage("LineNumber can not be empty")
            .InclusiveBetween(1, 100).WithMessage("LineNumber must be between 1 and 100");

        RuleFor(x => x.HallId).NotEmpty().GreaterThan(0).WithMessage("Hall Id must be greater than 0");
        
    }
}

public class CreateSeatCommandValidator : AbstractValidator<CreateSeatCommand> {
    public CreateSeatCommandValidator() {
        RuleFor(x => x.CreateSeatDto).NotNull().SetValidator(new CreateSeatDtoValidator());
    }
}

public class UpdateSeatDtoValidator : AbstractValidator<UpdateSeatDto> {
    public UpdateSeatDtoValidator() {
        RuleFor(x => x.Row).NotEmpty().WithMessage("Row can not be empty")
            .Matches("^[A-Z]+$").WithMessage("Row must contain only uppercase letters A-Z.");

        RuleFor(x => x.LineNumber).NotEmpty().WithMessage("LineNumber can not be empty")
            .InclusiveBetween(1, 100).WithMessage("LineNumber must be between 1 and 100");

        RuleFor(x => x.HallId).NotEmpty().GreaterThan(0).WithMessage("Hall Id must be greater than 0");
       
    }
}

public class UpdateSeatCommandValidator : AbstractValidator<UpdateSeatCommand> {
    public UpdateSeatCommandValidator() {
        RuleFor(x => x.UpdateSeatDto).NotNull().SetValidator(new UpdateSeatDtoValidator());
    }
}

public class DeleteSeatCommandValidator : AbstractValidator<DeleteSeatCommand> {
    public DeleteSeatCommandValidator() {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Seat Id must be greater than 0");

    }
}

public class GetOneSeatQueryValidator : AbstractValidator<GetOneSeatQuery> {
    public GetOneSeatQueryValidator() {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Seat Id must be greater than 0");
    }
}

public class GetOneHallsAllSeatsQueryValidator : AbstractValidator<GetOneHallsAllSeatsQuery> {
    public GetOneHallsAllSeatsQueryValidator() {
        RuleFor(x => x.HallId).NotEmpty().GreaterThan(0).WithMessage("Hall Id must be greater than 0");
    }
}

public class GetOneShowtimeAvailableSeatsQueryValidator : AbstractValidator<GetOneShowtimeAvailableSeatsQuery> {
    public GetOneShowtimeAvailableSeatsQueryValidator() {
        RuleFor(x => x.ShowtimeId).NotEmpty().GreaterThan(0).WithMessage("Showtime Id must be greater than 0");

        //RuleFor(x => x.HallId).NotEmpty().GreaterThan(0).WithMessage("Hall Id must be greater than 0");
    }
}