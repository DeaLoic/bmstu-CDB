using AccessDB.Enums;
using AccessDB.DTO;
using AccessDB.QueryBuilder.IQueryBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.QueryBuilder.ClickHouse
{
    public class DestinationsQueryBuilderClickHouse : IDestinationsQueryBuilder
    {
        public string CreateTableQuery()
        {
            return @"   CREATE TABLE IF NOT EXISTS data_destinations (
                        Ip String,
                        Type Int16
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Ip);";
        }
        public string AddQuery(DestinationDTO dataDestination)
        {
            return @$"INSERT INTO data_destinations (*) VALUES '{dataDestination.Ip}', {dataDestination.Type};";
        }

        public string DeleteQuery(DestinationDTO dataDestination)
        {
            return @$"ALTER TABLE IF EXISTS data_destinations DELETE WHERE Ip = '{dataDestination.Ip}'";
        }

        public string FindQuery(DestinationDTO dataDestination)
        {
            return @$"SELECT * from data_destinations WHERE Ip = '{dataDestination.Ip}'";
        }
        public string FindAllQuery()
        {
            return @$"SELECT * from data_destinations";
        }
    }
}