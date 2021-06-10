using AccessDB.DTO;
using AccessDB.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ModelLogic.Controllers
{
    public class AnalystController : UserController
    {
        protected IDifficultRepository _difficultRepository;
        public AnalystController(IDataSourcesRepository dataSourcesRepository, IDataSourceTypesRepository dataSourceTypesRepository,
                IDestinationsRepository destinationsRepository, IDestinationTypesRepository destinationTypesRepository,
                IUserInfoRepository userInfoRepository, IDifficultRepository difficultRepository, IFlowsRawRepository flowsRawRepository, ILogger<AnalystController> logger) :
                base(dataSourcesRepository, dataSourceTypesRepository, destinationsRepository, destinationTypesRepository, userInfoRepository, flowsRawRepository, logger)
        {
            _difficultRepository = difficultRepository;
        }

        public List<SumDTO> FindSum(int minutes)
        {
            var entities = new List<SumDTO>();
            entities = _difficultRepository.GetTraficCountPerSource(minutes).ToList();

            return entities;
        }
    }
}
