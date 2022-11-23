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

namespace Application.Employees.Commands.LoginEmployee;
public class LoginEmployeeCommand: IRequest<string>, IMapFrom<Employee>
{
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}

public class LoginEmployeeCommandHandler : IRequestHandler<LoginEmployeeCommand, string>
{
    private readonly UserManager<Employee> _userManager;
    private readonly SignInManager<Employee> _signInManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public LoginEmployeeCommandHandler(UserManager<Employee> userManager, IMapper mapper,
        IConfiguration config, SignInManager<Employee> signInManager)
    {
        this._mapper = mapper;
        this._signInManager = signInManager;
        this._userManager = userManager;
        this._config = config;
    }

    public async Task<string> Handle(LoginEmployeeCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Employee>(request);
        Employee existedUser = await _userManager.FindByEmailAsync(entity.Email);
        if (existedUser is null) throw new NotFoundException();
        bool result = await _userManager.CheckPasswordAsync(existedUser, entity.PasswordHash);
        if (!result) return "Username or password is incorrect";
        if (!existedUser.EmailConfirmed){ return "Email is not confirmed";}
        else
        {
        List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, existedUser.Id),
                new Claim(ClaimTypes.Email, existedUser.Email)
            };
        string keyStr = _config["Jwt:Key"];
        

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));

        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new JwtSecurityToken(
                audience: _config["Jwt:Audience"],
                issuer: _config["Jwt:Issue"],
                expires: DateTime.Now.AddDays(20),
                signingCredentials: credentials,
                claims: claims
                );

        string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenStr;
        }

    }
}

