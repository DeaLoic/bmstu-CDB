using AccessDB.Enums;
using AccessDB.QueryBuilder.IQueryBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.QueryBuilder.ClickHouse
{
    public class DataDestinationTypesQueryBuilderClickHouse : IDataDestinationTypesQueryBuilder
    {
        public string CreateTableQuery()
        {
            return @"   CREATE TABLE IF NOT EXISTS data_destination_types (
                        Type Int8,
                        Info String
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Type);";
        }
        public string AddTypeQuery(DataDestinationTypeDTO dataType)
        {
            return @$"INSERT INTO data_destination_types (*) VALUES {dataType.Type}, '{dataType.String}';";
        }

        public string DeleteTypeQuery(DataDestinationTypeDTO dataType)
        {
            return @$"ALTER TABLE IF EXISTS data_destination_types DELETE WHERE Type = {dataType.Type}";
        }

        public string FindTypeQuery(DataDestinationTypeDTO dataType)
        {
            return @$"SELECT * from data_destination_types WHERE Type = {dataType.Type}";
        }
        public string FindAllTypesQuery()
        {
            return @$"SELECT * from data_destination_types";
        }
    }
}
