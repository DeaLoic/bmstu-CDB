using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Qoollo.ClickHouse.Net.Repository;
using AccessDB.DbModels.PostgreSQL;

namespace AccessDB.DTO
{
    public class UserInfoDTO : IEnumerable
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Post { get; set; }

        public UserInfoDTO(IDataReader reader)
        {
            UUID = reader.GetString(0);
            Name = reader.GetString(1);
            Post = reader.GetString(2);
        }

        public UserInfoDTO() { }

        public UserInfoDTO(UserInfo w)
        {
            UUID = w.Id.ToString();
            Name = w.Name;
            Post = w.Post;
        }

        public IEnumerator GetEnumerator()
        {
            yield return UUID;
            yield return Name;
            yield return Post;
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
