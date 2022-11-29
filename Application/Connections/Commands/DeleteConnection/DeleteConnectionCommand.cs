using System;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.Connections.Commands.DeleteConnection;

public class DeleteConnectionCommand : IRequest<bool>
{
    public int Id { get; set; }
}
public class DeleteConnectionCommandHandler : IRequestHandler<DeleteConnectionCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IConnectionRepository _connectionRepository;
    public DeleteConnectionCommandHandler(IMapper mapper, IConnectionRepository connection)
    {
        _mapper = mapper;
        _connectionRepository = connection;
    }
    public async Task<bool> Handle(DeleteConnectionCommand request, CancellationToken cancellationToken)
    {
        Connection entity = await _connectionRepository.GetByIdInt(request.Id);
        bool result = _connectionRepository.Delete(entity);
        return result;
    }

}

