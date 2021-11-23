using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.DTO;
using AccessDB.Repositories.IRepositories;
using AccessDB.QueryBuilder.IQueryBuilder;
using Qoollo.ClickHouse.Net.Repository;
using Microsoft.Extensions.Logging;
using AccessDB.Enums;
using AccessDB.DbModels.PostgreSQL;
using System.Linq;

namespace AccessDB.Repositories.PostgreSQL
{
    public class DifficultRepositoryPostgreSQL : IDifficultRepository, IDisposable
    {
        defaultContext _context;
        ILogger<DifficultRepositoryPostgreSQL> _logger;
        public DifficultRepositoryPostgreSQL(defaultContext context, ILogger<DifficultRepositoryPostgreSQL> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<DifficultSourcesByPostThatToDestTypeMoreThanCountDTO> FindSourcesByPostThatToDestTypeMoreThanCount(string post, int type, int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MaxSpendingDTO> GetMaxSpendingDay()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SumDTO> GetTraficCountPerSource(int minutes)
        {
            return _context.FlowsRaws.ToList().Where(flow => { return (DateTime.Now - flow.Timeflowstart.Value).TotalSeconds / 60 <= minutes; })
                                       .GroupBy(flow => flow.Srcaddr)
                                       .Select(flow => new { SrcAddr = flow.Key, Sum = flow.Count() })
                                       .Select(w => { var a = new SumDTO(); a.SrcAddr = w.SrcAddr; a.Sum = w.Sum; return a; });
        }
    }
}
