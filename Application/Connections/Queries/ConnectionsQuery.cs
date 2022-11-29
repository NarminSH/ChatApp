using System;
using Application.Common;
using System.Net;
using Application.Dtos.ConnectionDtos;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.Connections.Queries
{
    public record ConnectionsQuery : IRequest<ResponseMessage>;

    public class ConnectionsQueryHandler : IRequestHandler<ConnectionsQuery, ResponseMessage>
    {
        private readonly IConnectionRepository _connectionRepositorty;
        private readonly IMapper _mapper;

        public ConnectionsQueryHandler(IConnectionRepository repository, IMapper mapper)
        {
            this._connectionRepositorty = repository;
            this._mapper = mapper;
        }

        public async Task<ResponseMessage> Handle(ConnectionsQuery request, CancellationToken cancellationToken)
        {
            var connections = await _connectionRepositorty.GetAllAsync();
            var connectionDtos = _mapper.Map<IEnumerable<GetConnectionDto>>(connections);
            return new ResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Data = connectionDtos
            };
        }

    }

}
