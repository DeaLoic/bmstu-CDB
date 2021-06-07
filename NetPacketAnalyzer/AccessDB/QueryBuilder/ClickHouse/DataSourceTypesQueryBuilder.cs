using AccessDB.Enums;
using AccessDB.QueryBuilder.IQueryBuilder;
using AccessDB.DTO;
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
                        Type Int16,
                        Info String
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Type);";
        }
        public string AddQuery(SourceTypeDTO dataType)
        {
            return @$"INSERT INTO data_source_types (*) VALUES {dataType.Type}, '{dataType.CommentString}';";
        }

        public string DeleteQuery(SourceTypeDTO dataType)
        {
            return @$"ALTER TABLE IF EXISTS data_source_types DELETE WHERE Type = {dataType.Type}";
        }

        public string FindQuery(SourceTypeDTO dataType)
        {
            return @$"SELECT * from data_source_types WHERE Type = {dataType.Type}";
        }
        public string FindAllQuery()
        {
            return @$"SELECT * from data_source_types";
        }
    }
}
