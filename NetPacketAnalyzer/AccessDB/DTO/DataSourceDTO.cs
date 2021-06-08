using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Qoollo.ClickHouse.Net.Repository;

namespace AccessDB.DTO
{
    public class DataSourceDTO : IEnumerable
    {
        public string Ip { get; set; }
        public string OwnerUUID { get; set; }
        public int Type { get; set; }

        public DataSourceDTO(IDataReader reader)
        {
            Ip = reader.GetString(0);
            OwnerUUID = reader.GetString(1);
            Type = reader.GetInt16(2);
        }

        public DataSourceDTO() { }

        public IEnumerator GetEnumerator()
        {
            yield return Ip;
            yield return OwnerUUID;
            yield return Type;
        }
    }

    public class DataSourceDTOMapper : IEntityMapper<DataSourceDTO>
    {
        public DataSourceDTO MapEntity(IDataReader reader)
        {
            return new DataSourceDTO(reader);
        }
    }
}
