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

namespace View
{
    public partial class LoginForm : Form
    {
        IConfiguration _config;
        IHost _host;
        IHostBuilder _builder;

        ClickHouseConnectionPoolConfiguration _clickConfig = new ClickHouseConnectionPoolConfiguration();
        ClickHouseConnectionSettings _connectionSettings;
        LoginInfo _loginInfo = new LoginInfo("", new List<RoleDTO>());
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
            }
            else
            {
                MessageBox.Show("Ошибка при чтении конфига. Проверьте наличие и корректность секции 'ClickHouseConnectionPoolConfiguration'");
                Application.Exit();
            }
        }

        private bool LoginProcess()
        {
            using (var serviceScope = _host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var rep = services.GetRequiredService<IUserManagmentRepository>();
                List<RoleDTO> roles = new List<RoleDTO>();
                try
                {
                    roles = rep.GetCurrentRoles().ToList();
                }
                catch (Exception exep)
                {
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
        }

        private void RebuildLoginDI()
        {
            _builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    DiExtensions.AddLoginLogicExtensions(services, _clickConfig);
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
                    DiExtensions.AddQueryBuildersClickHouseDI(services);
                    DiExtensions.AddEntityMappersClickHouseDI(services);
                    DiExtensions.AddRepositoriesClickHouseDI(services);
                    DiExtensions.AddClickHouseDI(services, _clickConfig);
                    DiExtensions.AddControllersDI(services);
                    services.AddSingleton<LandingForm>();
                    services.AddSingleton(provider => { return _loginInfo; });
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
                services.AddSingleton<ICrudPlusRepository<DataSourceDTO>, CrudPlusRepositoryClickHouse<DataSourceDTO>>();
                services.AddSingleton<IDataSourcesRepository, DataSourcesRepositoryClickHouse>();
                services.AddSingleton<IDataSourceTypesRepository, DataSourceTypesRepositoryClickHouse>();
                services.AddSingleton<IDestinationsRepository, DestinationsRepositoryClickHouse>();
                services.AddSingleton<IDestinationTypesRepository, DestinationTypesRepositoryClickHouse>();
                services.AddSingleton<IDifficultRepository, DifficultRepositoryClickHouse>();
                services.AddSingleton<IUserInfoRepository, UserInfoRepositoryClickHouse>();
                services.AddSingleton<IUserManagmentRepository, UserManagmentRepositoryClickHouse>();
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
}
