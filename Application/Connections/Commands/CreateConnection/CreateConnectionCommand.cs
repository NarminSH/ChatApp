using System;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.Connections.Commands.CreateConnection
{
    public class CreateConnectionCommand: IRequest<bool>, IMapFrom<Connection>
    {
        public string Name { get; set; } = null!;
        public bool IsChannel { get; set; }
        public bool IsPrivate { get; set; }
    }
    public class CreateConnectionCommandHandler: IRequestHandler<CreateConnectionCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IConnectionRepository _connectionRepository;
        public CreateConnectionCommandHandler(IMapper mapper, IConnectionRepository connection)
        {
            _mapper = mapper;
            _connectionRepository = connection;
        }
        public Task<bool> Handle(CreateConnectionCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Connection>(request);
            var result = _connectionRepository.AddAsync(entity);
            //todo repsonse message
            return result;
        }
    }
}

