using System;
using System.Collections.Generic;
using System.Text;
using DataObjects.Models;

namespace AccessDB.QueryBuilder.IQueryBuilder
{
    public interface IFlowsRawQueryBuilder
    {
        public string FindFilteredQuery(FlowFilters filters);
        public string FindAllQuery();
    }
}
