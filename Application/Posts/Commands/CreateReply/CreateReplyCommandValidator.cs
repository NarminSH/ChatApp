using System;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Posts.Commands.CreateReply
{
    public class CreateReplyCommandValidator: AbstractValidator<CreateReplyCommand>
    {
        private readonly UserManager<Employee> _userManager;
        public CreateReplyCommandValidator(UserManager<Employee> userManager)
        {
            this._userManager = userManager;

            RuleFor(v => v.PostId).NotEmpty().WithMessage("Post Id is required");
            RuleFor(v => v.Message).NotEmpty().WithMessage("Message is required");
            RuleFor(v => v.EmployeeId).NotEmpty().WithMessage("Employee is required")
                .Must(IfUserExistsInDb).WithMessage("Employee does not exist");
        }
        private bool IfUserExistsInDb(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (user != null) return true;
            else return false;
        }
    }
}

