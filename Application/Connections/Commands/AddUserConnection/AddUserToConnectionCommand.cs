using System;
using Application.Common;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.Connections.Commands.AddUserConnection;


public class AddUserToConnectionCommand : IRequest<ResponseMessage>
{
    public int Id { get; set; }
    public string ReceiverId { get; set; } = null!;
}
public class AddUserToConnectionCommandHandler : IRequestHandler<AddUserToConnectionCommand, ResponseMessage>
{
    private readonly IConnectionRepository _connectionRepository;
    public AddUserToConnectionCommandHandler(IConnectionRepository connection)
    {
        _connectionRepository = connection;
    }
    public async Task<ResponseMessage> Handle(AddUserToConnectionCommand request, CancellationToken cancellationToken)
    {
        Connection connection = await _connectionRepository.GetByIdInt(request.Id);
        connection.ReceiverId = request.ReceiverId;
        //Connection entity = _mapper.Map<Connection>(request);
        bool result = await _connectionRepository.Update(connection);
        return new ResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Data = result
        };

    }
}