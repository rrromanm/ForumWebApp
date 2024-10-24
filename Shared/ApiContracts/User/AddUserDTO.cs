namespace DTOs.User;

public class AddUserDTO
{
    public required string username { get; set; }
    public required string password { get; set; }
    public int Id { get; set; }
}