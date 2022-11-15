using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Employees.Commands.CreateEmployee;
using Application.Employees.Commands.LoginEmployee;
using Microsoft.AspNetCore.Mvc;



namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ApiBaseController
    {
        [HttpPost]
        public async Task Post([FromBody] CreateEmployeeCommand value)
        {
            await Mediator.Send(value);
        }
        [HttpPost("login")]
        public async Task Login([FromBody] LoginEmployeeCommand command)
        {
            await Mediator.Send(command);
        }
    }
}

