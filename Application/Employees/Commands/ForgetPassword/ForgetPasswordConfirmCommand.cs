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
    private readonly IEmployeeRepository _employeeRepository;
    private readonly UserManager<Employee> _userManager;

    public ForgetPasswordCommandConfirmHandler(IEmployeeRepository employeeRepository,
        UserManager<Employee> userManager)
    {
        this._employeeRepository = employeeRepository;
        this._userManager = userManager;

    }

    public async Task<ResponseMessage> Handle(ForgetPasswordCommandConfirm request, CancellationToken cancellationToken)
    {
        var existedUser = await _userManager.FindByEmailAsync(request.Email);
        if (existedUser!= null)
        {
            var result = await _userManager.ResetPasswordAsync(existedUser, request.Token,
                request.NewPassword);
            if (result.Succeeded) { return new ResponseMessage {
                StatusCode = HttpStatusCode.OK,
            Message = "Successfully changed password"
            };
            }
            else { return new ResponseMessage {StatusCode = HttpStatusCode.BadRequest,
            Message = "There was a problem changing password"
            };
            }
        }
        else { throw new NotFoundException(); }
    }

}

