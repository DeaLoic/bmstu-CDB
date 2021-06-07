using Qoollo.ClickHouse.Net.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AccessDB.DTO
{
    public class DestinationDTO
    {
        public string Ip { get; set; }
        public int Type { get; set; }

        public DestinationDTO(IDataReader reader)
        {
            Ip = reader.GetString(0);
            Type = reader.GetInt16(1);
        }

        public DestinationDTO() { }

        public IEnumerator GetEnumerator()
        {
            yield return Ip;
            yield return Type;
        }
    }

    public class DestinationDTOMapper : IEntityMapper<DestinationDTO>
    {
        public DestinationDTO MapEntity(IDataReader reader)
        {
            return new DestinationDTO(reader);
        }
    }
}
