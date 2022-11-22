using System;
using System.Xml.Linq;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Employees.Commands.CreateEmployee;
public class CreateEmployeeCommand : IRequest<IdentityResult>, IMapFrom<Employee>
{
    public string Fullname { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, IdentityResult>
{
    private readonly UserManager<Employee> _userManager;
    private readonly IMapper _mapper;
    private readonly IEmailSender _mailService;


    public CreateEmployeeCommandHandler(UserManager<Employee> userManager, IMapper mapper,
        IEmailSender emailSender)
    {
        this._mapper = mapper;
        this._userManager = userManager;
        this._mailService = emailSender;
    }
    public async Task<IdentityResult> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<Employee>(request);
        IdentityResult result = await _userManager.CreateAsync(entity, entity.PasswordHash);
        await _mailService.SendEmailAsync(entity.Email, "Register confirmation", "<h1> Please confirm your email</h1>");
        return result;
    }

}



