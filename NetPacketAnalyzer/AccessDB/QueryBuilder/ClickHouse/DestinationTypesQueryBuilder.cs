using AccessDB.DTO;
using AccessDB.QueryBuilder.IQueryBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.QueryBuilder.ClickHouse
{
    public class DestinationTypesQueryBuilderClickHouse : IDestinationTypesQueryBuilder
    {
        public string CreateTableQuery()
        {
            return @"   CREATE TABLE IF NOT EXISTS data_destination_types (
                        Type Int16,
                        Info String
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Type);";
        }
        public string AddQuery(DestinationTypeDTO dataType)
        {
            return @$"INSERT INTO data_destination_types (*) VALUES {dataType.Type}, '{dataType.CommentString}';";
        }

        public string DeleteQuery(DestinationTypeDTO dataType)
        {
            return @$"ALTER TABLE IF EXISTS data_destination_types DELETE WHERE Type = {dataType.Type}";
        }

        public string FindQuery(DestinationTypeDTO dataType)
        {
            return @$"SELECT * from data_destination_types WHERE Type = {dataType.Type}";
        }
        public string FindAllQuery()
        {
            return @$"SELECT * from data_destination_types";
        }
    }
}
