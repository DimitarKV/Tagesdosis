using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Services.User.Security;

namespace Tagesdosis.Services.User.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RoleController : ControllerBase
{
    public RoleController()
    {
        
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
    public async Task<IActionResult> GetRoles()
    {
        var userName = User.Identity!.Name;

        return Ok();
    }

}