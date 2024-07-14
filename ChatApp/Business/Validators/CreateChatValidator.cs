using Business.DTOs;
using Data.UnitOfWork;
using FluentValidation;

namespace Business.Validators;

public class CreateChatValidator : AbstractValidator<CreateChatDto>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateChatValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(x => x.Title)
            .NotEmpty()
            .Length(1, 100)
            .Matches(@"^[A-Za-z0-9\s-]*$")
            .WithMessage("Chat name must be at least 1 character long and no longer than 100 characters. You may use Latin letters only. Digits, special symbols, hyphens and spaces are allowed")
            .Must(title => !IsChatExistInDb(title))
            .WithMessage("A chat with the same title already exists.");

        RuleFor(x => x.CreatedById)
            .NotNull()
            .WithMessage("Id of User is required.")
            .Must(userId => UserExistInDb(userId))
            .WithMessage("User don't exist in database.");
    }
    
    private bool UserExistInDb(Guid id)
    {
        var user = _unitOfWork.Users.GetByIdAsync(id).Result;

        return user is not null;
    }

    private bool IsChatExistInDb(string title)
    {
        return _unitOfWork.Chats.GetByTitle(title) is not null;
    }
}