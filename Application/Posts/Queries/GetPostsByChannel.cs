using System;
using Application.Common;
using Application.Dtos.ConnectionDtos;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;
using System.Net;
using Application.Dtos.PostDtos;

namespace Application.Posts.Queries
{
    public record GetPostsByChannel(int connectionId) : IRequest<ResponseMessage>;

    public class GetPostsByChannelHandler : IRequestHandler<GetPostsByChannel, ResponseMessage>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetPostsByChannelHandler(IPostRepository repository, IMapper mapper)
        {
            this._postRepository = repository;
            this._mapper = mapper;
        }

        public async Task<ResponseMessage> Handle(GetPostsByChannel request, CancellationToken cancellationToken)
        {

            var posts = await _postRepository.GetChannelPosts(request.connectionId);
            var postDtos = _mapper.Map<IEnumerable<GetPostDto>>(posts);
            return new ResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Data =postDtos
            };
        }

    }
}

