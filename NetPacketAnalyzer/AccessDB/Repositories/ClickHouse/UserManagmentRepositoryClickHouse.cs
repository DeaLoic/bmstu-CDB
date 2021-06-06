using AccessDB.DTO;
using AccessDB.Repositories.IRepositories;
using AccessDB.QueryBuilder.IQueryBuilder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Qoollo.ClickHouse.Net.Repository;
using AccessDB.Tables;
using AccessDB.DTO;
using ClickHouse.Ado;
using System.Linq;
using AccessDB.Enums;

namespace AccessDB.Repositories.ClickHouse
{
    public class UserManagmentRepositoryClickHouse : IUserManagmentRepository
    {
        private IUserManagmentQueryBuilder _qbuilder;
        private IClickHouseRepository _clickHouseRepository;
        private ILogger _logger;
        private UserInfoDTOMapper _mapper = new UserInfoDTOMapper();
        public UserManagmentRepositoryClickHouse(IUserManagmentQueryBuilder qbuilder, IClickHouseRepository clickHouseRepository, ILogger<UserInfoDTO> logger)
        {
            _qbuilder = qbuilder;
            _clickHouseRepository = clickHouseRepository;
        }

        public void CreateEntityTableIfNotExists()
        {
            string createTableQuery = _qbuilder.CreateTableQuery();
            _clickHouseRepository.ExecuteNonQuery(createTableQuery);
        }

        public void CreateUser(string login, string pass)
        {
            string createUserQuery = _qbuilder.AddUserQuery(login, pass);
            _clickHouseRepository.ExecuteNonQuery(createUserQuery);
        }

        public void DeleteUser(string login)
        {
            string deleteUserQuery = _qbuilder.DropUserQuery(login);
            _clickHouseRepository.ExecuteNonQuery(deleteUserQuery);
        }

        public IEnumerable<UserInfoDTO> FindUser(string login)
        {
            string findUserQuery = _qbuilder.FindUserByLoginQuery(login);
            var user = _clickHouseRepository.ExecuteQueryMapping(findUserQuery, _mapper);
            return user;
        }
        public IEnumerable<UserInfoDTO> FindAllUsers()
        {
            string findAllUsersQuery = _qbuilder.FindAllUsersQuery();
            var users = _clickHouseRepository.ExecuteQueryMapping(findAllUsersQuery, _mapper);
            return users;
        }

        public void GrantUserRole(string login, Role role)
        {
            string grantRoleUserQuery = _qbuilder.GrantRoleUserQuery(login, role);
            _clickHouseRepository.ExecuteNonQuery(grantRoleUserQuery);
        }

        public void RevokeUserRole(string login, Role role)
        {
            string revokeRoleUserQuery = _qbuilder.RevokeRoleUserQuery(login, role);
            _clickHouseRepository.ExecuteNonQuery(revokeRoleUserQuery);
        }
    }
}
