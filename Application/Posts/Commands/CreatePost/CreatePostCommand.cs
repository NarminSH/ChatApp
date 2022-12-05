using System;
using Application.Common;
using Application.Hubs;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;

namespace Application.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<ResponseMessage>, IMapFrom<Post>
{
    public int ChannelId { get; set; }
    public string EmployeeId { get; set; } = null!;
    public string Message { get; set; } = null!;
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ResponseMessage>
{
    private readonly IConnectionRepository _connectionRepository;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    private IHubContext<NotifyHub, ITypedHubClient> _hubContext;

    public CreatePostCommandHandler(IPostRepository postRepository, IMapper mapper,
        IConnectionRepository connectionRepository,
        IHubContext<NotifyHub, ITypedHubClient> hubContext)
    {
        this._mapper = mapper;
        this._postRepository = postRepository;
        this._connectionRepository = connectionRepository;
        this._hubContext = hubContext;
    }

    public async Task<ResponseMessage> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        await _connectionRepository.GetByIdInt(request.ChannelId);
        Post entity = _mapper.Map<Post>(request);
        bool result = await _postRepository.AddAsync(entity);
        if (result)
        {
            await _hubContext.Clients.All.BroadcastPost(post: entity);
            return new ResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = "Created the Post successfully!"
            };
        }
        return new ResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest,
            Message = "Problem occured while publishing Post"
        };
    }

}


