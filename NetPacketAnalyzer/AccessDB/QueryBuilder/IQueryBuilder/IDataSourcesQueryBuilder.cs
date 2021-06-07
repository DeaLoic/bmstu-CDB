using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.DTO;

namespace AccessDB.QueryBuilder.IQueryBuilder
{
    public interface IDataSourcesQueryBuilder : ICrudQueryBuilder
    {
        public string AddSourceQuery(string login, string pass);
        public string DropSourceQuery(string login);

        public string FindSourceQuery(string login);
        public string FindAllSourcesQuery();
    }
}
