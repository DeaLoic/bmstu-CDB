using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.Enums;

namespace AccessDB.QueryBuilder.IQueryBuilder
{
    public interface IDifficultQueryBuilder
    {
        public string FindSourcesByPostThatToDestTypeMoreThanCount(string post, DestType type, int count);
    }
}