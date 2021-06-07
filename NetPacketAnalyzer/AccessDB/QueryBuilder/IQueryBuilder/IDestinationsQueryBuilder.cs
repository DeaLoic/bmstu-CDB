using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.Enums;

namespace AccessDB.QueryBuilder.IQueryBuilder
{
    public interface IDestinationsQueryBuilder : ICrudQueryBuilder
    {
        public string AddDestinationQuery(string login, string pass);
        public string DropDestinationQuery(string login);

        public string FindDestinationQuery(string login);
        public string FindAllDestinationsQuery();
    }
}
