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


    } 
    

}





