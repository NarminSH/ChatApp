using System;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    private readonly UserManager<Employee> _userManager;
    public CreateEmployeeCommandValidator(UserManager<Employee> userManager)
    {
        this._userManager = userManager;

        RuleFor(v => v.Email).NotEmpty().WithMessage("Email is required");

        RuleFor(v => v.Fullname).NotEmpty().WithMessage("Fullname is required").
            MaximumLength(90).WithMessage("Fullname must not exceed 90 characters");
    }
}

