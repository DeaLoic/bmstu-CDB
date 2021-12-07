using DataObjects.Enums;

namespace WebApplication.DTO
{
    public class UserCreateDTO
    {
        public string Username { get; }
        public string Password { get; }
        public Role Role { get; }

        public UserCreateDTO(string login, string password, int role)
        {
            Username = login;
            Password = password;
            Role = Role.Admin;
        }
    }
}