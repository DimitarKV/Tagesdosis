using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tagesdosis.Services.User.DTOs;
using Tagesdosis.Services.User.Entities;

namespace Tagesdosis.Services.User.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public UserController(IMapper mapper, UserManager<AppUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] UserCredentialsDTO credentialsDto)
    {
        var newUser = _mapper.Map<AppUser>(credentialsDto);
        newUser.CreatedDateTime = DateTime.Now;
        newUser.UpdatedDateTime = DateTime.Now;
        var created = await _userManager.CreateAsync(newUser, credentialsDto.Password);
        if (created.Succeeded)
            return Ok();
        return BadRequest();
    }
}