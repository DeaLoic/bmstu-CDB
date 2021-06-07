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
    class DataSourceTypesRepositoryClickHouse : CrudPlusRepositoryClickHouse<SourceTypeDTO>, IDataSourceTypesRepository
    {
        public DataSourceTypesRepositoryClickHouse(ICrudQueryBuilder<SourceTypeDTO> qbuilder, IClickHouseRepository clickHouseRepository, ILogger<DataSourceTypesRepositoryClickHouse> logger, IEntityMapper<SourceTypeDTO> mapper)
            : base(qbuilder, clickHouseRepository, logger, mapper)
        {
        }
    }
}
