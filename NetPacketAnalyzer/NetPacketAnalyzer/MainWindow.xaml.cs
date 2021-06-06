using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClickHouse.Client.ADO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AccessDB.QueryBuilder;
using Qoollo.ClickHouse.Net;
using Qoollo.ClickHouse.Net.Repository;
using AccessDB.QueryBuilder.IQueryBuilder;
using AccessDB.QueryBuilder.ClickHouse;
using AccessDB.Repositories.IRepositories;
using AccessDB.Repositories.ClickHouse;
using ClickHouse.Ado;

namespace NetPacketAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        IConfiguration _config;
        IHost _host;
        public LoginWindow()
        {
            _config = new ConfigurationBuilder()
                    .AddJsonFile("config.json")
                    .Build();
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddClickHouseRepository(_config.GetSection("ClickHouseConnectionPoolConfiguration"));
                    services.AddSingleton<IUserManagmentQueryBuilder, UserManagmentQueryBuilderClickHouse>();
                    services.AddSingleton<IUserManagmentRepository, UserManagmentRepositoryClickHouse>();
                });

            _host = builder.Build();
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {   
            if (mainTextBoxLogin.Text == "")
            {
                MessageBox.Show("Введите логин!");
                return;
            }

            using (var serviceScope = _host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                
                var rep = services.GetRequiredService<IUserManagmentRepository>();
                var resp = rep.FindUser(mainTextBoxLogin.Text);
                var users = resp.ToList();
                if (users.Count() == 0)
                {
                    MessageBox.Show("Пользователь с таким логином не найден!");
                }
                else
                {
                    string roles = "";
                    foreach (var i in users[0].Roles)
                    {
                        roles += i;
                    }
                    MessageBox.Show(users[0].Login + " " + roles);
                }
        }
    }
    public static class DiExtensions
    {
        public static void AddRepositoryExtensions(IServiceCollection services)
        {
        }
        public static void AddControllerExtensions(IServiceCollection services)
        {
        }
    }
}
