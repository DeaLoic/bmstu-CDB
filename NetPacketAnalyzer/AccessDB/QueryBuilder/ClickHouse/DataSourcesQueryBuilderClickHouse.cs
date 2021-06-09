using AccessDB.DTO;
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
                        Type Int16
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Ip);";
        }
        public string AddQuery(DataSourceDTO dataSource)
        {
            return @$"INSERT INTO data_sources SELECT '{dataSource.Ip}', {dataSource.OwnerUUID}, {dataSource.Type};";
        }

        public string DeleteQuery(DataSourceDTO dataSource)
        {
            return @$"ALTER TABLE IF EXISTS data_sources DELETE WHERE Ip = '{dataSource.Ip}'";
        }

        public string FindQuery(DataSourceDTO dataSource)
        {
            return @$"SELECT * from data_sources WHERE Ip = '{dataSource.Ip}'";
        }
        public string FindAllQuery()
        {
            return @$"SELECT * from data_sources";
        }
    }
}
