using Business.DTOs;
using Data.UnitOfWork;
using FluentValidation;

namespace Business.Validators;

public class AddMessageValidator : AbstractValidator<AddMessageDto>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public AddMessageValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        
        RuleFor(x => x.Text)
            .NotEmpty()
            .Length(1, 1024)
            .Matches(@"^[A-Za-z0-9\s-]*$")
            .WithMessage("Message text must be between 1 and 1024 characters long, start with a letter, and only contain Latin letters, spaces, and hyphens.");
        
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("Id of User is required.")
            .Must(userId => UserExistInDb(userId))
            .WithMessage("User don't exist in database.");
        
        RuleFor(x => x.ChatId)
            .NotNull()
            .WithMessage("Id of Chat is required.")
            .Must(chatId => ChatExistInDb(chatId))
            .WithMessage("Chat don't exist in database.");
    }
    
    private bool UserExistInDb(Guid id)
    {
        var user = _unitOfWork.Users.GetByIdAsync(id).Result;

        return user is not null;
    }
    
    private bool ChatExistInDb(Guid id)
    {
        var chat = _unitOfWork.Chats.GetByIdAsync(id).Result;

        return chat is not null;
    }
}