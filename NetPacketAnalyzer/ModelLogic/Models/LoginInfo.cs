using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.Enums;
using AccessDB.DTO;
using System.Linq;

namespace ModelLogic.Models
{
    public class LoginInfo
    {
        public string Username { get; }
        public List<Role> Roles { get; }

        public LoginInfo(SystemUserDTO user)
        {
            Username = user.Login;
            var stringifyRoles = new List<string>(user.Roles);
            Roles = stringifyRoles.Select((strRole, i) => RoleExtension.RoleStringToEnum(strRole)).ToList();
        }

        public LoginInfo(string login, List<RoleDTO> roles)
        {
            Username = login;
            Roles = roles.Select((el) => RoleExtension.RoleDTOToEnum(el)).ToList();
        }

        public LoginInfo(string login, List<Role> roles)
        {
            Username = login;
            Roles = roles;
        }
    }
}
