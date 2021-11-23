using AccessDB.QueryBuilder.IQueryBuilder;
using Microsoft.Extensions.Logging;
using Qoollo.ClickHouse.Net.Repository;
using AccessDB.DTO;
using AccessDB.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.DbModels.PostgreSQL;
using System.Linq;

namespace AccessDB.Repositories.PostgreSQL
{
    public class UserInfoRepositoryPostgreSQL : IUserInfoRepository, IDisposable
    {
        defaultContext _context;
        ILogger<UserInfoRepositoryPostgreSQL> _logger;
        public UserInfoRepositoryPostgreSQL(defaultContext context, ILogger<UserInfoRepositoryPostgreSQL> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Add(UserInfoDTO dto)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserInfoDTO dto)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<UserInfoDTO> Find(UserInfoDTO dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserInfoDTO> FindAll()
        {
            return _context.UserInfos.Select(w => new UserInfoDTO(w));
        }
    }
}
