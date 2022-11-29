using System;
using Application.Common;
using Application.Common.Exceptions;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.Connections.Commands.UpdateConnection
{
    public class UpdateConnectionCommand : IRequest<ResponseMessage>, IMapFrom<Connection>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsChannel { get; set; } 
        public bool IsPrivate { get; set; }
        public string SenderId { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
    }
    public class UpdateConnectionCommandHandler : IRequestHandler<UpdateConnectionCommand, ResponseMessage>
    {
        private readonly IMapper _mapper;
        private readonly IConnectionRepository _connectionRepository;
        public UpdateConnectionCommandHandler(IMapper mapper, IConnectionRepository connection)
        {
            _mapper = mapper;
            _connectionRepository = connection;
        }
        public async Task<ResponseMessage> Handle(UpdateConnectionCommand request, CancellationToken cancellationToken)
        {
            Connection connection = await _connectionRepository.GetByIdInt(request.Id);
            Connection entity = _mapper.Map<Connection>(request);
            bool result = await _connectionRepository.Update(entity);
            return new ResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = result
            };
            
        }
    }
}

