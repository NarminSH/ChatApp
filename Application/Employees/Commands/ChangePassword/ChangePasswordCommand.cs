using System;
using System.Net;
using Application.Common;
using Application.Common.Exceptions;
using Application.Repositories.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Employees.Commands.ChangePassword
{
    public class ChangePasswordCommand: IRequest<ResponseMessage>
    {
        public string? Email { get; set; }
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;    
    }
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ResponseMessage>
    {
        private readonly IAuth _authService;

        public ChangePasswordCommandHandler(IAuth auth)
        {
            _authService = auth;
        }
        public async Task<ResponseMessage> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            ResponseMessage res = await _authService.ChangePassword(changePasswordCommand: request);
            return new ResponseMessage
            {
                StatusCode = res.StatusCode,
                Message = res.Message
            };

        }
    }
}

