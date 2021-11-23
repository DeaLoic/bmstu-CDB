using AccessDB.DTO;
using System;
using System.Collections.Generic;

#nullable disable

namespace AccessDB.DbModels.PostgreSQL
{
    public partial class DataDestination
    {

        public string Ip { get; set; }
        public int? Type { get; set; }

        public virtual DataSourceType TypeNavigation { get; set; }
        public DataDestination(DestinationDTO dto)
        {
            Ip = dto.Ip;
            Type = dto.Type;
        }
    }
}
