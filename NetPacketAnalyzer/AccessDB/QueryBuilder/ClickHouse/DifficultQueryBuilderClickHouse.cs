using AccessDB.Enums;
using AccessDB.QueryBuilder.IQueryBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.QueryBuilder.ClickHouse
{
    public class DifficultQueryBuilderClickHouse : IDifficultQueryBuilder
    {
        public string FindSourcesByPostThatToDestTypeMoreThanCount(string post, int type, int count)
        {
            return "Select SrcAddr from flows_raw where";
        }

        public string GetTraficCountPerSource(int minutes)
        {
            return @$"WITH subtractMinutes(now(), {minutes}) as startTime
                       SELECT DstAddr, sum(Bytes) FROM flows_raw where TimeFlowStart >= startTime GROUP By DstAddr;";
        }
        public string GetMaxSpendingDayQuery()
        {
            return "SELECT \"Date\", SrcAddr, sum(Bytes) FROM flows_raw;";
        }
    }
}