using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Connections.Commands.CreateConnection;
using Application.Connections.Commands.DeleteConnection;
using Application.Connections.Commands.UpdateConnection;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.CreateReply;
using Application.Posts.Commands.DeletePost;
using Application.Posts.Commands.DownloadAttachment;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [HttpPost("replies")]
        public async Task<ResponseMessage> PostReply([FromBody] CreateReplyCommand command)
        {
            ResponseMessage res = await Mediator.Send(command);
            return res;
        }

        [HttpPost("attachments")]
        public async Task<ResponseMessage> PostAttachment([FromForm] DownloadAttachmentCommand command)
        {
            ResponseMessage res = await Mediator.Send(command);
            return res;
        }

        [HttpGet("channels/{id:int}")]
        public async Task<ResponseMessage> PostAttachment(int id)
        {
            ResponseMessage res = await Mediator.Send(new GetPostsByChannel(id));
            return res;
        }
    }
}

