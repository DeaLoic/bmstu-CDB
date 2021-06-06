using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.Enums
{
    public enum Role
    {
        Guest,
        Analyst,
        Admin,
        Error
    }
    public static class RoleExtension
    {
        public static string RoleEnumToString(Role role)
        {
            var roleString = "";
            switch (role)
            {
                case Role.Guest:
                    roleString = "guest";
                    break;
                case Role.Analyst:
                    roleString = "analyst";
                    break;
                case Role.Admin:
                    roleString = "admin";
                    break;
            }
            return roleString;
        }

        public static Role RoleStringToEnum(string role)
        {
            var roleEnum = Role.Error;
            if (role.Equals("guest"))
            {
                roleEnum = Role.Guest;
            }
            else if (role.Equals("analyst"))
            {
                roleEnum = Role.Analyst;
            }
            else if (role.Equals("admin"))
            {
                roleEnum = Role.Admin;
            }
            return roleEnum;
        }
    }

}
