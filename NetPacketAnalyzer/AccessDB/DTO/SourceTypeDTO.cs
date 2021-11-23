using Qoollo.ClickHouse.Net.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AccessDB.DTO
{
    public class SourceTypeDTO
    {
        public int Type { get; set; }
        public string CommentString { get; set; }

        public SourceTypeDTO(IDataReader reader)
        {
            Type = reader.GetInt16(0);
            CommentString = reader.GetString(1);
        }

        public SourceTypeDTO() { }

        public SourceTypeDTO(AccessDB.DbModels.PostgreSQL.DataSourceType res)
        {
            Type = res.Type;
            CommentString = res.Info;
        }

        public IEnumerator GetEnumerator()
        {
            yield return Type;
            yield return CommentString;
        }
    }

    public class SourceTypeDTOMapper : IEntityMapper<SourceTypeDTO>
    {
        public SourceTypeDTO MapEntity(IDataReader reader)
        {
            return new SourceTypeDTO(reader);
        }
    }
}
