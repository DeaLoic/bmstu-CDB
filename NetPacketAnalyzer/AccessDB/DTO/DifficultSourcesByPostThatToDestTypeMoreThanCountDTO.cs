using Qoollo.ClickHouse.Net.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AccessDB.DTO
{
    public class DifficultSourcesByPostThatToDestTypeMoreThanCountDTO
    {
        public int Type { get; set; }
        public string CommentString { get; set; }

        public DifficultSourcesByPostThatToDestTypeMoreThanCountDTO(IDataReader reader)
        {
            Type = reader.GetInt16(0);
            CommentString = reader.GetString(1);
        }

        public DifficultSourcesByPostThatToDestTypeMoreThanCountDTO() { }

        public IEnumerator GetEnumerator()
        {
            yield return Type;
            yield return CommentString;
        }
    }

    public class DifficultSourcesByMarkThatToDestTypeMoreThanCountDTOMapper : IEntityMapper<DifficultSourcesByPostThatToDestTypeMoreThanCountDTO>
    {
        public DifficultSourcesByPostThatToDestTypeMoreThanCountDTO MapEntity(IDataReader reader)
        {
            return new DifficultSourcesByPostThatToDestTypeMoreThanCountDTO(reader);
        }
    }
}
