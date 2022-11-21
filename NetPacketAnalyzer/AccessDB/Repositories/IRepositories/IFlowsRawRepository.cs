using System;
using System.Collections.Generic;
using System.Text;
using DataObjects.Models;

namespace AccessDB.Repositories.IRepositories
{
    public interface IFlowsRawRepository
    {
        public IEnumerable<Flow> FindFiltered(FlowFilters filters);
        public IEnumerable<Flow> FindAll();
    }
}
