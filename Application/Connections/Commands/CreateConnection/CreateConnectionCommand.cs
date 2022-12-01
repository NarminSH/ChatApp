using System;
using Application.Common;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.Connections.Commands.CreateConnection
{
    public class CreateConnectionCommand: IRequest<ResponseMessage>, IMapFrom<Connection>
    {
        public string Name { get; set; } = null!;
        public bool IsChannel { get; set; }
        public bool IsPrivate { get; set; }
    }
    public class CreateConnectionCommandHandler: IRequestHandler<CreateConnectionCommand, ResponseMessage>
    {
        private readonly IMapper _mapper;
        private readonly IConnectionRepository _connectionRepository;
        public CreateConnectionCommandHandler(IMapper mapper, IConnectionRepository connection)
        {
            _mapper = mapper;
            _connectionRepository = connection;
        }
        public async Task<ResponseMessage> Handle(CreateConnectionCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Connection>(request);
            var result = await _connectionRepository.AddAsync(entity);
            if (result)
            {
            return new ResponseMessage { StatusCode = System.Net.HttpStatusCode.Created,
            Message = "Created the Connection successfully!"};
            }
            return new ResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = "Problem occured while creating connection"
            };
        }
    }
}

