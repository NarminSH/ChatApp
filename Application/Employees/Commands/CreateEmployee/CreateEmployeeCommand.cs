using System;
using System.Xml.Linq;
using Application.Common;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Application.Employees.Commands.CreateEmployee;
public class CreateEmployeeCommand : IRequest<ResponseMessage>, IMapFrom<Employee>
{
    public string Fullname { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ResponseMessage>
{
    private readonly IMapper _mapper;
    private readonly IAuth _authservice;

    public CreateEmployeeCommandHandler(IMapper mapper, IAuth service)
    {
        this._mapper = mapper;
        this._authservice = service;
    }

    public async Task<ResponseMessage> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Employee>(request);
        var result = await _authservice.CreateEmployee(entity);
        return new ResponseMessage { StatusCode = System.Net.HttpStatusCode.OK,
            Message = "Successfully created the user! Please check your email to validate user",
        Data = result};
    }

}



