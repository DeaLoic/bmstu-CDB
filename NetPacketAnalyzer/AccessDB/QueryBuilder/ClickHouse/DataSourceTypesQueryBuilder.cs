using AccessDB.Enums;
using AccessDB.QueryBuilder.IQueryBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.QueryBuilder.ClickHouse
{
    public class DataSourceTypesQueryBuilderClickHouse : IDataSourceTypesQueryBuilder
    {
        public string CreateTableQuery()
        {
            return @"   CREATE TABLE IF NOT EXISTS data_source_types (
                        Type Int8,
                        Info String
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Type);";
        }
        public string AddTypeQuery(DataSourceTypeDTO dataType)
        {
            return @$"INSERT INTO data_source_types (*) VALUES {dataType.Type}, '{dataType.String}';";
        }

        public string DeleteTypeQuery(DataSourceTypeDTO dataType)
        {
            return @$"ALTER TABLE IF EXISTS data_source_types DELETE WHERE Type = {dataType.Type}";
        }

        public string FindTypeQuery(DataSourceTypeDTO dataType)
        {
            return @$"SELECT * from data_source_types WHERE Type = {dataType.Type}";
        }
        public string FindAllTypesQuery()
        {
            return @$"SELECT * from data_source_types";
        }
    }
}
