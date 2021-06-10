using Qoollo.ClickHouse.Net.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AccessDB.DTO
{
    public class SumDTO
    {
        public string SrcAddr { get; set; }
        public int Sum { get; set; }

        public SumDTO(IDataReader reader)
        {
            SrcAddr = reader.GetString(0);
            Sum = reader.GetInt32(1);
        }

        public SumDTO() { }

        public IEnumerator GetEnumerator()
        {
            yield return SrcAddr;
            yield return Sum;
        }
    }

    public class SumDTOMapper : IEntityMapper<SumDTO>
    {
        public SumDTO MapEntity(IDataReader reader)
        {
            return new SumDTO(reader);
        }
    }
}
