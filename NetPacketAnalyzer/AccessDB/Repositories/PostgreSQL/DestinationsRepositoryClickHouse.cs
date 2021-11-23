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
    public class DestinationsRepositoryPostgreSQL : IDestinationsRepository, IDisposable
    {
        defaultContext _context;
        ILogger<DestinationsRepositoryPostgreSQL> _logger;
        public DestinationsRepositoryPostgreSQL(defaultContext context, ILogger<DestinationsRepositoryPostgreSQL> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Add(DestinationDTO dto)
        {
            DataDestination element = new DataDestination(dto);
            try
            {
                _context.DataDestinations.Add(element);
                _context.SaveChanges();
                _logger.LogInformation($"Data destination {element.Type} added at {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void Delete(DestinationDTO dto)
        {
            DataDestination element = new DataDestination(dto);
            try
            {
                _context.DataDestinations.Remove(element);
                _context.SaveChanges();
                _logger.LogInformation($"Data source {element.Type} removed at {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }


        public IEnumerable<DestinationDTO> Find(DestinationDTO dto)
        {
            var res = _context.DataDestinations.Find(dto.Ip);
            if (res == null)
            {
                return new List<DestinationDTO>();
            }
            else
            {
                return new List<DestinationDTO>() { new DestinationDTO(res) };
            }
        }

        public IEnumerable<DestinationDTO> FindAll()
        {
            IEnumerable<DestinationDTO> ldto = _context.DataDestinations.Select((el) => new DestinationDTO(el));
            return ldto;
        }
    }
}
