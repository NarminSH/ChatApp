using System;
using Application.Common;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Application.Common.Exceptions;

namespace Application.Employees.Commands.ConfirmEmployee;
public class ConfirmEmployeeCommand : IRequest<ResponseMessage>
{
    public string Id { get; set; } = null!;
    public string Token { get; set; } = null!;
}
public class ConfirmEmployeeCommandHandler: IRequestHandler<ConfirmEmployeeCommand, ResponseMessage>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly UserManager<Employee> _userManager;

    public ConfirmEmployeeCommandHandler(IEmployeeRepository employeeRepository,UserManager<Employee> userManager)
    {
        this._employeeRepository = employeeRepository;
        this._userManager = userManager;
    }

    public async Task<ResponseMessage> Handle(ConfirmEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee employee = await _employeeRepository.GetById(request.Id);
        var result = await _userManager.ConfirmEmailAsync(employee, request.Token);
        if (result.Succeeded) {
            return new ResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Succcesfully confirmed email address"
            };
        }
        else
        {
            return new ResponseMessage { StatusCode = HttpStatusCode.BadRequest,
            Message = "Could not confirm email address"
            };
        }
    }
}


