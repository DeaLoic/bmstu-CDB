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
    public class DataSourceTypesRepositoryPostgreSQL : IDataSourceTypesRepository, IDisposable
    {
        defaultContext _context;
        ILogger<DataSourceTypesRepositoryPostgreSQL> _logger;
        public DataSourceTypesRepositoryPostgreSQL(defaultContext context, ILogger<DataSourceTypesRepositoryPostgreSQL> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Add(SourceTypeDTO dto)
        {
            DataSourceType element = new DataSourceType(dto);
            try
            {
                _context.DataSourceTypes.Add(element);
                _context.SaveChanges();
                _logger.LogInformation($"Data source {element.Type} added at {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void Delete(SourceTypeDTO dto)
        {
            DataSourceType element = new DataSourceType(dto);
            try
            {
                _context.DataSourceTypes.Remove(element);
                _context.SaveChanges();
                _logger.LogInformation($"Data source {element.Type} removed at {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }


        public IEnumerable<SourceTypeDTO> Find(SourceTypeDTO dto)
        {
            var res = _context.DataSourceTypes.Find(dto.Type);
            if (res == null)
            {
                return new List<SourceTypeDTO>();
            }
            else
            {
                return new List<SourceTypeDTO>() { new SourceTypeDTO(res) };
            }
        }

        public IEnumerable<SourceTypeDTO> FindAll()
        {
            IEnumerable<SourceTypeDTO> ldto = _context.DataSourceTypes.Select((el) => new SourceTypeDTO(el));
            return ldto;
        }
    }
}
