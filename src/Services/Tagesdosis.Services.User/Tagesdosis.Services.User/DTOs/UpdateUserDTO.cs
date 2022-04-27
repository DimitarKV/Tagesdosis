namespace Tagesdosis.Services.User.DTOs;

public class UpdateUserDTO
{
    public string? NewUserName { get; set; }
    public string? Email { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
}