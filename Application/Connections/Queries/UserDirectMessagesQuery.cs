using System;
using Application.Common;
using System.Net;
using Application.Dtos.ConnectionDtos;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.Connections.Queries
{
    public class UserDirectMessagesQuery : IRequest<ResponseMessage>
    {
        public string userId { get; set; } = null!;
    };

    public class UserDirectMessagesQueryHandler : IRequestHandler<UserDirectMessagesQuery, ResponseMessage>
    {
        private readonly IConnectionRepository _connectionRepositorty;
        private readonly IMapper _mapper;

        public UserDirectMessagesQueryHandler(IConnectionRepository repository, IMapper mapper)
        {
            this._connectionRepositorty = repository;
            this._mapper = mapper;
        }

        public async Task<ResponseMessage> Handle(UserDirectMessagesQuery request, CancellationToken cancellationToken)
        {
            var connections = _connectionRepositorty.GetUserDirectMessages(request.userId);
            var connectionDtos = _mapper.Map<IEnumerable<GetConnectionDto>>(connections);
            return new ResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Data = connectionDtos
            };
        }
    }
}

