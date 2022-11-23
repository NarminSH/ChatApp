using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Connections.Commands.CreateConnection;
using Application.Employees.Commands.CreateEmployee;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionsController : ApiBaseController
    {
        [HttpPost]
        public async Task<bool> Post([FromBody] CreateConnectionCommand value)
        {
            bool res = await Mediator.Send(value);
            return res;
        }
       
    }
}

