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

        public List<Flow> FindFiltered(FlowFilters filters)
        {
            var entities = _flowsRawRepository.FindFiltered(filters).ToList();

            return entities;
        }

        public List<Flow> FindAll()
        {
            var entities = _flowsRawRepository.FindAll().ToList();

            return entities;
        }
    }
}
