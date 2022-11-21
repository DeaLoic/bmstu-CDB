using System;
using System.Collections.Generic;
using System.Text;
using DataObjects.Models;
using AccessDB.QueryBuilder.IQueryBuilder;

namespace AccessDB.QueryBuilder.ClickHouse
{
    public class FlowsRawQueryBuilder : IFlowsRawQueryBuilder
    {
        public string FindFilteredQuery(FlowFilters filters)
        {
            List<string> filtersString = new List<string> {filters.MinTimeFlowStart == null ? "" : @$"TimeFlowStart >= {filters.MinTimeFlowStart}",
                                                           filters.MaxTimeFlowStart == null ? "" : @$"TimeFlowStart <= {filters.MaxTimeFlowStart}",
                                                           filters.MinBytes == null ? "" : @$"Bytes >= {filters.MinBytes}",
                                                           filters.MaxBytes == null ? "" : @$"Bytes <= {filters.MaxBytes}",
                                                           filters.SrcAddr == null ? "" : @$"SrcAddr = {filters.SrcAddr}",
                                                           filters.DstAddr == null ? "" : @$"DstAddr = {filters.DstAddr}" };
            string filter = "";
            foreach (var filterString in filtersString)
            {
                if (filterString.Length > 0)
                {
                    if (filter.Length > 0)
                    {
                        filter += "and ";
                    }
                    filter += filterString;
                }
            }

            if (filter.Length > 0)
            {
                filter = " where " + filter;
            }

            return "SELECT * FROM flows_raw" + filter + ";";
        }
        public string FindAllQuery()
        {
            return @$"SELECT * FROM flows_raw;";
        }
    }
}
