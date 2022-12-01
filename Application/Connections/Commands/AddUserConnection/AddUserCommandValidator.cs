using System;
using Application.Connections.Commands.CreateConnection;
using Application.Repositories.Abstraction;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Connections.Commands.AddUserConnection
{
    public class AddUserCommandValidator : AbstractValidator<AddUserToConnectionCommand>
    {
        private readonly IConnectionRepository _connection;
        private readonly UserManager<Employee> _userManager;
        public AddUserCommandValidator(IConnectionRepository connectionRepository,
            UserManager<Employee> userManager)
        {
            this._connection = connectionRepository;
            this._userManager = userManager;
            RuleFor(v => v.EmployeeId).NotEmpty().WithMessage("Employee id is required")
                .Must(IfUserExistsInDb).WithMessage("Employee with provided id does not exist");
        }
        private bool IfUserExistsInDb(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (user != null) return true;
            else return false;
        }
    }
}

