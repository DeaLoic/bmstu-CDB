using System;
using System.Collections.Generic;
using System.Text;
using DataObjects.Enums;
using System.Linq;

namespace WebApplication.DTO
{
    public class UserDTO
    {
        public int Id { get; }
        public string Username { get; }
        public string Password { get; }
        public int Role { get; }

        public UserDTO(string login, string password, int role, int id=0)
        {
            Username = login;
            Password = password;
            Role = role;
            Id = id;
        }
    }
}
