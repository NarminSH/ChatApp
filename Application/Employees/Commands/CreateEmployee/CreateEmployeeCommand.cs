using System;
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

    public CreateEmployeeCommandHandler(UserManager<Employee> userManager, IMapper mapper)
    {
        this._mapper = mapper;
        this._userManager = userManager;
    }
    public async Task<IdentityResult> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<Employee>(request);
        IdentityResult result = await _userManager.CreateAsync(entity, entity.PasswordHash);
        return result;

    }

}



