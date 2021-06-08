using Qoollo.ClickHouse.Net.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AccessDB.DTO
{
    public class RoleDTO
    {
        public string RoleName { get; set; }
        public int AdminOption { get; set; }
        public int IsDefault { get; set; }

        public RoleDTO(IDataReader reader)
        {
            RoleName = reader.GetString(0);
            AdminOption = reader.GetInt16(1);
            IsDefault = reader.GetInt16(2);
        }

        public RoleDTO() { }

        public IEnumerator GetEnumerator()
        {
            yield return RoleName;
            yield return AdminOption;
            yield return IsDefault;
        }
    }

    public class RoleDTOMapper : IEntityMapper<RoleDTO>
    {
        public RoleDTO MapEntity(IDataReader reader)
        {
            return new RoleDTO(reader);
        }
    }
}
