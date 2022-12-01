using System;
using Application.Common;
using Application.Connections.Commands.UpdateConnection;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommand : IRequest<ResponseMessage>, IMapFrom<Post>
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public string EmployeeId { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int? ReplyPostId { get; set; }
    }
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ResponseMessage>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly IConnectionRepository _connectionRepository;
        public UpdatePostCommandHandler(IMapper mapper, IPostRepository postRepository,
            IConnectionRepository connectionRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _connectionRepository = connectionRepository; 
        }
        public async Task<ResponseMessage> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            await _connectionRepository.GetByIdInt(request.ChannelId);
            await _postRepository.GetByIdInt(request.Id);
            Post entity = _mapper.Map<Post>(request);
            entity.IsEdited = true;
            bool result = await _postRepository.Update(entity);
            return new ResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Data = result
            };

        }
    }
}

