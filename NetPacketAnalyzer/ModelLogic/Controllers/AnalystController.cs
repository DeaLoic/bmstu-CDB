using AccessDB.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLogic.Controllers
{
    public class AnalystController : UserController
    {
        protected IDifficultRepository _difficultRepository;
        public AnalystController(IDataSourcesRepository dataSourcesRepository, IDataSourceTypesRepository dataSourceTypesRepository,
                IDestinationsRepository destinationsRepository, IDestinationTypesRepository destinationTypesRepository,
                IUserInfoRepository userInfoRepository, IDifficultRepository difficultRepository, ILogger<AnalystController> logger) :
                base(dataSourcesRepository, dataSourceTypesRepository, destinationsRepository, destinationTypesRepository, userInfoRepository, logger)
        {
            _difficultRepository = difficultRepository;
        }
    }
}
