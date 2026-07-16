using System.Data;
using FluentValidation;

namespace MovieReservationSystemAPI.Application;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand> {
    public CreateGenreCommandValidator() {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Genre name can not be empty");
    }
}

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand> {
    public UpdateGenreCommandValidator() {
        RuleFor(x => x.GenreName).NotEmpty().WithMessage("Genre name can not be empty");
    
        RuleFor(x => x.GenreId).NotEmpty().GreaterThan(0).WithMessage("Genre Id must be greater than 0");

    }
}

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand> {
    public DeleteGenreCommandValidator() {   
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Genre Id must be greater than 0");

    }
}

public class GetOneGenreQueryValidator : AbstractValidator<GetOneGenreQuery> {
    public GetOneGenreQueryValidator() {   
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Genre Id must be greater than 0");

    }
}

public class GetManyGenresQueryValidator : AbstractValidator<GetManyGenresQuery> {
    public GetManyGenresQueryValidator() {  
        RuleFor(x => x.Page).NotEmpty().GreaterThan(0).WithMessage("Page must be greater than 0");

        RuleFor(x => x.Limit).NotEmpty().GreaterThan(0).WithMessage("Limit Id must be greater than 0");


    }
}