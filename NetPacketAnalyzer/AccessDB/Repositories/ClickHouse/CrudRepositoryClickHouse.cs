using System;
using System.Collections.Generic;
using System.Text;
using Qoollo.ClickHouse.Net.Repository;
using Microsoft.Extensions.Logging;
using AccessDB.QueryBuilder.IQueryBuilder;
using AccessDB.Repositories.IRepositories;
using System.Linq;

namespace AccessDB.Repositories.ClickHouse
{
    public class CrudRepositoryClickHouse<DTO> : ICrudRepository<DTO>
    {
        protected ICrudQueryBuilder<DTO> _qbuilder;
        protected IClickHouseRepository _clickHouseRepository;
        protected ILogger _logger;
        protected IEntityMapper<DTO> _mapper;
        public CrudRepositoryClickHouse(ICrudQueryBuilder<DTO> qbuilder, IClickHouseRepository clickHouseRepository, ILogger logger, IEntityMapper<DTO> mapper)
        {
            _qbuilder = qbuilder;
            _clickHouseRepository = clickHouseRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public void Add(DTO dto)
        {
            string createQuery = _qbuilder.AddQuery(dto);
            _clickHouseRepository.ExecuteNonQuery(createQuery);
            _logger.LogInformation("Entity {Type} was added", typeof(DTO), DateTime.UtcNow);
        }


        public void Delete(DTO dto)
        {
            string deleteQuery = _qbuilder.DeleteQuery(dto);
            _clickHouseRepository.ExecuteNonQuery(deleteQuery);
            _logger.LogInformation("Entity {Type} was deleted", typeof(DTO));
        }

        public IEnumerable<DTO> Find(DTO dto)
        {
            string findQuery = _qbuilder.FindQuery(dto);
            var entities = _clickHouseRepository.ExecuteQueryMapping(findQuery, _mapper);
            _logger.LogInformation("Entity {Type} was finded {N}-time", typeof(DTO), entities.ToList().Count());
            return entities;
        }

        public IEnumerable<DTO> FindAll()
        {
            string findAllQuery = _qbuilder.FindAllQuery();
            var entities = _clickHouseRepository.ExecuteQueryMapping(findAllQuery, _mapper);
            _logger.LogInformation("Entity {Type} was finded", typeof(DTO), entities.ToList().Count());
            return entities;
        }
    }
}
