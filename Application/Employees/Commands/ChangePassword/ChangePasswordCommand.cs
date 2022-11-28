using System;
using Application.Repositories.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Employees.Commands.ChangePassword
{
    public class ChangePasswordCommand: IRequest<bool>
    {
        public string Id { get; set; } = null!;
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;    
    }
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<Employee> _userManager;

        public ChangePasswordCommandHandler(IEmployeeRepository employeeRepository,
            UserManager<Employee> userManager)
        {
            this._employeeRepository = employeeRepository;
            this._userManager = userManager;
        }
        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            Employee employee = await _userManager.FindByIdAsync(request.Id);
            var res = await _userManager.ChangePasswordAsync(employee,
                request.CurrentPassword, request.NewPassword);
            if (res.Succeeded)
            {
                return true;
            }
            else { return false; }
        }
    }
}

