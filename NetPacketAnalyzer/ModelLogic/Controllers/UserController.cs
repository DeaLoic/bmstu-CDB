using System;
using System.Collections.Generic;
using System.Text;
using AccessDB.Repositories.IRepositories;
using ModelLogic.Models;
using Microsoft.Extensions.Logging;
using Qoollo.ClickHouse.Net.ConnectionPool;
using System.Linq;
using AccessDB.DTO;

namespace ModelLogic.Controllers
{
    public class UserController
    {
        protected IDataSourcesRepository _dataSourcesRepository;
        protected IDataSourceTypesRepository _dataSourceTypesRepository;
        protected IDestinationsRepository _destinationsRepository;
        protected IDestinationTypesRepository _destinationTypesRepository;
        protected IUserInfoRepository _userInfoRepository;
        protected ILogger<UserController> _logger;

        public UserController(IDataSourcesRepository dataSourcesRepository, IDataSourceTypesRepository dataSourceTypesRepository,
            IDestinationsRepository destinationsRepository, IDestinationTypesRepository destinationTypesRepository,
            IUserInfoRepository userInfoRepository, ILogger<UserController> logger)
        {
            _dataSourcesRepository = dataSourcesRepository;
            _dataSourceTypesRepository = dataSourceTypesRepository;
            _destinationsRepository = destinationsRepository;
            _destinationTypesRepository = destinationTypesRepository;
            _userInfoRepository = userInfoRepository;
            _logger = logger;
        }

        public List<DataSource> FindAllDataSources()
        {
            var entities = new List<DataSourceDTO>();
            entities = _dataSourcesRepository.FindAll().ToList();

            return entities.Select( (dto) => new DataSource(dto) ).ToList();
        }
        public List<SourceType> FindAllSourceTypes()
        {
            var entities = new List<SourceTypeDTO>();
            entities = _dataSourceTypesRepository.FindAll().ToList();

            return entities.Select((dto) => new SourceType(dto)).ToList();
        }

        public List<Destination> FindAllDestinations()
        {
            var entities = new List<DestinationDTO>();
            entities = _destinationsRepository.FindAll().ToList();

            return entities.Select((dto) => new Destination(dto)).ToList();
        }
        public List<DestinationType> FindAllDestinationTypes()
        {
            var entities = new List<DestinationTypeDTO>();
            entities = _destinationTypesRepository.FindAll().ToList();

            return entities.Select((dto) => new DestinationType(dto)).ToList();
        }
        public List<UserInfo> FindAllUserInfo()
        {
            var entities = new List<UserInfoDTO>();
            entities = _userInfoRepository.FindAll().ToList();

            return entities.Select((dto) => new UserInfo(dto)).ToList();
        }
    }
}
