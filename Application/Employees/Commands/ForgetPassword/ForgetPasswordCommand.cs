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
    private readonly IEmployeeRepository _employeeRepository;
    private readonly UserManager<Employee> _userManager;
    private readonly IHttpContextAccessor _httpContext;
    private readonly LinkGenerator _linkGenerator;
    private readonly IEmailSender _mailService;

    public ForgetPasswordCommandHandler(IEmployeeRepository employeeRepository,
        UserManager<Employee> userManager, IHttpContextAccessor httpContext,
        LinkGenerator generator, IEmailSender emailSender)
    {
        this._employeeRepository = employeeRepository;
        this._userManager = userManager;
        this._httpContext = httpContext;
        this._linkGenerator = generator;
        this._mailService = emailSender;
    }

    public async Task<ResponseMessage> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var existedUser = await _userManager.FindByEmailAsync(request.Email);
        if (existedUser != null)
        {
            string token = await _userManager.GeneratePasswordResetTokenAsync(existedUser);
            Console.WriteLine(token);
            Console.WriteLine("...............");
            string url = _linkGenerator.GetUriByAction(_httpContext.HttpContext,
            action: "ForgetPassword", controller: "Employees", values: new { token, existedUser.Email });
            string message = "Please click to reset password " + url;
            await _mailService.SendEmailAsync(existedUser.Email, "Password Recovery", message);
            return new ResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Please check your email"

            };
        } else { throw new NotFoundException(); }
        
    }
}


