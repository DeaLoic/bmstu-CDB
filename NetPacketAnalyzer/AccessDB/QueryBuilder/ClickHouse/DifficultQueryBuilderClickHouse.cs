using AccessDB.Enums;
using AccessDB.QueryBuilder.IQueryBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.QueryBuilder.ClickHouse
{
    public class DifficultQueryBuilderClickHouse : IDifficultQueryBuilder
    {
        public string FindSourcesByPostThatToDestTypeMoreThanCount(string post, DestType type, int count)
        {
            return ";";
        }
    }
}