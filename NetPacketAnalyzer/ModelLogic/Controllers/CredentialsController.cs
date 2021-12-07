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

        public (List<User>, Error) FindAllUsers()
        {
            Error error = Error.OK;
            List<User> entities = null;
            try
            {
                entities = _credentialRepository.FindAllUsers().ToList();
            }
            catch (Exception ex)
            {
                error = Error.Internal;
            }

            return (entities, error);
        }

        public (User, Error) FindUserByLogin(string login)
        {
            Error error = Error.OK;
            User entity = null;
            try
            {
                entity = _credentialRepository.FindUser(login);
            }
            catch (Exception ex)
            {
                error = Error.Internal;
            }

            return (entity, error);
        }

        public Error CreateUser(string login, string pass, Role role)
        {
            Error error = Error.OK;
            try
            {
                _credentialRepository.CreateUser(new User(login, pass, role));
            }
            catch (Exception ex)
            {
                error = Error.Internal;
            }

            return error;
        }

        public Error DeleteUser(string login)
        {
            Error error = Error.OK;
            try
            {
                _credentialRepository.DeleteUser(login);
            }
            catch (Exception ex)
            {
                error = Error.Internal;
            }

            return error;
        }
        public Error GrantUserRole(string login, Role role)
        {
            Error error = Error.OK;
            try
            {
                _credentialRepository.GrantUserRole(login, role);
            }
            catch (Exception ex)
            {
                error = Error.Internal;
            }

            return error;
            
        }
    }
}
