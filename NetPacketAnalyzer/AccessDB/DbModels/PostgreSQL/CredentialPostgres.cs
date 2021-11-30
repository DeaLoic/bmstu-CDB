using System;
using System.Collections.Generic;

#nullable disable

namespace AccessDB.DbModels.PostgreSQL
{
    public partial class CredentialPostgres
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }

        public CredentialPostgres(string login, string password, int role, int id = 0)
        {
            Id = id;
            Login = login;
            Password = password;
            Role = role;
        }
    }
}
