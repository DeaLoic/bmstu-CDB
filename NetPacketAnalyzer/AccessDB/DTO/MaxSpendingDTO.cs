using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using Qoollo.ClickHouse.Net.Repository;

namespace AccessDB.DTO
{
    public class MaxSpendingDTO : IEnumerable
    {
        public DateTime Date { get; set; }
        public string SrcIp { get; set; }
        public int Spend { get; set; }

        public MaxSpendingDTO(IDataReader reader)
        {
            Date = reader.GetDateTime(0);
            SrcIp = reader.GetString(1);
            Spend = reader.GetInt32(2);
        }

        public MaxSpendingDTO() { }

        public IEnumerator GetEnumerator()
        {
            yield return Date;
            yield return SrcIp;
            yield return Spend;
        }
    }

    public class MaxSpendingDTOMapper : IEntityMapper<MaxSpendingDTO>
    {
        public MaxSpendingDTO MapEntity(IDataReader reader)
        {
            return new MaxSpendingDTO(reader);
        }
    }
}
