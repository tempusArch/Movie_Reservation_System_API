using FluentValidation;

namespace MovieReservationSystemAPI.Application;

public class CreateMovieDtoValidator : AbstractValidator<CreateMovieDto> {
    public CreateMovieDtoValidator() {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Movie title can not be empty")
            .Length(1, 50).WithMessage("Movie title must be between 1 and 50 characters");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Description can not be empty")
            .MaximumLength(600).WithMessage("Movie description can not exceed 600 characters");

        RuleFor(x => x.Duration).NotEmpty().WithMessage("Duration can not be empty")
            .Must(d => 
                d > TimeSpan.FromMinutes(30) &&
                d < TimeSpan.FromHours(5))
            .WithMessage("Duration must be longer than 30 minutes and less than 5 hours");

        RuleFor(x => x.GenreIdRisuto).NotEmpty().WithMessage("Genre Id can not be empty");

        //RuleFor(x => x.ShowtimeIdRisuto).NotEmpty().WithMessage("Showtime Id can not be empty");

        
    }
}

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand> {
    public CreateMovieCommandValidator() {
        RuleFor(x => x.CreateMovieDto).NotNull().SetValidator(new CreateMovieDtoValidator());
    }
}

public class UpdateMovieDtoValidator : AbstractValidator<UpdateMovieDto> {
    public UpdateMovieDtoValidator() {

        RuleFor(x => x.Title).NotEmpty().WithMessage("Movie title can not be empty")
            .Length(1, 50).WithMessage("Movie title must be between 1 and 50 characters");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Description can not be empty")
            .MaximumLength(600).WithMessage("Movie description can not exceed 600 characters");

        RuleFor(x => x.Duration).NotEmpty().WithMessage("Duration can not be empty")
            .Must(d => 
                d > TimeSpan.FromMinutes(30) &&
                d < TimeSpan.FromHours(5))
            .WithMessage("Duration must be longer than 30 minutes and less than 5 hours");

        RuleFor(x => x.GenreIdRisuto).NotEmpty().WithMessage("Genre Id can not be empty");

        //RuleFor(x => x.ShowtimeIdRisuto).NotEmpty().WithMessage("Showtime Id can not be empty");


        
    }
}

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand> {
    public UpdateMovieCommandValidator() {
        RuleFor(x => x.UpdateMovieDto).NotNull().SetValidator(new UpdateMovieDtoValidator());
    }
}

public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand> {
    public DeleteMovieCommandValidator() {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Movie Id must be greater than 0");

    }
}

public class GetOneMovieQueryValidator : AbstractValidator<GetOneMovieQuery> {
    public GetOneMovieQueryValidator() {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Movie Id must be greater than 0");
    }
}

public class GetManyMoviesQueryValidator : AbstractValidator<GetManyMoviesQuery> {
    public GetManyMoviesQueryValidator() {  
        RuleFor(x => x.Page).NotEmpty().GreaterThan(0).WithMessage("Page must be greater than 0");

        RuleFor(x => x.Limit).NotEmpty().GreaterThan(0).WithMessage("Limit Id must be greater than 0");


    }
}