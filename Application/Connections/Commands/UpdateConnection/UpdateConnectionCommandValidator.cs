using System;
using Application.Connections.Commands.CreateConnection;
using Application.Repositories.Abstraction;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Connections.Commands.UpdateConnection;

public class UpdateConnectionCommandValidator : AbstractValidator<UpdateConnectionCommand>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly UserManager<Employee> _userManager;

    public UpdateConnectionCommandValidator(IConnectionRepository connectionRepository,
        UserManager<Employee> userManager)
    {
        this._connectionRepository = connectionRepository;
        this._userManager = userManager;
       
        RuleFor(c => c.Name).NotEmpty().WithMessage("Connection name is required").
        MaximumLength(60).WithMessage("Name must not exceed 60 characters");

        //RuleFor(v => v.ReceiverId).NotEmpty().WithMessage("Receiver id is required")
        //    .Must(IfUserExistsInDb).WithMessage("Receiver with provided id does not exist");

        //RuleFor(v => v.SenderId).NotEmpty().WithMessage("Sender id is required").
        //    Must(IfUserExistsInDb).WithMessage("Sender with provided id does not exist");

        //RuleFor(c => c.IsChannel).NotEmpty().WithMessage("IsChannel field is required");
        
        //RuleFor(c => c.IsPrivate).NotEmpty().WithMessage("IsPrivate field is required");

    }

    private bool IfUserExistsInDb(string id)
    {
        var user = _userManager.Users.FirstOrDefault(c => c.Id == id);
        if (user != null) return true;
        else return false;
    }

}