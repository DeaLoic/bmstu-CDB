using AccessDB.DTO;
using System;
using System.Collections.Generic;

#nullable disable

namespace AccessDB.DbModels.PostgreSQL
{
    public partial class DataDestinationType
    {
        public DataDestinationType(DestinationTypeDTO dto)
        {
            Type = dto.Type;
            Info = dto.CommentString;
        }

        public int Type { get; set; }
        public string Info { get; set; }
    }
}
