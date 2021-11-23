using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccessDB.DbModels.PostgreSQL;
using AccessDB.DTO;
using AccessDB.QueryBuilder.IQueryBuilder;
using AccessDB.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using Qoollo.ClickHouse.Net.Repository;

namespace AccessDB.Repositories.PostgreSQL
{
    public class DestinationTypesRepositoryPostgreSQL : IDestinationTypesRepository, IDisposable
    {
        defaultContext _context;
        ILogger<DestinationTypesRepositoryPostgreSQL> _logger;
        public DestinationTypesRepositoryPostgreSQL(defaultContext context, ILogger<DestinationTypesRepositoryPostgreSQL> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Add(DestinationTypeDTO dto)
        {
            DataDestinationType element = new DataDestinationType(dto);
            try
            {
                _context.DataDestinationTypes.Add(element);
                _context.SaveChanges();
                _logger.LogInformation($"Data destination {element.Type} added at {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void Delete(DestinationTypeDTO dto)
        {
            DataDestinationType element = new DataDestinationType(dto);
            try
            {
                _context.DataDestinationTypes.Remove(element);
                _context.SaveChanges();
                _logger.LogInformation($"Data source {element.Type} removed at {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }


        public IEnumerable<DestinationTypeDTO> Find(DestinationTypeDTO dto)
        {
            var res = _context.DataDestinationTypes.Find(dto.Type);
            if (res == null)
            {
                return new List<DestinationTypeDTO>();
            }
            else
            {
                return new List<DestinationTypeDTO>() { new DestinationTypeDTO(res) };
            }
        }

        public IEnumerable<DestinationTypeDTO> FindAll()
        {
            IEnumerable<DestinationTypeDTO> ldto = _context.DataDestinationTypes.Select((el) => new DestinationTypeDTO(el));
            return ldto;
        }
    }
}
