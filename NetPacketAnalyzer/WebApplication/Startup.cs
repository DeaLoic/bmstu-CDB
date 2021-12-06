using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;

using ClickHouse.Ado;
using AccessDB.Repositories.IRepositories;
using AccessDB.Repositories.ClickHouse;
using AccessDB.QueryBuilder.IQueryBuilder;
using AccessDB.QueryBuilder.ClickHouse;
using Qoollo.ClickHouse.Net.Repository;
using Qoollo.ClickHouse.Net.ConnectionPool.Configuration;
using Qoollo.ClickHouse.Net.ConnectionPool;
using ModelLogic.Controllers;
using Serilog;
using AccessDB.Repositories.PostgreSQL;
using AccessDB.DbModels.PostgreSQL;
using AccessDB.DbModels.ClickHouse;
using Microsoft.EntityFrameworkCore;


namespace WebApplication
{
    public class Startup
    {
        ClickHouseConnectionPoolConfiguration _clickConfig = new ClickHouseConnectionPoolConfiguration();
        ClickHouseConnectionSettings _connectionSettings;

        string _connectionStringPostgres = null;
        public IConfiguration _config { get; }

        public Startup(IConfiguration configuration)
        {
            Console.WriteLine("START");
            _config = configuration;

        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ParseConfig();
            DiExtensions.AddClickHouseDI(services, _clickConfig);
            DiExtensions.AddControllersDI(services);
            DiExtensions.AddPostgresDI(services, _connectionStringPostgres);
            DiExtensions.AddEntityMappersDI(services);
            DiExtensions.AddQueryBuildersClickHouseDI(services);
            DiExtensions.AddRepositoriesClickHouseDI(services);
            DiExtensions.AddRepositoriesPostgresDI(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SystemAPI", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SystemAPI v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ParseConfig()
        {
            _clickConfig = _config.GetSection("ClickHouseConnectionPoolConfiguration").Get<ClickHouseConnectionPoolConfiguration>();
            _connectionStringPostgres = _config["PostgresConnectionString"];
            if ((_clickConfig != null) && (_clickConfig.ConnectionStrings.Count() > 0) && _connectionStringPostgres != null)
            {
                string connectionString = _clickConfig.ConnectionStrings[0];
                Console.WriteLine(connectionString);
                _connectionSettings = new ClickHouseConnectionSettings(connectionString);
            }
            else
            {
                Console.WriteLine("Ошибка при чтении конфига. Проверьте наличие и корректность секции 'ClickHouseConnectionPoolConfiguration', `HostPort` и 'PostgresConnectionString'");
                throw new Exception();
            }
        }

        private void AddLoggerExtensions(IServiceCollection services)
        {
            var serilogLogger = new LoggerConfiguration()
                .WriteTo.File(_config["Logger"])
                .CreateLogger();

            services.AddLogging(x =>
            {
                x.AddSerilog(logger: serilogLogger, dispose: true);
            });
        }

        public static class DiExtensions
        {
            public static void AddRepositoriesClickHouseDI(IServiceCollection services)
            {
                services.AddScoped<IFlowsRawRepository, FlowsRawRepositoryClickHouse>();
            }
            public static void AddQueryBuildersClickHouseDI(IServiceCollection services)
            {
                services.AddScoped<IFlowsRawQueryBuilder, FlowsRawQueryBuilder>();
            }

            public static void AddEntityMappersDI(IServiceCollection services)
            {
                services.AddScoped<IEntityMapper<FlowClickHouse>, FlowClickHouseMapperDB>();
            }
            public static void AddClickHouseDI(IServiceCollection services, ClickHouseConnectionPoolConfiguration config)
            {
                services.AddTransient<IClickHouseConnectionPoolConfiguration>(serviceProvider => config);
                services.AddTransient<ClickHouseConnectionPool>();
                services.AddTransient<IClickHouseRepository, ClickHouseRepository>();
            }
            public static void AddControllersDI(IServiceCollection services)
            {
                services.AddScoped<CredentialsController>();
                services.AddScoped<FlowsRawController>();
            }

            public static void AddPostgresDI(IServiceCollection services, string connectionString)
            {
                services.AddDbContext<defaultContext>(option => option.UseNpgsql(connectionString));

            }

            public static void AddRepositoriesPostgresDI(IServiceCollection services)
            {
                services.AddScoped<ICredentialsRepository, CredentialsRepositoryPostgres>();

            }
        }
    }
}
