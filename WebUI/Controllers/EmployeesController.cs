using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Employees.Commands.ConfirmEmployee;
using Application.Employees.Commands.CreateEmployee;
using Application.Employees.Commands.ForgetPassword;
using Application.Employees.Commands.LoginEmployee;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ApiBaseController
    {
      
        [HttpPost("register")]
        public async Task<IdentityResult> Register([FromBody] CreateEmployeeCommand value)
        {
           
            IdentityResult result = await Mediator.Send(value);
            return result;
        }
        [HttpPost("login")]
        public async Task<string> Login([FromBody] LoginEmployeeCommand command)
        {
            string result = await Mediator.Send(command);
            return result;
        }
        [HttpPost("confirmEmail")]
        public async Task<string> ConfirmEmail([FromBody] ConfirmEmployeeCommand command)
        {
            string result = await Mediator.Send(command);
            return result;
        }
        [HttpPost("forgetPassword")]
        public async Task<string> ForgetPassword([FromBody] ForgetPasswordCommand command)
        {
            string result = await Mediator.Send(command);
            return result;
        }

        [HttpPost("forgetPasswordConfirmation")]
        public async Task<string> ForgetPasswordConfirmation([FromBody] ForgetPasswordCommandConfirm command)
        {
            string result = await Mediator.Send(command);
            return result;
        }
    }
} 

