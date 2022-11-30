using System;
using Application.Common;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace Application.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<ResponseMessage>, IMapFrom<Post>
{
    public int ChannelId { get; set; }
    public string EmployeeId { get; set; } = null!;
    public string Message { get; set; } = null!;
    public int? ReplyPostId { get; set; }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ResponseMessage>
{
    private readonly UserManager<Employee> _userManager;
    private readonly IMapper _mapper;

    public CreatePostCommandHandler(UserManager<Employee> userManager, IMapper mapper)
    {
        this._mapper = mapper;
        this._userManager = userManager;
    }

    public async Task<ResponseMessage> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Employee>(request);
        
        return new ResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Message = "Successfully created the user!"
        };
    }

}


