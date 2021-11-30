using AccessDB.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using DataObjects.Models;
using Qoollo.ClickHouse.Net.ConnectionPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelLogic.Controllers
{
    public class FlowsRawController
    {
        protected IFlowsRawRepository _flowsRawRepository;

        public FlowsRawController(IFlowsRawRepository flowsRawRepository)
        {
            _flowsRawRepository = flowsRawRepository;
        }
        public List<Flow> FindFlowByMinutes(int minutes)
        {
            var entities = _flowsRawRepository.FindForTime(minutes).ToList();

            return entities;
        }

        public List<Flow> FindForTimePeriod(int start, int stop)
        {
            var entities = _flowsRawRepository.FindForTimePeriod(start, stop).ToList();

            return entities;
        }
    }
}
