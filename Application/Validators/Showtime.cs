using FluentValidation;

namespace MovieReservationSystemAPI.Application;

public class CreateShowtimeDtoValidator : AbstractValidator<CreateShowtimeDto> {
    public CreateShowtimeDtoValidator() {
        RuleFor(x => x.StartTime).NotEmpty().WithMessage("StartTime can not be empty")
            .Must(date => date > DateTime.UtcNow)
            .WithMessage("Start time must be in the future.");

        RuleFor(cp => cp.Price).NotEmpty().GreaterThan(0).WithMessage("Price must be greater than 0")
            .PrecisionScale(10, 2, false).WithMessage("Price have a maximum of 10 digits and 2 decimal places.");

        RuleFor(x => x.HallId).NotEmpty().GreaterThan(0).WithMessage("Hall Id must be greater than 0");

        RuleFor(x => x.MovieId).NotEmpty().GreaterThan(0).WithMessage("Movie Id must be greater than 0");
        
    }
}

public class CreateShowtimeCommandValidator : AbstractValidator<CreateShowtimeCommand> {
    public CreateShowtimeCommandValidator() {
        RuleFor(x => x.CreateShowtimeDto).NotNull().SetValidator(new CreateShowtimeDtoValidator());
    }
}

public class UpdateShowtimeDtoValidator : AbstractValidator<UpdateShowtimeDto> {
    public UpdateShowtimeDtoValidator() {

        RuleFor(x => x.StartTime).NotEmpty().WithMessage("StartTime can not be empty")
            .Must(date => date > DateTime.UtcNow)
            .WithMessage("Start time must be in the future.");

        RuleFor(cp => cp.Price).NotEmpty().GreaterThan(0).WithMessage("Price must be greater than 0")
            .PrecisionScale(10, 2, false).WithMessage("Price have a maximum of 10 digits and 2 decimal places.");

        //RuleFor(x => x.HallId).NotEmpty().GreaterThan(0).WithMessage("Hall Id must be greater than 0");

        RuleFor(x => x.MovieId).NotEmpty().GreaterThan(0).WithMessage("Movie Id must be greater than 0");


        
    }
}

public class UpdateShowtimeCommandValidator : AbstractValidator<UpdateShowtimeCommand> {
    public UpdateShowtimeCommandValidator() {
        RuleFor(x => x.UpdateShowtimeDto).NotNull().SetValidator(new UpdateShowtimeDtoValidator());
    }
}

public class DeleteShowtimeCommandValidator : AbstractValidator<DeleteShowtimeCommand> {
    public DeleteShowtimeCommandValidator() {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Movie Id must be greater than 0");

    }
}

public class GetOneShowtimeQueryValidator : AbstractValidator<GetOneShowtimeQuery> {
    public GetOneShowtimeQueryValidator() {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Showtime Id must be greater than 0");
    }
}

public class GetManyShowtimesQueryValidator : AbstractValidator<GetManyShowtimesQuery> {
    public GetManyShowtimesQueryValidator() { 
        RuleFor(x => x.HallId).GreaterThan(0).WithMessage("Hall Id must be greater than 0");

        RuleFor(x => x.MovieId).GreaterThan(0).WithMessage("Movie Id must be greater than 0");

        RuleFor(x => x.DateOnly).GreaterThan(new DateOnly(1888, 10, 14)).WithMessage("Date must be after October 14, 1888.");

        RuleFor(x => x.Page).NotEmpty().GreaterThan(0).WithMessage("Page must be greater than 0");

        RuleFor(x => x.Limit).NotEmpty().GreaterThan(0).WithMessage("Limit Id must be greater than 0");


    }
}