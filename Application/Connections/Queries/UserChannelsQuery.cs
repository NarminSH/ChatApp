using System;
using Application.Common;
using Application.Dtos.ConnectionDtos;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Connections.Queries
{
    public class UserChannelsQuery : IRequest<ResponseMessage>
    {
        public string userId { get; set; } = null!;
    };

    public class UserChannelsQueryHandler : IRequestHandler<UserChannelsQuery,ResponseMessage>
    {
        private readonly IConnectionRepository _connectionRepositorty;
        private readonly IMapper _mapper;

        public UserChannelsQueryHandler(IConnectionRepository repository, IMapper mapper)
        {
            this._connectionRepositorty = repository;
            this._mapper = mapper;
        }

        public async Task<ResponseMessage> Handle(UserChannelsQuery request, CancellationToken cancellationToken)
        {
            var connections = _connectionRepositorty.GetUsersChannels(request.userId);
            var connectionDtos = _mapper.Map<IEnumerable<GetConnectionDto>>(connections);
            return new ResponseMessage {StatusCode = HttpStatusCode.OK,
            Data = connectionDtos};
        }
    }
}



