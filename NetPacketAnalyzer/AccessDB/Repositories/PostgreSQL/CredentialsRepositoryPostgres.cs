using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccessDB.DbModels.PostgreSQL;
using AccessDB.DbModels.DataMappers;
using AccessDB.QueryBuilder.IQueryBuilder;
using AccessDB.Repositories.IRepositories;
using DataObjects.Models;
using DataObjects.Enums;
using Microsoft.Extensions.Logging;

namespace AccessDB.Repositories.PostgreSQL
{
    public class CredentialsRepositoryPostgres : ICredentialsRepository, IDisposable
    {
        defaultContext _context;
        public CredentialsRepositoryPostgres(defaultContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            CredentialPostgres element = UserMapperDB.MapToPostgres(user);
            try
            {
                _context.Credentials.Add(element);
                _context.SaveChanges();
            }
            catch (Exception ex) { }
        }

        public void DeleteUser(string login)
        {
            var userToRemove = _context.Credentials.SingleOrDefault(x => x.Login == login); //returns a single item.

            if (userToRemove != null)
            {
                try
                {
                    _context.Credentials.Remove(userToRemove);
                    _context.SaveChanges();
                }
                catch (Exception ex) { }
            }            
        }

        public User FindUser(string login)
        {
            User user = null;
            try
            {
                user = UserMapperDB.MapPostgres(_context.Credentials.Where((c) => c.Login == login).First());
            }
            catch (Exception ex) { }
            return user;
        }
        public IEnumerable<User> FindAllUsers()
        {
            IEnumerable<User> users = null;
            try
            {
                users = _context.Credentials.Select(c => UserMapperDB.MapPostgres(c));
            }
            catch (Exception ex) { }
            return users;
        }

        public void GrantUserRole(string login, Role role)
        {
            User user = null;
            try
            {
                user = UserMapperDB.MapPostgres(_context.Credentials.Where((c) => c.Login == login).First());
                user.Role = role;
                _context.SaveChanges();
            }
            catch (Exception ex) { }
        }

        public void Dispose() {}
    }
}
