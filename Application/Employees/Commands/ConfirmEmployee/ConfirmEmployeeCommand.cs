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
    private readonly IMapper _mapper;

    public ConfirmEmployeeCommandHandler(IEmployeeRepository employeeRepository,
        IMapper mapper, UserManager<Employee> userManager)
    {
        this._employeeRepository = employeeRepository;
        this._mapper = mapper;
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
                return "There was a problem confirming email";
            }
        }
        else
        {
            return "Could not find user";
        }
    }
}


