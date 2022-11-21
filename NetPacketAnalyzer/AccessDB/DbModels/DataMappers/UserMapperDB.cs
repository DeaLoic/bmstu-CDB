using System;
using System.Collections.Generic;
using System.Text;
using DataObjects.Models;
using DataObjects.Enums;
using AccessDB.DbModels.PostgreSQL;

namespace AccessDB.DbModels.DataMappers
{
    static class UserMapperDB
    {
        static public User MapPostgres(CredentialPostgres cred)
        {
            return new User(cred.Login, cred.Password, RoleExtension.IntToEnum(cred.Role), cred.Id);
        }
        static public CredentialPostgres MapToPostgres(User user)
        {
            return new CredentialPostgres(user.Username, user.Password, RoleExtension.EnumToInt(user.Role), user.Id);
        }
    }
}
