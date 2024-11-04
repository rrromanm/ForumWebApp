namespace DTOs.User;

public class UserDTO
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public UserDTO(string username, string password)
    {
        Username = username;
        Password = password;
    }
}