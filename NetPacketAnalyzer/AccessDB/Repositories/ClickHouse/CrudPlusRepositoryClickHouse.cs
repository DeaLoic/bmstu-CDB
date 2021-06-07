using System;
using System.Collections.Generic;
using System.Text;
using Qoollo.ClickHouse.Net.Repository;
using Microsoft.Extensions.Logging;
using AccessDB.QueryBuilder.IQueryBuilder;
using AccessDB.Repositories.IRepositories;

namespace AccessDB.Repositories.ClickHouse
{
    public class CrudPlusRepositoryClickHouse<DTO> : CrudRepositoryClickHouse<DTO>, ICrudPlusRepository<DTO>
    {
        public CrudPlusRepositoryClickHouse(ICrudQueryBuilder<DTO> qbuilder, IClickHouseRepository clickHouseRepository, ILogger logger, IEntityMapper<DTO> mapper) :
            base(qbuilder, clickHouseRepository, logger, mapper)
        {
        }

        public void CreateEntityTableIfNotExists()
        {
            string createTableQuery = _qbuilder.CreateTableQuery();
            _clickHouseRepository.ExecuteNonQuery(createTableQuery);
        }
    }
}
