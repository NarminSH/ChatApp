using System;
using Application.Common;
using Application.Hubs;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Application.Posts.Commands.CreateReply
{
    public class CreateReplyCommand: IRequest<ResponseMessage>, IMapFrom<Post>
    {
        public int PostId { get; set; }
        public string EmployeeId { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
    public class CreateReplyCommandHandler: IRequestHandler<CreateReplyCommand, ResponseMessage>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;
        public CreateReplyCommandHandler(IPostRepository postRepo,
            IMapper mapper, IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _postRepository = postRepo;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<ResponseMessage> Handle(CreateReplyCommand request, CancellationToken cancellationToken)
        {
            Post existedPost = await _postRepository.GetByIdInt(request.PostId);
            Post replyPost = _mapper.Map<Post>(request);
            replyPost.ChannelId = existedPost.ChannelId;
            replyPost.ReplyPostId = existedPost.Id;
            var res = await _postRepository.AddAsync(replyPost);
            await _postRepository.Update(existedPost);
            await _hubContext.Clients.All.BroadcastPost(post: replyPost);
            return new ResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Created the reply!"
            };
            
        }
    }

}

