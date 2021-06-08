using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.DTO;
using AccessDB.QueryBuilder.IQueryBuilder;
using AccessDB.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using Qoollo.ClickHouse.Net.Repository;

namespace AccessDB.Repositories.ClickHouse
{
    public class DataSourcesRepositoryClickHouse : CrudPlusRepositoryClickHouse<DataSourceDTO>, IDataSourcesRepository
    {
        public DataSourcesRepositoryClickHouse(IDataSourcesQueryBuilder qbuilder, IClickHouseRepository clickHouseRepository, ILogger<DataSourcesRepositoryClickHouse> logger, IEntityMapper<DataSourceDTO> mapper)
            : base(qbuilder, clickHouseRepository, logger, mapper)
        {
        }
    }
}
