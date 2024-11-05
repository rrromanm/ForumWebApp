namespace DTOs.User;

public class AddUserDTO
{
    public string username { get; set; }
    public string password { get; set; }
    public int Id { get; set; }
    
    public AddUserDTO(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}