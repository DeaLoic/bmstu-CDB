using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.Repositories.IRepositories
{
    public interface ICrudPlusRepository<DTO> : ICrudRepository<DTO>
    {
        void CreateEntityTableIfNotExists();
    }

}
