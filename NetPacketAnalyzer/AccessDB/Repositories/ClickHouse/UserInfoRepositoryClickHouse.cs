using AccessDB.QueryBuilder.IQueryBuilder;
using Microsoft.Extensions.Logging;
using Qoollo.ClickHouse.Net.Repository;
using AccessDB.DTO;
using AccessDB.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.Repositories.ClickHouse
{
    public class UserInfoRepositoryClickHouse : CrudPlusRepositoryClickHouse<UserInfoDTO>, IUserInfoRepository
    {
        public UserInfoRepositoryClickHouse(IUserInfoQueryBuilder qbuilder, IClickHouseRepository clickHouseRepository, ILogger<UserInfoRepositoryClickHouse> logger, IEntityMapper<UserInfoDTO> mapper) : base(qbuilder, clickHouseRepository, logger, mapper)
        {
        }
    }
}
