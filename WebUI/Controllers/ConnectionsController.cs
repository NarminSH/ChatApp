using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Connections.Commands.AddUserConnection;
using Application.Connections.Commands.CreateConnection;
using Application.Connections.Commands.DeleteConnection;
using Application.Connections.Commands.UpdateConnection;
using Application.Connections.Queries;
using Application.Dtos.ConnectionDtos;
using Application.Employees.Commands.CreateEmployee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConnectionsController : ApiBaseController
    {
        [HttpGet]
        public async Task<ResponseMessage> GetAllConnections()
        {
            ResponseMessage res = await Mediator.Send(new ConnectionsQuery());
            return res;
        }
        [HttpPost]
        public async Task<bool> Post([FromBody] CreateConnectionCommand value)
        {
            bool res = await Mediator.Send(value);
            return res;
        }

        [HttpPut]
        public async Task<ResponseMessage> Update([FromBody] UpdateConnectionCommand command)
        {
            ResponseMessage res = await Mediator.Send(command);
            return res;
        }

        [HttpDelete]
        public async Task<bool> Delete([FromBody] DeleteConnectionCommand command)
        {
            bool res = await Mediator.Send(command);
            return res;
        }

        [HttpPost("getUserChannels/{id}")]
        public async Task<ResponseMessage> GetUserChannels([FromBody] UserChannelsQuery query)
        {
            ResponseMessage res = await Mediator.Send(query);
            return res;
        }
        [HttpGet("getUserMessages/{id}")]
        public async Task<ResponseMessage> GetUserMessages([FromQuery] UserDirectMessagesQuery query)
        {
            ResponseMessage res = await Mediator.Send(query);
            return res;
        }

        [HttpPost("addUserToConnection")]
        public async Task<ResponseMessage> AddUserToConnection([FromBody] AddUserToConnectionCommand command)
        {
            ResponseMessage res = await Mediator.Send(command);
            return res;
        }

    }
}

