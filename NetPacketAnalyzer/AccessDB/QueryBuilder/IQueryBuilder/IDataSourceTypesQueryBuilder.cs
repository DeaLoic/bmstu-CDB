using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.Enums;

namespace AccessDB.QueryBuilder.IQueryBuilder
{
    public interface IDataSourceTypesQueryBuilder : ICrudQueryBuilder
    {
        public string AddTypeQuery(string login, string pass);
        public string DropTypeQuery(string login);

        public string FindTypeQuery(string login);
        public string FindAllTypesQuery();
    }
}
