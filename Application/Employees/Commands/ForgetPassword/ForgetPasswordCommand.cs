using System;
using Application.Common;
using System.Net;
using Application.Common.Exceptions;
using Application.Repositories.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace Application.Employees.Commands.ForgetPassword;
public class ForgetPasswordCommand: IRequest<ResponseMessage>
{
    public string Email { get; set; } = null!;
}

public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ResponseMessage>
{
    private readonly IAuth _authService;
    public ForgetPasswordCommandHandler(IAuth auth)
    {
        this._authService = auth;
    }

    public async Task<ResponseMessage> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        ResponseMessage res = await  _authService.ForgetPassword(request.Email);
        return new ResponseMessage
        {
            StatusCode = res.StatusCode,
            Message = res.Message
        };
        

    }
}


