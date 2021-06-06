using AccessDB.Enums;
using AccessDB.QueryBuilder.IQueryBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.QueryBuilder.ClickHouse
{
    public class UserManagmentQueryBuilderClickHouse : IUserManagmentQueryBuilder
    {
        public string CreateTableQuery()
        {
            /*
            return @"   CREATE TABLE IF NOT EXISTS user_info (
                        Id UUID,
                        Login String,
                        Hash String,
                        Permission Int8
                        )
                        ENGINE=MergeTree()
                        ORDER BY (Login);";
                        */
            return ";";
        }
        public string AddUserQuery(string login, string pass)
        {
            return @$"CREATE user IF NOT EXISTS {login} WITH sha256_password BY '{pass}';";
        }
        public string DropUserQuery(string login)
        {
            return @$"DROP user IF EXISTS {login}";
        }
        public string FindUserByLoginQuery(string login)
        {
            return @$"select user_name, groupArray(granted_role_name) from system.role_grants where user_name = '{login}' group by user_name;";
        }
        public string FindAllUsersQuery()
        {
            return @$"select user_name, groupArray(granted_role_name) from system.role_grants group by user_name;";
        }
        public string RevokeRoleUserQuery(string login, Role role)
        {
            return @$"revoke {RoleExtension.RoleEnumToString(role)} from {login};";
        }

        public string GrantRoleUserQuery(string login, Role role)
        {
            return @$"grant {RoleExtension.RoleEnumToString(role)} to {login};";
        }
    }
}
