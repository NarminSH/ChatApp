using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Employees.Commands.ChangePassword;
using Application.Employees.Commands.ConfirmEmployee;
using Application.Employees.Commands.CreateEmployee;
using Application.Employees.Commands.ForgetPassword;
using Application.Employees.Commands.LoginEmployee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ApiBaseController
    {
      
        [HttpPost("register")]
        public async Task<ResponseMessage> Register([FromBody] CreateEmployeeCommand value)
        {

            ResponseMessage result = await Mediator.Send(value);
            return result;
        }
        [HttpPost("login")]
        public async Task<ResponseMessage> Login([FromBody] LoginEmployeeCommand command)
        {
            ResponseMessage result = await Mediator.Send(command);
            return result;
        }
        [HttpPost("confirmEmail")]
        public async Task<ResponseMessage> ConfirmEmail([FromBody] ConfirmEmployeeCommand command)
        {
            ResponseMessage result = await Mediator.Send(command);
            return result;
        }
        [HttpPost("forgetPassword")]
        public async Task<ResponseMessage> ForgetPassword([FromBody] ForgetPasswordCommand command)
        {
            ResponseMessage result = await Mediator.Send(command);
            return result;
        }

        [HttpPost("forgetPasswordConfirmation")]
        public async Task<ResponseMessage> ForgetPasswordConfirmation([FromBody] ForgetPasswordCommandConfirm command)
        {
            ResponseMessage result = await Mediator.Send(command);
            return result;
        }

        [HttpPost("changePassword")]
        [Authorize]
        public async Task<ResponseMessage> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            command.Email = userEmail;
            ResponseMessage result = await Mediator.Send(command);
            return result;
        }
    }
} 

