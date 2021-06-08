using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.DTO;
using AccessDB.Repositories.IRepositories;
using AccessDB.QueryBuilder.IQueryBuilder;
using Qoollo.ClickHouse.Net.Repository;
using Microsoft.Extensions.Logging;
using AccessDB.Enums;

namespace AccessDB.Repositories.ClickHouse
{
    public class DifficultRepositoryClickHouse : IDifficultRepository
    {
        protected IDifficultQueryBuilder _qbuilder;
        protected IClickHouseRepository _clickHouseRepository;
        protected ILogger _logger;
        protected IEntityMapper<DifficultSourcesByPostThatToDestTypeMoreThanCountDTO> _mapper;
        public DifficultRepositoryClickHouse(IDifficultQueryBuilder qbuilder, IClickHouseRepository clickHouseRepository, ILogger<DifficultRepositoryClickHouse> logger, IEntityMapper<DifficultSourcesByPostThatToDestTypeMoreThanCountDTO> mapper)
        {
            _qbuilder = qbuilder;
            _clickHouseRepository = clickHouseRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public IEnumerable<DifficultSourcesByPostThatToDestTypeMoreThanCountDTO> FindSourcesByPostThatToDestTypeMoreThanCount(string post, int type, int count)
        {
            string queryString = _qbuilder.FindSourcesByPostThatToDestTypeMoreThanCount(post, type, count);
            var entities = _clickHouseRepository.ExecuteQueryMapping(queryString, _mapper);
            return entities;
        }
    }
}
