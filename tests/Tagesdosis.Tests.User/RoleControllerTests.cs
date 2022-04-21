using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Domain.Types;
using Tagesdosis.Services.User.Api.Controllers;
using Tagesdosis.Services.User.Data.Entities;
using Tagesdosis.Services.User.Identity;
using Tagesdosis.Services.User.MappingProfiles;
using Tagesdosis.Services.User.Queries.Role.GetRolesForUser;
using Tagesdosis.Tests.User.Mock;
using Xunit;

namespace Tagesdosis.Tests.User;

public class RoleControllerTests
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    
    public RoleControllerTests()
    {
        _userManager = UserManagerMock.Create();
        
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UserProfile());
        });
        _mapper = new Mapper(configuration);
    }

    [Theory]
    [InlineData("johndoe")]
    public async Task GetRolesForUser_ShouldReturnOk(string userName)
    {
        var mediator =
            MediatorMock.Create<ApiResponse<List<string>>,
                GetRolesForUserQuery,
                GetRolesForUserQueryHandler,
                GetRolesForUserQueryValidator>(
                new GetRolesForUserQueryHandler(new IdentityService(_userManager)),
                new ApiResponse<List<string>>(null, "",new[] {"Error"}));
        
        var controller = new RoleController(mediator.Object);

        var claims = new Claim[]
        {
            new(ClaimTypes.Name, userName)
        };

        controller.ControllerContext = CreateControllerContextWithClaims(claims);
        
        var result = await controller.GetRolesForUserAsync();
        Assert.IsType<OkObjectResult>(result);
    }

    [Theory]
    [InlineData("johndoe")]
    public async Task AddUserToRole_ShouldReturnOk(string userName)
    {
        var mediator =
            MediatorMock.Create<ApiResponse<List<string>>,
                GetRolesForUserQuery,
                GetRolesForUserQueryHandler,
                GetRolesForUserQueryValidator>(
                new GetRolesForUserQueryHandler(new IdentityService(_userManager)),
                new ApiResponse<List<string>>(null, "",new[] {"Error"}));
        
        var controller = new RoleController(mediator.Object);

        var claims = new Claim[]
        {
            new(ClaimTypes.Name, userName)
        };
        
        controller.ControllerContext = CreateControllerContextWithClaims(claims);
        
        var result = await controller.GetRolesForUserAsync();
        Assert.IsType<OkObjectResult>(result);
    }

    private ControllerContext CreateControllerContextWithClaims(IEnumerable<Claim> claims)
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
        var context = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = user
            }
        };

        return context;
    }
}