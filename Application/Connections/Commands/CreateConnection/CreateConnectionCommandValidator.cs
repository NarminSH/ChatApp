using System;
using Application.Employees.Commands.CreateEmployee;
using Application.Repositories.Abstraction;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Connections.Commands.CreateConnection;

public class CreateConnectionCommandValidator : AbstractValidator<CreateConnectionCommand>
{
    private readonly IConnectionRepository _connection;
    private readonly UserManager<Employee> _userManager;
    public CreateConnectionCommandValidator(IConnectionRepository connectionRepository,
        UserManager<Employee> userManager)
    {
        this._connection = connectionRepository;
        this._userManager = userManager;
        RuleFor(c => c.Name).NotEmpty().WithMessage("Connection name is required").
        MaximumLength(60).WithMessage("Name must not exceed 60 characters");

        RuleFor(v => v.ReceiverId).NotEmpty().WithMessage("Receiver id is required")
            .Must(IfUserExistsInDb).WithMessage("Receiver with provided id does not exist");

        RuleFor(v => v.SenderId).NotEmpty().WithMessage("Sender id is required")
            .Must(IfUserExistsInDb).WithMessage("Sender with provided id does not exist");

    } 
    private bool IfUserExistsInDb(string id)
    {
        var user = _userManager.Users.Where(u => u.Id == id).First();
        if (user != null) return true;
        else return false;
    }

}





