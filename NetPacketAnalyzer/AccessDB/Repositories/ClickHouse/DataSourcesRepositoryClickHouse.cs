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
    class DataSourcesRepositoryClickHouse : CrudPlusRepositoryClickHouse<DataSourceDTO>, IDataSourcesRepository
    {
        public DataSourcesRepositoryClickHouse(ICrudQueryBuilder<DataSourceDTO> qbuilder, IClickHouseRepository clickHouseRepository, ILogger<DataSourcesRepositoryClickHouse> logger, IEntityMapper<DataSourceDTO> mapper)
            : base(qbuilder, clickHouseRepository, logger, mapper)
        {
        }
    }
}
