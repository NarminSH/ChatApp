using System;
using Application.Common;
using Application.Repositories.Abstraction;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Connections.Commands.AddUserConnection;


public class AddUserToConnectionCommand : IRequest<ResponseMessage>, IMapFrom<EmployeeChannel>
{
    public int ChannelId { get; set; }
    public string EmployeeId { get; set; } = null!;
}
public class AddUserToConnectionCommandHandler : IRequestHandler<AddUserToConnectionCommand, ResponseMessage>
{
    private readonly IEmployeeChannelRepository _employeeChannel;
    private readonly IConnectionRepository _connectionRepository;
    private readonly IMapper _mapper;
    public AddUserToConnectionCommandHandler(IEmployeeChannelRepository repository,
        IConnectionRepository connectionRepository, IMapper mapper)
    {
        _employeeChannel = repository;
        _connectionRepository = connectionRepository;
        _mapper = mapper;
    }
    public async Task<ResponseMessage> Handle(AddUserToConnectionCommand request, CancellationToken cancellationToken)
    {
        Connection connection = await _connectionRepository.GetByIdInt(request.ChannelId);
        var entity = _mapper.Map<EmployeeChannel>(request);
        var result = await _employeeChannel.AddAsync(entity);
        if (result)
        {
        return new ResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Added user"
        };
        }
        return new ResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest,
            Message = "Problem occured"
        };

    }
}