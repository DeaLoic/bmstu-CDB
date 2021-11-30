using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Qoollo.ClickHouse.Net.ConnectionPool;
using System;
using AccessDB.Repositories.IRepositories;
using DataObjects.Enums;
using DataObjects.Models;

namespace ModelLogic.Controllers
{
    public class CredentialsController
    {
        protected ICredentialsRepository _credentialRepository;

        public CredentialsController(ICredentialsRepository credentialRepository)
        {
            _credentialRepository = credentialRepository;
        }

        public List<User> FindAllUsers()
        {
            var entities = _credentialRepository.FindAllUsers().ToList();

            return entities;
        }

        public User FindUserByLogin(string login)
        {
            User entity = _credentialRepository.FindUser(login);

            return entity;
        }

        public void CreateUser(string login, string pass, Role role)
        {
            _credentialRepository.CreateUser(new User(login, pass, role)); 
        }

        public void DeleteUser(string login)
        {
            _credentialRepository.DeleteUser(login);
        }
        public void GrantUserRole(string login, Role role)
        {
            _credentialRepository.GrantUserRole(login, role);
        }
    }
}
