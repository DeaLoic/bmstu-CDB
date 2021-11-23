using AccessDB.DTO;
using System;
using System.Collections.Generic;

#nullable disable

namespace AccessDB.DbModels.PostgreSQL
{
    public partial class DataSourceType
    {
        public DataSourceType()
        {
            DataSources = new HashSet<DataSource>();
        }

        public int Type { get; set; }
        public string Info { get; set; }

        public virtual ICollection<DataSource> DataSources { get; set; }
        public DataSourceType(SourceTypeDTO dto)
        {
            Type = dto.Type;
            Info = dto.CommentString;
        }
    }
}
