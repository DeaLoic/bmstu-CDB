using AccessDB.DTO;
using AccessDB.Enums;
using AccessDB.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using ModelLogic.Models;
using Qoollo.ClickHouse.Net.ConnectionPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelLogic.Controllers
{
    public class AdminController : UserController
    {
        protected IUserManagmentRepository _userManagmentRepository;

        public AdminController(IDataSourcesRepository dataSourcesRepository, IDataSourceTypesRepository dataSourceTypesRepository,
            IDestinationsRepository destinationsRepository, IDestinationTypesRepository destinationTypesRepository,
            IUserInfoRepository userInfoRepository, IUserManagmentRepository userManagmentRepository, ILogger<AdminController> logger) :
            base(dataSourcesRepository, dataSourceTypesRepository, destinationsRepository, destinationTypesRepository, userInfoRepository, logger)
        {
            _userManagmentRepository = userManagmentRepository;
        }
        public List<LoginInfo> FindAllSystemUsers()
        {
            var entities = new List<SystemUserDTO>();
            entities = _userManagmentRepository.FindAllUsers().ToList();

            return entities.Select((dto) => new LoginInfo(dto)).ToList();
        }

        public LoginInfo FindSystemUserByLogin(string login)
        {
            LoginInfo entity = null;
            var entities = _userManagmentRepository.FindUser(login).ToList();
            if (entities.Count() > 0)
            {
                entity = new LoginInfo(entities[0]);
            }

            return entity;
        }

        public void CreateSystemUser(string login, string pass)
        {
            _userManagmentRepository.CreateUser(login, pass); 
        }

        public void DeleteSystemUser(string login)
        {
            _userManagmentRepository.DeleteUser(login);
        }
        public void GrantUserRole(string login, Role role)
        {
            _userManagmentRepository.GrantUserRole(login, role);
        }

        public void RevokeUserRole(string login, Role role)
        {
            _userManagmentRepository.RevokeUserRole(login, role);
        }

    }
}
