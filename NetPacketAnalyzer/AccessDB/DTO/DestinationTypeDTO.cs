using Qoollo.ClickHouse.Net.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AccessDB.DTO
{
    public class DestinationTypeDTO
    {
        public int Type { get; set; }
        public string CommentString { get; set; }

        public DestinationTypeDTO(IDataReader reader)
        {
            Type = reader.GetInt16(0);
            CommentString = reader.GetString(1);
        }

        public DestinationTypeDTO() { }

        public IEnumerator GetEnumerator()
        {
            yield return Type;
            yield return CommentString;
        }
    }

    public class DestinationTypeDTOMapper : IEntityMapper<DestinationTypeDTO>
    {
        public DestinationTypeDTO MapEntity(IDataReader reader)
        {
            return new DestinationTypeDTO(reader);
        }
    }
}
