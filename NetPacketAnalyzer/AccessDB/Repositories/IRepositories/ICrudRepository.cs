using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.Repositories.IRepositories
{
    public interface ICrudRepository<Entity>
    {
        void CreateEntityTableIfNotExists();
    }

}
