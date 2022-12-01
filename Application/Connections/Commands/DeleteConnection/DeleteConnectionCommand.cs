using System;
using Application.Common;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.Connections.Commands.DeleteConnection;

public record DeleteConnectionCommand(int connectionId) : IRequest<ResponseMessage>;
public class DeleteConnectionCommandHandler : IRequestHandler<DeleteConnectionCommand, ResponseMessage>
{
    private readonly IMapper _mapper;
    private readonly IConnectionRepository _connectionRepository;
    public DeleteConnectionCommandHandler(IMapper mapper, IConnectionRepository connection)
    {
        _mapper = mapper;
        _connectionRepository = connection;
    }
    public async Task<ResponseMessage> Handle(DeleteConnectionCommand request, CancellationToken cancellationToken)
    {
        Connection entity = await _connectionRepository.GetByIdInt(request.connectionId);
        bool result = _connectionRepository.Delete(entity);
        return new ResponseMessage {  StatusCode = System.Net.HttpStatusCode.OK,
        Message = "Deleted the Connection successfully!"};
    }

}

