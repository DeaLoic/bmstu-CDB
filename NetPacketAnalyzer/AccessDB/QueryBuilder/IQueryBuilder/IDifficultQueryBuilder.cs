using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.Enums;

namespace AccessDB.QueryBuilder.IQueryBuilder
{
    public interface IDifficultQueryBuilder
    {
        public string FindSourcesByPostThatToDestTypeMoreThanCount(string post, int type, int count);
        public string GetTraficCountPerSource(int minutes);
        public string GetMaxSpendingDayQuery();
    }
}