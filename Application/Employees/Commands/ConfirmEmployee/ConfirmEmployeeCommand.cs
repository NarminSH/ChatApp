using System;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Employees.Commands.ConfirmEmployee;
public class ConfirmEmployeeCommand : IRequest<string>
{
    public string Id { get; set; } = null!;
    public string Token { get; set; } = null!;
}
public class ConfirmEmployeeCommandHandler: IRequestHandler<ConfirmEmployeeCommand, string>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly UserManager<Employee> _userManager;

    public ConfirmEmployeeCommandHandler(IEmployeeRepository employeeRepository,UserManager<Employee> userManager)
    {
        this._employeeRepository = employeeRepository;
        this._userManager = userManager;
    }

    public async Task<string> Handle(ConfirmEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeeRepository.GetById(request.Id);
        if (employee != null)
        {
            var result = await _userManager.ConfirmEmailAsync(employee, request.Token);
            if (result.Succeeded) { return "Succcesfully confirmed email address"; }
            else
            {
                return "Could not confirm email address";
            }
        }
        else
        {
            return "Could not find user";
        }
    }
}


