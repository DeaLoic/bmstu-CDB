using System;
using System.Collections.Generic;
using System.Text;
using DataObjects.Models;
using AccessDB.DbModels.ClickHouse;
using AccessDB.DbModels.DataMappers;
using AccessDB.QueryBuilder.IQueryBuilder;
using AccessDB.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using Qoollo.ClickHouse.Net.Repository;
using System.Linq;

namespace AccessDB.Repositories.ClickHouse
{
    public class FlowsRawRepositoryClickHouse : IFlowsRawRepository
    {
        protected IFlowsRawQueryBuilder _qbuilder;
        protected IClickHouseRepository _clickHouseRepository;
        protected ILogger<FlowsRawRepositoryClickHouse> _logger;
        protected IEntityMapper<FlowClickHouse> _mapper;
        public FlowsRawRepositoryClickHouse(IFlowsRawQueryBuilder qbuilder, IClickHouseRepository clickHouseRepository, ILogger<FlowsRawRepositoryClickHouse> logger, IEntityMapper<FlowClickHouse> mapper)
        {
            _qbuilder = qbuilder;
            _clickHouseRepository = clickHouseRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public void DeleteForTime(int minutes)
        {
            string deleteQuery = _qbuilder.DeleteForTimeQuery(minutes);
            _clickHouseRepository.ExecuteNonQuery(deleteQuery);
        }

        public IEnumerable<Flow> FindAll()
        {
            string findFlowsQuery = _qbuilder.FindAllQuery();
            var flows = _clickHouseRepository.ExecuteQueryMapping(findFlowsQuery, _mapper);
            return flows.Select(c => FlowMapperDB.MapClickHouse(c));
        }

        public IEnumerable<Flow> FindForTime(int minutes)
        {
            return FindForTimePeriod(minutes, 0);
        }

        public IEnumerable<Flow> FindForTimePeriod(int minutesStart, int minutesEnd)
        {
            string findFlowsQuery = _qbuilder.FindForTimePeriodQuery(minutesStart, minutesEnd);
            var flows = _clickHouseRepository.ExecuteQueryMapping(findFlowsQuery, _mapper);
            return flows.Select(c => FlowMapperDB.MapClickHouse(c));
        }
    }
}
