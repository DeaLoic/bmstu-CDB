using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.Repositories.IRepositories
{
    public interface ICrudRepository<DTO>
    {
        public void Add(DTO dto);
        public void Delete(DTO dto);
        public IEnumerable<DTO> Find(DTO dto);
        public IEnumerable<DTO> FindAll();
    }

}
