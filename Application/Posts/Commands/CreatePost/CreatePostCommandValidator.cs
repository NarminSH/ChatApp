using System;
using Application.Employees.Commands.CreateEmployee;
using Application.Posts.Commands.CreatePost;
using Application.Repositories.Abstraction;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly UserManager<Employee> _userManager;
    public CreatePostCommandValidator(IPostRepository repository,
        UserManager<Employee> userManager)
    {
        this._postRepository = repository;
        this._userManager = userManager;

        RuleFor(v => v.ChannelId).NotEmpty().WithMessage("Channel Id is required");
        RuleFor(v => v.EmployeeId).NotEmpty().WithMessage("Employee is required")
            .Must(IfUserExistsInDb).WithMessage("Employee does not exist");
    }
    private bool IfUserExistsInDb(string id)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
        if (user != null) return true;
        else return false;
    }
}