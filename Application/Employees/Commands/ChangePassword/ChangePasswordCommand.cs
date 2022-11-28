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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<Employee> _userManager;

        public ChangePasswordCommandHandler(IEmployeeRepository employeeRepository,
            UserManager<Employee> userManager)
        {
            this._employeeRepository = employeeRepository;
            this._userManager = userManager;
        }
        public async Task<ResponseMessage> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            Employee employee = await _userManager.FindByEmailAsync(request.Email);
            if (employee != null)
            {
            var res = await _userManager.ChangePasswordAsync(employee,
                request.CurrentPassword, request.NewPassword);
                if (res.Succeeded)
                {
                    return new ResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Successfully changed the password"
                    };
                }
                else
                {
                    return new ResponseMessage
                    {
                        StatusCode = HttpStatusCode.Forbidden,
                        Message = "Was not able to change the password"
                    };
                }
            }
            else { throw new NotFoundException("Could not find employee"); }
        }
    }
}

