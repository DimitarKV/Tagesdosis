namespace Tagesdosis.Gateways.Portal.Data.Models.User;

public class UserCredentials
{
    public UserCredentials(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }

    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}