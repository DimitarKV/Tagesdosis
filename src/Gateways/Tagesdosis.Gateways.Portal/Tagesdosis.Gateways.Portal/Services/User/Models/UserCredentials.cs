namespace Tagesdosis.Gateways.Portal.Services.User.Models;

public class UserCredentials
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public UserCredentials(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }
}