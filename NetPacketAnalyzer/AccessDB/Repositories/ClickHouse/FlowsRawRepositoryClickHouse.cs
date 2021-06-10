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
    public class FlowsRawRepositoryClickHouse : IFlowsRawRepository
    {
        protected IFlowsRawQueryBuilder _qbuilder;
        protected IClickHouseRepository _clickHouseRepository;
        protected ILogger<FlowsRawRepositoryClickHouse> _logger;
        protected IEntityMapper<FlowDTO> _mapper;
        public FlowsRawRepositoryClickHouse(IFlowsRawQueryBuilder qbuilder, IClickHouseRepository clickHouseRepository, ILogger<FlowsRawRepositoryClickHouse> logger, IEntityMapper<FlowDTO> mapper)
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

        public IEnumerable<FlowDTO> FindAll()
        {
            string findFlowsQuery = _qbuilder.FindAllQuery();
            var flows = _clickHouseRepository.ExecuteQueryMapping(findFlowsQuery, _mapper);
            return flows;
        }

        public IEnumerable<FlowDTO> FindForTime(int minutes)
        {
            return FindForTimePeriod(minutes, 0);
        }

        public IEnumerable<FlowDTO> FindForTimePeriod(int minutesStart, int minutesEnd)
        {
            string findFlowsQuery = _qbuilder.FindForTimePeriodQuery(minutesStart, minutesEnd);
            var flows = _clickHouseRepository.ExecuteQueryMapping(findFlowsQuery, _mapper);
            return flows;
        }
    }
}
