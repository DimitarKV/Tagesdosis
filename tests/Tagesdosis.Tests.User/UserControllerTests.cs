using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Api.Controllers;
using Tagesdosis.Services.User.Commands.User.CreateUserCommand;
using Tagesdosis.Services.User.Commands.User.DeleteUserCommand;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.DTOs;
using Tagesdosis.Services.User.Identity;
using Tagesdosis.Services.User.MappingProfiles;
using Tagesdosis.Tests.User.Mock;
using Xunit;

namespace Tagesdosis.Tests.User;

public class UserControllerTests
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    
    public UserControllerTests()
    {
        _userManager = UserManagerMock.Create();
        
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UserProfile());
        });
        _mapper = new Mapper(configuration);
    }

    [Theory]
    [InlineData("johndoe", "johndoe@test.com", "#Password123")]
    public async Task CreateUser_ShouldReturnOk(string userName, string email, string password)
    {
        var result = await CreateUserAsync(userName, email, password);
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Theory]
    [InlineData("johndoe", "johndoe@test.com", "short")]
    [InlineData("johndoe", "johndoe@test.com", "NonNumeric1234")]
    [InlineData("johndoe", "johndoe@test.com", "#NoNumbers")]
    public async Task CreateUser_ShouldReturnBadRequest(string userName, string email, string password)
    {
        var result = await CreateUserAsync(userName, email, password);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    private async Task<IActionResult> CreateUserAsync(string userName, string email, string password)
    {
        var mediator =
            MediatorMock.Create<ApiResponse,
                CreateUserCommand,
                CreateUserCommandHandler,
                CreateUserCommandValidator>(
                new CreateUserCommandHandler(_mapper, new IdentityService(_userManager)),
                new ApiResponse(null, new[] {"Error"}));
        
        var controller = new UserController(mediator.Object, _mapper);

        var credentials = new UserCredentialsDTO
        {
            UserName = userName,
            Email = email,
            Password = password
        };
        
        return await controller.RegisterAsync(credentials);
    }

    [Theory]
    [InlineData("johndoe")]
    public async Task DeleteUser_ShouldReturnOk(string userName)
    {
        var result = await DeleteUserAsync(userName);
        Assert.IsType<OkObjectResult>(result);
    }
    
    private async Task<IActionResult> DeleteUserAsync(string userName)
    {
        var mediator =
            MediatorMock.Create<ApiResponse,
                DeleteUserCommand,
                DeleteUserCommandHandler,
                DeleteUserCommandValidator>(
                new DeleteUserCommandHandler(new IdentityService(_userManager)),
                new ApiResponse(null, new[] {"Error"}));
        
        var controller = new UserController(mediator.Object, _mapper);

        var userProp = controller
            .GetType()
            .GetProperty(nameof(UserController.User), BindingFlags.Instance | BindingFlags.Public);

        var claims = new Claim[]
        {
            new Claim(ClaimTypes.Name, userName)
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
        var context = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = user
            }
        };

        controller.ControllerContext = context;
        
        return await controller.DeleteUser();
    }
}