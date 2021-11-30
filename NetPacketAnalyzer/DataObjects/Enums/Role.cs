using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjects.Enums
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
        public static Role IntToEnum(int role)
        {
            var roleEnum = Role.Error;
            switch (role)
            {
                case 0:
                    roleEnum = Role.Admin;
                    break;
                case 1:
                    roleEnum = Role.Analyst;
                    break;
                case 2:
                    roleEnum = Role.Guest;
                    break;
            }
            return roleEnum;
        }

        public static int EnumToInt(Role role)
        {
            var roleInt = 3;
            switch (role)
            {
                case Role.Admin:
                    roleInt = 0;
                    break;
                case Role.Analyst:
                    roleInt = 1;
                    break;
                case Role.Guest:
                    roleInt = 2;
                    break;
            }
            return roleInt;
        }

    }

}
