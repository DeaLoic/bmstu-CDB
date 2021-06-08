using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.Enums;

namespace AccessDB.QueryBuilder.IQueryBuilder
{
    public interface IUserManagmentQueryBuilder
    {
        public string AddUserQuery(string login, string pass);
        public string DropUserQuery(string login);

        public string FindUserByLoginQuery(string login);
        public string FindAllUsersQuery();

        public string RevokeRoleUserQuery(string login, Role role);
        public string GrantRoleUserQuery(string login, Role role);
        public string CurrentRolesQuery();
    }
}
