using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClickHouse.Ado;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Qoollo.ClickHouse.Net.ConnectionPool.Configuration;
using ModelLogic;
using ModelLogic.Models;
using AccessDB.DTO;
using ModelLogic.Controllers;
using AccessDB.QueryBuilder.IQueryBuilder;
using AccessDB.Repositories.IRepositories;
using AccessDB.Repositories.ClickHouse;
using Qoollo.ClickHouse.Net.Repository;
using AccessDB.QueryBuilder.ClickHouse;
using Qoollo.ClickHouse.Net.ConnectionPool;
using AccessDB.Enums;
using System.Text;
using ModelLogic.Utilities;

namespace TechUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Checker check = new Checker("default_admin", "");
        }
    }

    class Checker
    {
        IConfiguration _config;
        IHost _host;
        IHostBuilder _builder;

        ClickHouseConnectionPoolConfiguration _clickConfig = new ClickHouseConnectionPoolConfiguration();
        ClickHouseConnectionSettings _connectionSettings;
        LoginInfo _loginInfo = new LoginInfo("default_admin", new List<AccessDB.Enums.Role>() { AccessDB.Enums.Role.Admin });
        public Checker(string login, string pass)
        {
            _config = new ConfigurationBuilder()
                   .AddJsonFile("config.json")
                   .Build();

            ParseConfig(_config);
            ChangeUser(login, pass);

            RebuildLandingDI();

            OpenLandingForm();
        }

        private void OpenLandingForm()
        {
            using (var serviceScope = _host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var start = services.GetRequiredService<Start>();
                start.Run();
            }
        }

        private void ParseConfig(IConfiguration _config)
        {
            _clickConfig = _config.GetSection("ClickHouseConnectionPoolConfiguration").Get<ClickHouseConnectionPoolConfiguration>();
            if ((_clickConfig != null) && (_clickConfig.ConnectionStrings.Count() > 0))
            {
                string connectionString = _clickConfig.ConnectionStrings[0];
                _connectionSettings = new ClickHouseConnectionSettings(connectionString);
            }
            else
            {
                Console.WriteLine("Ошибка при чтении конфига. Проверьте наличие и корректность секции 'ClickHouseConnectionPoolConfiguration'");
                throw new Exception();
            }
        }

        private void ChangeUser(string login, string pass)
        {
            _loginInfo = new LoginInfo(login, _loginInfo.Roles);
            _connectionSettings.User = login;
            _connectionSettings.Password = pass;
            _clickConfig.ConnectionStrings = new List<string> { _connectionSettings.ToString() };
        }

        private void RebuildLandingDI()
        {
            _builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    DiExtensions.AddQueryBuildersClickHouseDI(services);
                    DiExtensions.AddEntityMappersClickHouseDI(services);
                    DiExtensions.AddRepositoriesClickHouseDI(services);
                    DiExtensions.AddClickHouseDI(services, _clickConfig);
                    DiExtensions.AddControllersDI(services);
                    services.AddSingleton<Start>();
                    services.AddSingleton(provider => { return _loginInfo; });
                });

            if (_host != null)
            {
                var t = _host.StopAsync();
                t.Wait();
            }
            _host = _builder.Build();
        }
    }
    class Start
    {
        private UserController _user;
        private AnalystController _analyst;
        private AdminController _admin;
        private GuestController _guest;
        public Start(UserController user, GuestController guest, AdminController admin, AnalystController analyst)
        {
            _user = user;
            _analyst = analyst;
            _admin = admin;
            _guest = guest;
        }
        public void Run()
        {
            int need = 10;
            while (need != 0)
            {
                PrintMenuMain();
                need = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (need)
                {
                    case 1:
                        CheckGuest();
                        break;
                    case 3:
                        CheckAdmin();
                        break;
                    case 2:
                        CheckAnalyst();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void PrintMenuMain()
        {
            Console.WriteLine("0 - Выход\n1 - Гость\n2 - Аналитик\n3 - Админ\n");
        }
        void CheckGuest()
        {
            int need = 100;
            while (need != 0)
            {
                Console.WriteLine();
                Console.WriteLine("Гость\n0 - Выйти \n1 - Посмотреть потоки за 48 часов\n2 - Посмотреть все типы источников\n3 - Посмотреть все" +
               " назначения");
                Console.WriteLine();
                need = Convert.ToInt32(Console.ReadLine());
                switch (need)
                {
                    case 1:
                        GetFlow24();
                        break;
                    case 2:
                        GetTypeSources();
                        break;
                    case 3:
                        GetAllDest();
                        break;
                    case 0:
                       break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void GetFlow24()
        {
            List<Flow> flows = _guest.FindFlowByMinutes(60 * 24 * 2);
            if (flows != null)
            {
                foreach (var flow in flows)
                {
                    Console.WriteLine(flow.TimeReceived.ToString() + " " +
                                            flow.TimeFlowStart.ToString() + " " +
                                            flow.SequenceNum.ToString() + " " +
                                            flow.SamplingRate.ToString() + " " +
                                            flow.SamplerAddress.ToString() + " " +
                                            flow.SrcAddr.ToString() + " " +
                                            flow.DstAddr.ToString() + " " +
                                            flow.SrcAS.ToString() + " " +
                                            flow.DstAS.ToString() + " " +
                                            flow.EType.ToString() + " " +
                                            flow.Proto.ToString() + " " +
                                            flow.SrcPort.ToString() + " " +
                                            flow.DstPort.ToString() + " " +
                                            flow.Bytes.ToString() + " " +
                                            flow.Packets.ToString() + " ");
                }
            }
            else
            {
                Console.WriteLine("Потоки не найдены");
            }
        }
        void GetTypeSources()
        {
            List<SourceType> stypes = _user.FindAllSourceTypes();
            if (stypes != null)
            {
                foreach (var type in stypes)
                {
                    Console.WriteLine(type.Type.ToString() + "  " + type.CommentString);
                }
            }
            else
            {
                Console.WriteLine("Типы источников не найдены");
            }
        }
        void GetAllDest()
        {
            List<Destination> dests = _user.FindAllDestinations();
            if (dests != null)
            {
                foreach (var dest in dests)
                {
                    Console.WriteLine(dest.Ip + "  " + dest.Type.ToString());
                }
            }
            else
            {
                Console.WriteLine("Назначения не найдены");
            }
        }

        void CheckAdmin()
        {
            int need = 10;
            while (need != 2)
            {
                Console.WriteLine("\nАдмин\n0 - Выход\n1 - Просмотреть всех пользователей\n2 - Добавить пользователя\n3 - Удалить пользователя");
                need = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (need)
                {
                    case 0:
                        break;
                    case 1:
                        GetAllUsers();
                        break;
                    case 2:
                        AddUser();
                        break;
                    case 3:
                        DeleteUser();
                        break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void AddUser()
        {
            Console.WriteLine("Введите логин: ");
            string login = Console.ReadLine().Trim();
            Console.WriteLine("Введите Пароль: ");
            string pass = Console.ReadLine().Trim();

            _admin.CreateSystemUser(login, pass);
        }

        void DeleteUser()
        {
            Console.WriteLine("Введите логин: ");
            string login = Console.ReadLine().Trim();

            _admin.DeleteSystemUser(login);
        }

        void GetAllUsers()
        {
            List<LoginInfo> users = _admin.FindAllSystemUsers();
            if (users != null)
            {
                foreach (var user in users)
                {
                    string roleRepresent = "";
                    bool isFirst = true;
                    foreach (var role in user.Roles)
                    {
                        if (!isFirst)
                        {
                            roleRepresent += ", ";
                        }
                        isFirst = false;
                        roleRepresent += RoleExtension.RoleEnumToString(role);
                    }
                    Console.WriteLine(user.Username + "  " + roleRepresent);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Пользователи не найдены");
            }
        }
        void CheckAnalyst()
        {
            int need = 0;
            while (need != 3)
            {
                Console.WriteLine("\nАналитик\n0 - Выход\n1 - Получить сумму трафика от источников за 48 часов");
                need = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (need)
                {
                    case 1:
                        GetSumTraff();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Неверный номер");
                        break;
                }
            }
        }
        void GetSumTraff()
        {
            List<SumDTO> sums = _analyst.FindSum(24 * 60 * 2);
            if (sums != null)
            {
                foreach (var sum in sums)
                {
                    Console.WriteLine(IpTransformer.MaskToString(Encoding.ASCII.GetBytes(sum.SrcAddr)) + " " + sum.Sum);
                }
            }
            else
            {
                Console.WriteLine("Трафик не найден");
            }
        }
    }
    public static class DiExtensions
    {
        public static void AddLoginLogicExtensions(IServiceCollection services, ClickHouseConnectionPoolConfiguration config)
        {
            AddClickHouseDI(services, config);
            //services.AddClickHouseRepository(_config.GetSection("ClickHouseConnectionPoolConfiguration"));
            services.AddSingleton<IUserManagmentQueryBuilder, UserManagmentQueryBuilderClickHouse>();
            services.AddSingleton<IUserManagmentRepository, UserManagmentRepositoryClickHouse>();
            services.AddSingleton<IEntityMapper<SystemUserDTO>, SystemUserDTOMapper>();
        }
        public static void AddRepositoriesClickHouseDI(IServiceCollection services)
        {
            services.AddSingleton<IDataSourcesRepository, DataSourcesRepositoryClickHouse>();
            services.AddSingleton<IDataSourceTypesRepository, DataSourceTypesRepositoryClickHouse>();
            services.AddSingleton<IDestinationsRepository, DestinationsRepositoryClickHouse>();
            services.AddSingleton<IDestinationTypesRepository, DestinationTypesRepositoryClickHouse>();
            services.AddSingleton<IDifficultRepository, DifficultRepositoryClickHouse>();
            services.AddSingleton<IUserInfoRepository, UserInfoRepositoryClickHouse>();
            services.AddSingleton<IUserManagmentRepository, UserManagmentRepositoryClickHouse>();
            services.AddSingleton<IFlowsRawRepository, FlowsRawRepositoryClickHouse>();
        }
        public static void AddQueryBuildersClickHouseDI(IServiceCollection services)
        {
            services.AddSingleton<IDataSourcesQueryBuilder, DataSourcesQueryBuilderClickHouse>();
            services.AddSingleton<IDataSourceTypesQueryBuilder, DataSourceTypesQueryBuilderClickHouse>();
            services.AddSingleton<IDestinationsQueryBuilder, DestinationsQueryBuilderClickHouse>();
            services.AddSingleton<IDestinationTypesQueryBuilder, DestinationTypesQueryBuilderClickHouse>();
            services.AddSingleton<IDifficultQueryBuilder, DifficultQueryBuilderClickHouse>();
            services.AddSingleton<IUserInfoQueryBuilder, UserInfoQueryBuilderClickHouse>();
            services.AddSingleton<IUserManagmentQueryBuilder, UserManagmentQueryBuilderClickHouse>();
            services.AddSingleton<IFlowsRawQueryBuilder, FlowsRawQueryBuilder>();
        }

        public static void AddEntityMappersClickHouseDI(IServiceCollection services)
        {
            services.AddSingleton<IEntityMapper<DataSourceDTO>, DataSourceDTOMapper>();
            services.AddSingleton<IEntityMapper<SourceTypeDTO>, SourceTypeDTOMapper>();
            services.AddSingleton<IEntityMapper<DestinationDTO>, DestinationDTOMapper>();
            services.AddSingleton<IEntityMapper<DestinationTypeDTO>, DestinationTypeDTOMapper>();
            services.AddSingleton<IEntityMapper<DifficultSourcesByPostThatToDestTypeMoreThanCountDTO>, DifficultSourcesByMarkThatToDestTypeMoreThanCountDTOMapper>();
            services.AddSingleton<IEntityMapper<SystemUserDTO>, SystemUserDTOMapper>();
            services.AddSingleton<IEntityMapper<UserInfoDTO>, UserInfoDTOMapper>();
            services.AddSingleton<IEntityMapper<RoleDTO>, RoleDTOMapper>();
            services.AddSingleton<IEntityMapper<FlowDTO>, FlowDTOMapper>();
        }
        public static void AddClickHouseDI(IServiceCollection services, ClickHouseConnectionPoolConfiguration config)
        {
            services.AddTransient<IClickHouseConnectionPoolConfiguration>(serviceProvider => config);
            services.AddTransient<ClickHouseConnectionPool>();
            services.AddTransient<IClickHouseRepository, ClickHouseRepository>();
        }
        public static void AddControllersDI(IServiceCollection services)
        {
            services.AddSingleton<UserController>();
            services.AddSingleton<AdminController>();
            services.AddSingleton<AnalystController>();
            services.AddSingleton<GuestController>();
        }
    }
}

