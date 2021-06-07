using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.QueryBuilder.IQueryBuilder
{
    public interface ICrudQueryBuilder<DTO>
    {
        string CreateTableQuery();

        public string AddQuery(DTO dto);
        public string DeleteQuery(DTO dto);
        public string FindQuery(DTO dto);
        public string FindAllQuery();
    }
}
