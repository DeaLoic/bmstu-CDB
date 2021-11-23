using System;
using System.Collections.Generic;

#nullable disable

namespace AccessDB.DbModels.PostgreSQL
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            DataSources = new HashSet<DataSource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Post { get; set; }

        public virtual ICollection<DataSource> DataSources { get; set; }
    }
}
