namespace OLAPPortifolio.Models;

public class UserRegisterDTO
{
    public int Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool IsActive { get; private set; } 
    public bool IsAdmin { get; private set; }
    public string Expiration { get; private set; }
    public string JwtToken { get; private set; }
    public string Document { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected UserRegisterDTO() { }

    public UserRegisterDTO(int id, string username, string email, string password, bool isActive, bool isAdmin, string expiration, string jwtToken, string document, DateTime createdAt)
    {
        Id = id;
        Username = username;
        Email = email;
        Password = password;
        IsActive = true;
        IsAdmin = false;
        Expiration = expiration;
        JwtToken = jwtToken;
        Document = document;
        CreatedAt = DateTime.Now;
    }

    public void Deactivate() => IsActive = false;
}
