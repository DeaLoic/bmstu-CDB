using AccessDB.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using DataObjects.Models;
using DataObjects.Enums;
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

        public (List<Flow>, Error) FindFiltered(FlowFilters filters)
        {
            Error error = Error.OK;
            List<Flow> entities = null;
            try
            {
                entities = _flowsRawRepository.FindFiltered(filters).ToList();
            }
            catch (Exception ex)
            {
                error = Error.Internal;
            }

            return (entities, error);
        }

        public (List<Flow>, Error) FindAll()
        {
            Error error = Error.OK;
            List<Flow> entities = null;
            try
            {
                entities = _flowsRawRepository.FindAll().ToList();
            }
            catch (Exception ex)
            {
                error = Error.Internal;
            }

            return (entities, error);
        }
    }
}
