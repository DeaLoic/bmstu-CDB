using AccessDB.Enums;
using AccessDB.QueryBuilder.IQueryBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.QueryBuilder.ClickHouse
{
    public class DataSourcesQueryBuilderClickHouse : IDataSourcesQueryBuilder
    {
        public string CreateTableQuery()
        {
            return @"   CREATE TABLE IF NOT EXISTS data_sources (
                        Ip String,
                        OwnerUUID UUID,
                        Type Int8
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Ip);";
        }
        public string AddSourceQuery(DataSourceDTO dataSource)
        {
            return @$"INSERT INTO data_sources (*) VALUES '{dataSource.Ip}', {dataSource.OwnerUUID}, {dataSource.Type};";
        }

        public string DeleteSourceQuery(DataSourceDTO dataSource)
        {
            return @$"ALTER TABLE IF EXISTS data_sources DELETE WHERE Ip = '{dataSource.Ip}'";
        }

        public string FindSourceQuery(DataSourceDTO dataSource)
        {
            return @$"SELECT * from data_sources WHERE Ip = '{dataSource.Ip}'";
        }
        public string FindAllSourcesQuery()
        {
            return @$"SELECT * from data_sources";
        }
    }
}
