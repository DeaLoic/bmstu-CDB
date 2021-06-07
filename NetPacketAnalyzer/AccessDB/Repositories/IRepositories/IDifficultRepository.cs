using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.DTO;
using AccessDB.Enums;

namespace AccessDB.Repositories.IRepositories
{
    interface IDifficultRepository
    {
        public IEnumerable<DifficultSourcesByPostThatToDestTypeMoreThanCountDTO> FindSourcesByPostThatToDestTypeMoreThanCount(string post, DestType type, int count);
    }
}
