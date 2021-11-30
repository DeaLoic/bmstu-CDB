using System;
using System.Collections.Generic;
using System.Text;
using DataObjects.Enums;
using System.Linq;

namespace WebApplication.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public UserDTO(string login, string password, Role role, int id=0)
        {
            Username = login;
            Password = password;
            Role = role;
            Id = id;
        }
    }
}
