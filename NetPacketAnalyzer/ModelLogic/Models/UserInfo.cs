using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.DTO;

namespace ModelLogic.Models
{
    public class UserInfo
    {
        public string UUID { get; set; }
        public string Name { get; set; }
        public string Post { get; set; }

        public UserInfo(UserInfoDTO info)
        {
            Name = info.Name;
            UUID = info.UUID;
            Post = info.Post;
        }
    }
}
