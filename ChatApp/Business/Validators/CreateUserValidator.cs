using Business.DTOs;
using Data.UnitOfWork;
using FluentValidation;

namespace Business.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateUserValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Login)
            .NotEmpty()
            .Length(3, 32)
            .Matches(@"^[A-Za-z0-9\s-]*$")
            .Must(login => !IsUserExistInDb(login))
            .WithMessage("A user with the same login already exists.");
    }

    public bool IsUserExistInDb(string login)
    {
        return _unitOfWork.Users.GetByLogin(login).Result is not null;
    }
}