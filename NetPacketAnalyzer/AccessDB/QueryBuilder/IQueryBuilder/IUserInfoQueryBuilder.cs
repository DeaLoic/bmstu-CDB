using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.Enums;

namespace AccessDB.QueryBuilder.IQueryBuilder
{
    public interface IUserInfoQueryBuilder : ICrudQueryBuilder
    {
        public string AddUserQuery(UserInfoDTO user);
        public string DropUserQuery(string login);

        public string FindUserByUUIDQuery(string login);
        public string FindAllUsersQuery();
    }
}