using System;
using Application.Employees.Commands.CreateEmployee;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.Common.Exceptions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Application.Common;
using System.Net;
using Application.Repositories.Abstraction;

namespace Application.Employees.Commands.LoginEmployee;
public class LoginEmployeeCommand: IRequest<ResponseMessage>, IMapFrom<Employee>
{
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}

public class LoginEmployeeCommandHandler : IRequestHandler<LoginEmployeeCommand, ResponseMessage>
{
    private readonly IMapper _mapper;
    private readonly IAuth _authService;  

    public LoginEmployeeCommandHandler(UserManager<Employee> userManager, IMapper mapper,
        IConfiguration config, SignInManager<Employee> signInManager,
        IAuth auth)
    {
        this._mapper = mapper;
        this._authService = auth;
    }

    public async Task<ResponseMessage> Handle(LoginEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Employee>(request);
        ResponseMessage res = await _authService.LoginEmployee(entity);
        return new ResponseMessage
        {
            StatusCode = res.StatusCode,
            Message = res.Message,
            Data = res.Data
        };
        }
    }


