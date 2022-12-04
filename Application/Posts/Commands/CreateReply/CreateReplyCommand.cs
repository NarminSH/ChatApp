using System;
using Application.Common;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;

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
        public CreateReplyCommandHandler(IPostRepository postRepo,
            IMapper mapper)
        {
            _postRepository = postRepo;
            _mapper = mapper;
        }

        public async Task<ResponseMessage> Handle(CreateReplyCommand request, CancellationToken cancellationToken)
        {
            Post existedPost = await _postRepository.GetByIdInt(request.PostId);
            Post replyPost = _mapper.Map<Post>(request);
            replyPost.ChannelId = existedPost.ChannelId;
            var res = await _postRepository.AddAsync(replyPost);
            existedPost.ReplyPostId = replyPost.Id;
            await _postRepository.Update(existedPost);
            return new ResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Created the reply!"
            };
            
        }
    }

}

