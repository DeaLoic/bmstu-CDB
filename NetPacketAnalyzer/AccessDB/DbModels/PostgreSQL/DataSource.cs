using AccessDB.DTO;
using System;
using System.Collections.Generic;

#nullable disable

namespace AccessDB.DbModels.PostgreSQL
{
    public partial class DataSource
    {

        public string Ip { get; set; }
        public int? Owneruuid { get; set; }
        public int? Type { get; set; }

        public virtual UserInfo Owneruu { get; set; }
        public virtual DataSourceType TypeNavigation { get; set; }

        public DataSource(DataSourceDTO dto)
        {
            this.Ip = dto.Ip;
            this.Owneruuid = Int32.Parse(dto.OwnerUUID);
            this.Type = dto.Type;
        }
        public DataSource()
        {
        }
    }
}
