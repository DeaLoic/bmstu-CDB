using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.Enums;
using AccessDB.DTO;

namespace ModelLogic.Models
{
    public class DataSource
    {
        public string Ip { get; }
        public string OwnerUUID { get; }
        public int SourceType { get; }

        public DataSource(DataSourceDTO data)
        {
            Ip = data.Ip;
            OwnerUUID = data.OwnerUUID;
            SourceType = data.Type;
        }
    }
}
