using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.DTO;
using DataObjects.Enums;
using DataObjects.Models;

namespace WebApplication.DataMappers
{
    class UserMapperDTO
    {
        static public User MapToInner(UserDTO cred)
        {
            return new User(cred.Username, cred.Password, RoleExtension.IntToEnum(cred.Role), cred.Id);
        }
        static public UserDTO MapToDto(User user)
        {
            return new UserDTO(user.Username, user.Password, RoleExtension.EnumToInt(user.Role), user.Id);
        }
    }
}
