using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Employees.Commands.ConfirmEmployee;
using Application.Employees.Commands.CreateEmployee;
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
        public async Task<IdentityResult> Post([FromBody] CreateEmployeeCommand value)
        {
           
            IdentityResult res = await Mediator.Send(value);
            return res;
        }
        [HttpPost("login")]
        public async Task<string> Login([FromBody] LoginEmployeeCommand command)
        {
            string res = await Mediator.Send(command);
            return res;
        }
        [HttpPost("confirmEmail")]
        public async Task<string> ConfirmEmail([FromBody] ConfirmEmployeeCommand command)
        {
            string res = await Mediator.Send(command);
            return res;
        }
    }
} 

