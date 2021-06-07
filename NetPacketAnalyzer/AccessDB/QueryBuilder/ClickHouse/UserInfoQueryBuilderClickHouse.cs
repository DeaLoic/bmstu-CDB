using AccessDB.Enums;
using AccessDB.QueryBuilder.IQueryBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.QueryBuilder.ClickHouse
{
    public class UserInfoQueryBuilderClickHouse : IUserInfoQueryBuilder
    {
        public string CreateTableQuery()
        {
            return @"   CREATE TABLE IF NOT EXISTS user_info (
                        Id UUID,
                        Name String,
                        Post String,
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Id);";
        }
        public string AddUserQuery(UserInfoDTO users)
        {
            return @$"INSERT INTO user_info (*) VALUES {users.UUID}, '{users.Name}', '{users.Post}';";
        }
        public string DropUserQuery(UserInfoDTO users)
        {
            return @$"ALTER TABLE IF EXISTS user_info DELETE WHERE ID = {users.UUID}";
        }
        public string FindUserByUUIDQuery(UserInfoDTO users)
        {
            return @$"SELECT * FROM user_info WHERE ID = {users.UUID}";
        }
        public string FindAllUsersQuery()
        {
            return @$"SELECT * FROM user_info";
        }
    }
}