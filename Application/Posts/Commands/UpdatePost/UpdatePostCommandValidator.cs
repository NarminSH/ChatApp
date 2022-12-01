using System;
using Application.Connections.Commands.UpdateConnection;
using Application.Repositories.Abstraction;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IPostRepository post;

        public UpdatePostCommandValidator(UserManager<Employee> userManager)
        {
            this._userManager = userManager;

            RuleFor(c => c.Message).NotEmpty().WithMessage("Message field is required");

            RuleFor(v => v.EmployeeId).NotEmpty().WithMessage("Sender id is required").
                Must(IfUserExistsInDb).WithMessage("Sender with provided id does not exist");

        }


        private bool IfUserExistsInDb(string id)
        {
            var user = _userManager.Users.FirstOrDefault(c => c.Id == id);
            if (user != null) return true;
            else return false;
        }

    }
}

