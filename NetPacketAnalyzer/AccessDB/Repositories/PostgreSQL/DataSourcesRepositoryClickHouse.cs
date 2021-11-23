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
    public class DataSourcesRepositoryPostgreSQL : IDataSourcesRepository, IDisposable
    {
        defaultContext _context;
        ILogger<DataSourcesRepositoryPostgreSQL> _logger;
        public DataSourcesRepositoryPostgreSQL(defaultContext context, ILogger<DataSourcesRepositoryPostgreSQL> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Add(DataSourceDTO dto)
        {
            DataSource element = new DataSource(dto);
            try
            {
                _context.DataSources.Add(element);
                _context.SaveChanges();
                _logger.LogInformation($"Data source {element.Ip} added at {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void Delete(DataSourceDTO dto)
        {
            DataSource element = new DataSource(dto);
            try
            {
                _context.DataSources.Remove(element);
                _context.SaveChanges();
                _logger.LogInformation($"Data source {element.Ip} removed at {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }


        public IEnumerable<DataSourceDTO> Find(DataSourceDTO dto)
        {
            var res = _context.DataSources.Find(dto.Ip);
            if (res == null) {
                return new List<DataSourceDTO>();
            }
            else
            {
                return new List<DataSourceDTO>() { new DataSourceDTO(res)};
            }
        }

        public IEnumerable<DataSourceDTO> FindAll()
        {
            IEnumerable<DataSourceDTO> ldto = _context.DataSources.Select((el) => new DataSourceDTO(el));
            return ldto;
        }
    }
}
