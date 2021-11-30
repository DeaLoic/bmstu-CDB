using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ClickHouse.Ado;
using AccessDB.Repositories.IRepositories;
using AccessDB.Repositories.ClickHouse;
using AccessDB.QueryBuilder.IQueryBuilder;
using AccessDB.QueryBuilder.ClickHouse;
using AccessDB;
using Qoollo.ClickHouse.Net.Repository;
using AccessDB.DTO;
using Qoollo.ClickHouse.Net.ConnectionPool.Configuration;
using Qoollo.ClickHouse.Net.ConnectionPool;
using ModelLogic.Controllers;
using ModelLogic.Models;
using Npgsql;
using Serilog;
using Serilog.Core;
using AccessDB.Repositories.PostgreSQL;
using AccessDB.DbModels.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace View
{
    public partial class LoginForm : Form
    {
        IConfiguration _config;
        IHost _host;
        IHostBuilder _builder;

        DBType db = DBType.None;

        Logger serilogLogger;
        ClickHouseConnectionPoolConfiguration _clickConfig = new ClickHouseConnectionPoolConfiguration();
        ClickHouseConnectionSettings _connectionSettings;
        private string _connectionString;
        LoginInfo _loginInfo = new LoginInfo("", new List<RoleDTO>());
        NpgsqlConnectionStringBuilder _postgresBuilder = new NpgsqlConnectionStringBuilder();
        public LoginForm()
        {
            _config = new ConfigurationBuilder()
                   .AddJsonFile("config.json")
                   .Build();

            ParseConfig(_config);
            RebuildLoginDI();

            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string pass = textBoxPass.Text;
            if (login == "")
            {
                MessageBox.Show("Введите логин!");
                return;
            }

            ChangeUser(login, pass);
            RebuildLoginDI();

            bool isSuccess = LoginProcess();

            if (isSuccess)
            {
                RebuildLandingDI();
                OpenLandingForm();
            }
        }

        private void OpenLandingForm()
        {
            using (var serviceScope = _host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                    MessageBox.Show("Логин успешен!");
                    this.Hide();
                    var landing = services.GetRequiredService<LandingForm>();
                    landing.Show();

            }
        }

        private void ParseConfig(IConfiguration _config)
        {
            _clickConfig = _config.GetSection("ClickHouseConnectionPoolConfiguration").Get<ClickHouseConnectionPoolConfiguration>();
            if ((_clickConfig != null) && (_clickConfig.ConnectionStrings.Count() > 0))
            {
                string connectionString = _clickConfig.ConnectionStrings[0];
                _connectionSettings = new ClickHouseConnectionSettings(connectionString);
                db = DBType.ClickHouse;
            }
            else
            {
                db = DBType.None;
                MessageBox.Show("Ошибка при чтении конфига. Проверьте наличие и корректность секции 'ClickHouseConnectionPoolConfiguration' или `PostgresConfig`");
                Application.Exit();
            }
            serilogLogger = new LoggerConfiguration()
                        .WriteTo.File(_config["LoggerFile"])
                        .CreateLogger();
        }

        private bool LoginProcess()
        {
            using (var serviceScope = _host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var rep = services.GetRequiredService<IUserManagmentRepository>();
                List<RoleDTO> roles = new List<RoleDTO>();
                var logger = services.GetRequiredService<ILogger<LoginForm>>();
                try
                {
                    roles = rep.GetCurrentRoles().ToList();
                }
                catch (Exception exep)
                {
                    logger.LogError("Error: {error}. Trace: {trace}", exep.Message, exep.StackTrace);
                    MessageBox.Show(exep.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (roles.Count() == 0)
                {
                    MessageBox.Show("Пользователь с таким логином не найден!");
                    return false;
                };

                _loginInfo = new LoginInfo(_loginInfo.Username, roles);

                return true;
            }
        }

        private void ChangeUser(string login, string pass)
        {
            _loginInfo = new LoginInfo(login, _loginInfo.Roles);
            _connectionSettings.User = login;
            _connectionSettings.Password = pass;
            _clickConfig.ConnectionStrings = new List<string> { _connectionSettings.ToString() };
            _postgresBuilder.Username = login;
            _postgresBuilder.Password = pass;
        }

        private void RebuildLoginDI()
        {
            _builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    if (db == DBType.ClickHouse)
                    {
                        DiExtensions.AddLoginLogicExtensionsClickHouse(services, _clickConfig, serilogLogger);
                    }
                    else
                    {
                        DiExtensions.AddLoginLogicExtensionsPostgres(services, _postgresBuilder.ConnectionString);
                    }
                });

            if (_host != null)
            {
                var t = _host.StopAsync();
                t.Wait();
            }
            _host = _builder.Build();
        }

        private void RebuildLandingDI()
        {
            _builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    if (db == DBType.ClickHouse)
                    {
                        DiExtensions.AddQueryBuildersClickHouseDI(services);
                        DiExtensions.AddEntityMappersDI(services);
                        DiExtensions.AddRepositoriesClickHouseDI(services);
                        DiExtensions.AddClickHouseDI(services, _clickConfig);
                        DiExtensions.AddControllersDI(services);
                    }
                    else
                    {
                        DiExtensions.AddEntityMappersDI(services);
                        DiExtensions.AddRepositoriesPostgresDI(services);
                        DiExtensions.AddPostgresDI(services, _postgresBuilder.ConnectionString);
                        DiExtensions.AddControllersDI(services);
                    }
                    services.AddSingleton<LandingForm>();
                    services.AddSingleton(provider => { return _loginInfo; });
                    services.AddLogging(x =>
                    {
                        x.AddSerilog(logger: serilogLogger, dispose: true);
                    });
                });

            if (_host != null)
            {
                var t = _host.StopAsync();
                t.Wait();
            }
            _host = _builder.Build();
        }

        public static class DiExtensions
        {
            public static void AddLoginLogicExtensionsClickHouse(IServiceCollection services, ClickHouseConnectionPoolConfiguration config, Serilog.ILogger logger)
            {
                AddClickHouseDI(services, config);
                //services.AddClickHouseRepository(_config.GetSection("ClickHouseConnectionPoolConfiguration"));
                services.AddSingleton<IUserManagmentQueryBuilder, UserManagmentQueryBuilderClickHouse>();
                services.AddSingleton<IUserManagmentRepository, UserManagmentRepositoryClickHouse>();
                services.AddSingleton<IEntityMapper<SystemUserDTO>, SystemUserDTOMapper>();
                services.AddLogging(x =>
                {
                    x.AddSerilog(logger: logger, dispose: true);
                });
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

            public static void AddEntityMappersDI(IServiceCollection services)
            {
                services.AddSingleton<IEntityMapper<DataSourceDTO>, DataSourceDTOMapper>();
                services.AddSingleton<IEntityMapper<SourceTypeDTO>, SourceTypeDTOMapper>();
                services.AddSingleton<IEntityMapper<DestinationDTO>, DestinationDTOMapper>();
                services.AddSingleton<IEntityMapper<DestinationTypeDTO>, DestinationTypeDTOMapper>();
                services.AddSingleton<IEntityMapper<DifficultSourcesByPostThatToDestTypeMoreThanCountDTO>, DifficultSourcesByMarkThatToDestTypeMoreThanCountDTOMapper>();
                services.AddSingleton<IEntityMapper<SystemUserDTO>, SystemUserDTOMapper>();
                services.AddSingleton<IEntityMapper<UserInfoDTO>, UserInfoDTOMapper>();
                services.AddSingleton<IEntityMapper<RoleDTO>, RoleDTOMapper>();
                services.AddSingleton<IEntityMapper<AccessDB.DTO.FlowClickHouse>, FlowDTOMapper>();
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

            public static void AddPostgresDI(IServiceCollection services, string connectionString)
            { 
                services.AddDbContext<defaultContext>(option => option.UseNpgsql(connectionString));
                
            }
            public static void AddLoginLogicExtensionsPostgres(IServiceCollection services, string connectionString)
            {   
                AddPostgresDI(services, connectionString);
                //services.AddSingleton<IUserManagmentRepository, UserManagmentRepositoryPostgreSQL>();
                services.AddSingleton<IEntityMapper<SystemUserDTO>, SystemUserDTOMapper>();
                
            }

            public static void AddRepositoriesPostgresDI(IServiceCollection services)
            { 
                services.AddSingleton<IDataSourcesRepository, DataSourcesRepositoryPostgreSQL>();
                services.AddSingleton<IDataSourceTypesRepository, DataSourceTypesRepositoryPostgreSQL>();
                services.AddSingleton<IDestinationsRepository, DestinationsRepositoryPostgreSQL>();
                services.AddSingleton<IDestinationTypesRepository, DestinationTypesRepositoryPostgreSQL>();
                services.AddSingleton<IDifficultRepository, DifficultRepositoryPostgreSQL>();
                services.AddSingleton<IUserInfoRepository, UserInfoRepositoryPostgreSQL>();
                //services.AddSingleton<IUserManagmentRepository, UserManagmentRepositoryPostgreSQL>();
                services.AddSingleton<IFlowsRawRepository, FlowsRawRepositoryPostgreSQL>();
                
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private enum DBType
        {
            ClickHouse,
            Postgres,
            None
        }
    }

}
