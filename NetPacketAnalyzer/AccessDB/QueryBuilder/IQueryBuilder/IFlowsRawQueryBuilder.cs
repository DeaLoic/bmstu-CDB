using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.DTO;

namespace AccessDB.QueryBuilder.IQueryBuilder
{
    public interface IFlowsRawQueryBuilder
    {
        // period = [curTime - minutes, curTime]
        public string DeleteForTimeQuery(int minutes);
        // period = [curTime - minutesStart, curTime - minutesEnd]
        public string FindForTimePeriodQuery(int minutesStart, int minutesEnd);
        public string FindAllQuery();
    }
}
