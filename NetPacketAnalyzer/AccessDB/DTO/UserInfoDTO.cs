﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Qoollo.ClickHouse.Net.Repository;

namespace AccessDB.DTO
{
    public class UserInfoDTO : IEnumerable
    {
        public string Login { get; set; }
        public string[] Roles { get; set; }

        public UserInfoDTO(IDataReader reader)
        {
            Login = reader.GetString(0);
            var objects = (object[])reader.GetValue(1);
            Roles = objects.Select(o => (string)o).ToArray();
        }

        public UserInfoDTO() { }

        public IEnumerator GetEnumerator()
        {
            yield return Login;
            yield return Roles;
        }

    }

    public class UserInfoDTOMapper : IEntityMapper<UserInfoDTO>
    {
        public UserInfoDTO MapEntity(IDataReader reader)
        {
            return new UserInfoDTO(reader);
        }
    }
}