using AccessDB.DTO;
using AccessDB.Repositories.IRepositories;
using AccessDB.QueryBuilder.IQueryBuilder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Qoollo.ClickHouse.Net.Repository;
using ClickHouse.Ado;
using System.Linq;
using AccessDB.Enums;

namespace AccessDB.Repositories.PostgreSQL
{
    public class UserManagmentRepositoryPostgreSQL : IUserManagmentRepository
    {
        protected IUserManagmentQueryBuilder _qbuilder;
        protected IClickHouseRepository _clickHouseRepository;
        protected ILogger<UserManagmentRepositoryPostgreSQL> _logger;
        protected IEntityMapper<SystemUserDTO> _mapper;
        public UserManagmentRepositoryPostgreSQL(IUserManagmentQueryBuilder qbuilder, IClickHouseRepository clickHouseRepository, ILogger<UserManagmentRepositoryPostgreSQL> logger, IEntityMapper<SystemUserDTO> mapper)
        {
            _qbuilder = qbuilder;
            _clickHouseRepository = clickHouseRepository;
            _logger = logger;
            _mapper = mapper;
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

        public IEnumerable<SystemUserDTO> FindUser(string login)
        {
            string findUserQuery = _qbuilder.FindUserByLoginQuery(login);
            var user = _clickHouseRepository.ExecuteQueryMapping(findUserQuery, _mapper);
            return user;
        }
        public IEnumerable<SystemUserDTO> FindAllUsers()
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

        public IEnumerable<RoleDTO> GetCurrentRoles()
        {
            string currentRolesQuery = _qbuilder.CurrentRolesQuery();
            return _clickHouseRepository.ExecuteQueryMapping(currentRolesQuery, new RoleDTOMapper());
        }
    }
}
