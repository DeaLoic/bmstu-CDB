﻿using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.DTO;
using AccessDB.QueryBuilder.IQueryBuilder;
using AccessDB.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using Qoollo.ClickHouse.Net.Repository;

namespace AccessDB.Repositories.ClickHouse
{
    class DestinationTypesRepositoryClickHouse : CrudPlusRepositoryClickHouse<DestinationTypeDTO>, IDestinationTypesRepository
    {
        public DestinationTypesRepositoryClickHouse(ICrudQueryBuilder<DestinationTypeDTO> qbuilder, IClickHouseRepository clickHouseRepository, ILogger<DestinationTypesRepositoryClickHouse> logger, IEntityMapper<DestinationTypeDTO> mapper)
            : base(qbuilder, clickHouseRepository, logger, mapper)
        {
        }
    }
}
