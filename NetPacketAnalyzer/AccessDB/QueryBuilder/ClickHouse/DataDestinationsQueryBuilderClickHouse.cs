using AccessDB.Enums;
using AccessDB.QueryBuilder.IQueryBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.QueryBuilder.ClickHouse
{
    public class DataDestinationsQueryBuilderClickHouse : IDataDestinationsQueryBuilder
    {
        public string CreateTableQuery()
        {
            return @"   CREATE TABLE IF NOT EXISTS data_destinations (
                        Ip String,
                        Type Int8
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Ip);";
        }
        public string AddSourceQuery(DataDestinationDTO dataSource)
        {
            return @$"INSERT INTO data_destinations (*) VALUES '{dataDestination.Ip}', {dataDestination.Type};";
        }

        public string DeleteSourceQuery(DataDestinationDTO dataDestination)
        {
            return @$"ALTER TABLE IF EXISTS data_destinations DELETE WHERE Ip = '{dataDestination.Ip}'";
        }

        public string FindDestinationQuery(DataDestinationDTO dataDestination)
        {
            return @$"SELECT * from data_destinations WHERE Ip = '{dataDestination.Ip}'";
        }
        public string FindAllDestinationsQuery()
        {
            return @$"SELECT * from data_destinations";
        }
    }
}