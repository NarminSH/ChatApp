using System;
using Application.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;
using Application.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Application.Employees.Commands.ForgetPassword;
using MediatR;
using Application.Employees.Commands.ChangePassword;

namespace Infrastructure.Services
{
    public class AuthService : IAuth
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IEmailSender _mailService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration _config;

        public AuthService(UserManager<Employee> userManager,
        IEmailSender emailSender, LinkGenerator linkGenerator, IHttpContextAccessor http,
        IConfiguration config)
        {
            this._userManager = userManager;
            this._mailService = emailSender;
            this._linkGenerator = linkGenerator;
            this._httpContext = http;
            this._config = config;
        }
        public async Task<IdentityResult> CreateEmployee(Employee entity)
        {
            IdentityResult result = await _userManager.CreateAsync(entity, entity.PasswordHash);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(entity);
            Console.WriteLine(".................");
            Console.WriteLine("Register confirmation token is: " + token);
            Console.WriteLine(".................");
            string url = _linkGenerator.GetUriByAction(_httpContext.HttpContext,
                action: "ConfirmEmail", controller: "Employees", values: new { token, entity.Id });
            string message = "Please confirm your email by clicking here " + url;
            await _mailService.SendEmailAsync(entity.Email, "Register confirmation", message);
            return result;
        }



        public async Task<ResponseMessage> LoginEmployee(Employee entity)
        {
            Employee existedUser = await _userManager.FindByEmailAsync(entity.Email);
            if (existedUser is null) throw new NotFoundException();
            bool result = await _userManager.CheckPasswordAsync(existedUser, entity.PasswordHash);
            if (!result)
            {
                return new ResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Username or password is incorrect"

                };
            }
            if (!existedUser.EmailConfirmed)
            {
                return new ResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Please confirm email address"

                };
            }
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
                return new ResponseMessage {StatusCode = HttpStatusCode.OK,
                    Message = "Successfully logged in!",
                    Data = tokenStr };

            }
        }

        public async Task<ResponseMessage> ForgetPassword(string email)
        {
            var existedUser = await _userManager.FindByEmailAsync(email);
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
            }
            else { throw new NotFoundException(); }
        }

        public async Task<ResponseMessage> ForgetPasswordConfirmation(ForgetPasswordCommandConfirm forgetPasswordCommand)
        {
            var existedUser = await _userManager.FindByEmailAsync(forgetPasswordCommand.Email);
            if (existedUser != null)
            {
                var result = await _userManager.ResetPasswordAsync(existedUser, forgetPasswordCommand.Token,
                    forgetPasswordCommand.NewPassword);
                if (result.Succeeded)
                {
                    return new ResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Successfully changed password"
                    };
                }
                else
                {
                    return new ResponseMessage
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "There was a problem changing password"
                    };
                }
            }
            else { throw new NotFoundException(); }
        }

        public async Task<ResponseMessage> ChangePassword(ChangePasswordCommand changePasswordCommand)
        {
            Employee employee = await _userManager.FindByEmailAsync(changePasswordCommand.Email);
            if (employee != null)
            {
                var res = await _userManager.ChangePasswordAsync(employee,
                    changePasswordCommand.CurrentPassword, changePasswordCommand.NewPassword);
                if (res.Succeeded)
                {
                    return new ResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Successfully changed the password"
                    };
                }
                else
                {
                    return new ResponseMessage
                    {
                        StatusCode = HttpStatusCode.Forbidden,
                        Message = "Was not able to change the password"
                    };
                }
            }
            else { throw new NotFoundException("Could not find employee"); }
        }
    }
}

