using System;
using System.Collections.Generic;
using System.Text;
using Qoollo.ClickHouse.Net.Repository;
using DataObjects.Models;
using DataObjects.Enums;

namespace AccessDB.Repositories.IRepositories
{
    public interface ICredentialsRepository
    {
        public void CreateUser(User user);
        public void DeleteUser(string login);
        public User FindUser(string login);
        public IEnumerable<User> FindAllUsers();
        public void GrantUserRole(string login, Role role);

    }
}
