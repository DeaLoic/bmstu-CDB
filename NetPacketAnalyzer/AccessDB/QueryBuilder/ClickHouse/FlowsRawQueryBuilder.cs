using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.QueryBuilder.IQueryBuilder;

namespace AccessDB.QueryBuilder.ClickHouse
{
    public class FlowsRawQueryBuilder : IFlowsRawQueryBuilder
    {
        // period = [curTime - minutes, curTime]
        public string DeleteForTimeQuery(int minutes)
        {
            return @$"ALTER TABLE IF EXISTS flows_raw DELETE WHERE TimeFlowStart >= subtractMinutes(now(), {minutes})";
        }
        // period = [curTime - minutesStart, curTime - minutesEnd]
        public string FindForTimePeriodQuery(int minutesStart, int minutesEnd)
        {
            return @$"WITH subtractMinutes(now(), {minutesStart}) as startTime,
                            subtractMinutes(now(), {minutesEnd}) as endTime
                      SELECT * FROM flows_raw where TimeFlowStart >= startTime and TimeFlowStart <= endTime;";
        }
        public string FindAllQuery()
        {
            return @$"SELECT * FROM flows_raw";
        }
    }
}
