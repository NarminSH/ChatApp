using System;
using Application.Common;
using Application.Common.Exceptions;
using Application.Repositories.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Employees.Commands.ChangePassword
{
    public class ChangePasswordCommand: IRequest<string>
    {
        public string? Email { get; set; }
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;    
    }
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<Employee> _userManager;

        public ChangePasswordCommandHandler(IEmployeeRepository employeeRepository,
            UserManager<Employee> userManager)
        {
            this._employeeRepository = employeeRepository;
            this._userManager = userManager;
        }
        public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            Employee employee = await _userManager.FindByEmailAsync(request.Email);
            if (employee != null)
            {
            var res = await _userManager.ChangePasswordAsync(employee,
                request.CurrentPassword, request.NewPassword);
            if (res.Succeeded)
            {
                return "Successfully changed password";
            } else { return "Was not able to change password "; }
            }
            else { throw new NotFoundException("Could not find employee"); }
        }
    }
}

