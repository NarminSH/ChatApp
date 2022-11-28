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
    private readonly UserManager<Employee> _userManager;
    private readonly IMapper _mapper;
    private readonly IEmailSender _mailService;
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContext;

    public CreateEmployeeCommandHandler(UserManager<Employee> userManager, IMapper mapper,
        IEmailSender emailSender, LinkGenerator linkGenerator, IHttpContextAccessor http)
    {
        this._mapper = mapper;
        this._userManager = userManager;
        this._mailService = emailSender; 
        this._linkGenerator = linkGenerator;
        this._httpContext = http;
    }

    public async Task<ResponseMessage> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Employee>(request);
        IdentityResult result = await _userManager.CreateAsync(entity, entity.PasswordHash);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(entity);
        Console.WriteLine(".................");
        Console.WriteLine("Register confirmation token is: " +token);
        Console.WriteLine(".................");
        string url = _linkGenerator.GetUriByAction( _httpContext.HttpContext,
            action: "ConfirmEmail", controller: "Employees", values:  new { token, entity.Id });
        string message = "Please confirm your email by clicking here " + url;
        await _mailService.SendEmailAsync(entity.Email, "Register confirmation", message);
        return new ResponseMessage { StatusCode = System.Net.HttpStatusCode.OK,
        Data = result};
    }

}



