using System;
using Application.Common;
using Application.Employees.Commands.ChangePassword;
using Application.Employees.Commands.ForgetPassword;
using Microsoft.AspNetCore.Identity;

namespace Application.Repositories.Abstraction
{
    public interface IAuth
    {
        Task<IdentityResult> CreateEmployee(Employee entity);
        Task<ResponseMessage> LoginEmployee(Employee entity);
        Task<ResponseMessage> ForgetPassword(string email);
        Task<ResponseMessage> ForgetPasswordConfirmation(ForgetPasswordCommandConfirm forgetPasswordCommand);
        Task<ResponseMessage> ChangePassword(ChangePasswordCommand changePasswordCommand);

    }
}

