using System;
using Application.Common;
using Application.Repositories.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using System.Net;
using Application.Common.Exceptions;

namespace Application.Employees.Commands.ForgetPassword;

public class ForgetPasswordCommandConfirm : IRequest<ResponseMessage>
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}

public class ForgetPasswordCommandConfirmHandler : IRequestHandler<ForgetPasswordCommandConfirm, ResponseMessage>
{
    private readonly IAuth _authService;

    public ForgetPasswordCommandConfirmHandler(IAuth auth)
    {
        this._authService = auth;
    }

    public async Task<ResponseMessage> Handle(ForgetPasswordCommandConfirm request, CancellationToken cancellationToken)
    {
       ResponseMessage res = await _authService.ForgetPasswordConfirmation(forgetPasswordCommand: request);
        return new ResponseMessage
        {
            StatusCode = res.StatusCode,
            Message = res.Message
        };

    }

}

