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
    public class FlowsRawRepositoryPostgreSQL : IFlowsRawRepository, IDisposable
    {
        defaultContext _context;
        ILogger<FlowsRawRepositoryPostgreSQL> _logger;
        public FlowsRawRepositoryPostgreSQL(defaultContext context, ILogger<FlowsRawRepositoryPostgreSQL> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void DeleteForTime(int minutes)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FlowDTO> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FlowDTO> FindForTime(int minutes)
        {
            return FindForTimePeriod(minutes, 0);
        }

        public IEnumerable<FlowDTO> FindForTimePeriod(int minutesStart, int minutesEnd)
        {
            return _context.FlowsRaws.Where(flow =>  ((DateTime.Now - flow.Timeflowstart.Value).TotalSeconds / 60 < minutesStart && (DateTime.Now - flow.Timeflowstart.Value).TotalSeconds / 60 > minutesEnd))
                                      .Select(w => new FlowDTO(w));
        }
    }
}
