using System.Data;

public class User
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string HomeAddress { get; set; }
    public Role UserRole { get; set; }
}