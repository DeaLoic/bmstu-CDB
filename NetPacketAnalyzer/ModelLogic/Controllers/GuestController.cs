using AccessDB.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLogic.Controllers
{
    public class GuestController : UserController
    {

        public GuestController(IDataSourcesRepository dataSourcesRepository, IDataSourceTypesRepository dataSourceTypesRepository,
                IDestinationsRepository destinationsRepository, IDestinationTypesRepository destinationTypesRepository,
                IUserInfoRepository userInfoRepository, IFlowsRawRepository flowsRawRepository, ILogger<GuestController> logger) :
                base(dataSourcesRepository, dataSourceTypesRepository, destinationsRepository, destinationTypesRepository, userInfoRepository, flowsRawRepository, logger)
        {
        }
    }
}
