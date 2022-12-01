using System;
using Application.Common;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.Posts.Commands.DeletePost
{
    public record DeletePostCommand(int postId) : IRequest<ResponseMessage>;
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, ResponseMessage>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        public DeletePostCommandHandler(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }
        public async Task<ResponseMessage> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            Post entity = await _postRepository.GetByIdInt(request.postId);
            _postRepository.Delete(entity);
            return new ResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Deleted the Post successfully!"
            };
        }

    }
}