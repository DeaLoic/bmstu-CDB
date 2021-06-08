using System;
using System.Collections.Generic;
using System.Text;
using Qoollo.ClickHouse.Net.Repository;
using AccessDB.DTO;
using AccessDB.Enums;

namespace AccessDB.Repositories.IRepositories
{
    public interface IUserManagmentRepository
    {
        public void CreateUser(string login, string pass);
        public void DeleteUser(string login);
        public IEnumerable<SystemUserDTO> FindUser(string login);
        public IEnumerable<SystemUserDTO> FindAllUsers();
        public void GrantUserRole(string login, Role role);
        public void RevokeUserRole(string login, Role role);
        public IEnumerable<RoleDTO> GetCurrentRoles();

    }
}
