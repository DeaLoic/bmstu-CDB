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
    class DestinationsRepositoryClickHouse : CrudPlusRepositoryClickHouse<DestinationDTO>, IDestinationsRepository
    {
        public DestinationsRepositoryClickHouse(ICrudQueryBuilder<DestinationDTO> qbuilder, IClickHouseRepository clickHouseRepository, ILogger<DestinationsRepositoryClickHouse> logger, IEntityMapper<DestinationDTO> mapper)
            : base(qbuilder, clickHouseRepository, logger, mapper)
        {
        }
    }
}
