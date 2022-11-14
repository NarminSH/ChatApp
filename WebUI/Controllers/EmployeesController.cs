using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Employees.Commands.CreateEmployee;
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
    }
}

