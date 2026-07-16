using System.Data;
using FluentValidation;

namespace MovieReservationSystemAPI.Application;

public class CreateHallCommandValidator : AbstractValidator<CreateHallCommand> {
    public CreateHallCommandValidator() {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Hall name can not be empty");
    }
}

public class UpdateHallCommandValidator : AbstractValidator<UpdateHallCommand> {
    public UpdateHallCommandValidator() {
        RuleFor(x => x.HallName).NotEmpty().WithMessage("Hall name can not be empty");
    
        RuleFor(x => x.HallId).NotEmpty().GreaterThan(0).WithMessage("Hall Id must be greater than 0");

    }
}

public class DeleteHallCommandValidator : AbstractValidator<DeleteHallCommand> {
    public DeleteHallCommandValidator() {   
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Hall Id must be greater than 0");

    }
}

public class GetOneHallQueryValidator : AbstractValidator<GetOneHallQuery> {
    public GetOneHallQueryValidator() {   
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0).WithMessage("Hall Id must be greater than 0");

    }
}

public class GetAllHallsQueryValidator : AbstractValidator<GetAllHallsQuery> {
    public GetAllHallsQueryValidator() {  
        RuleFor(x => x.Page).NotEmpty().GreaterThan(0).WithMessage("Page must be greater than 0");

        RuleFor(x => x.Limit).NotEmpty().GreaterThan(0).WithMessage("Limit Id must be greater than 0");


    }
}