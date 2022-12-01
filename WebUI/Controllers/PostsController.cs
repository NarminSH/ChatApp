using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Connections.Commands.CreateConnection;
using Application.Connections.Commands.DeleteConnection;
using Application.Connections.Commands.UpdateConnection;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.DeletePost;
using Application.Posts.Commands.UpdatePost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //todo don't forget
    //[Authorize]
    public class PostsController : ApiBaseController
    {

        [HttpPost]
        public async Task<ResponseMessage> Post([FromBody] CreatePostCommand value)
        {
            ResponseMessage res = await Mediator.Send(value);
            return res;
        }

        [HttpPut]
        public async Task<ResponseMessage> Update([FromBody] UpdatePostCommand command)
        {
            ResponseMessage res = await Mediator.Send(command);
            return res;
        }

        [HttpDelete("{id:int}")]
        public async Task<ResponseMessage> Delete(int id)
        {
            ResponseMessage res = await Mediator.Send(new DeletePostCommand(id));
            return res;
        }
    }
}

