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
using Qoollo.ClickHouse.Net;
using AccessDB.Repositories.IRepositories;
using AccessDB.Repositories.ClickHouse;
using AccessDB.QueryBuilder.IQueryBuilder;
using AccessDB.QueryBuilder.ClickHouse;
using AccessDB;
using Qoollo.ClickHouse.Net.Repository;
using AccessDB.DTO;

namespace View
{
    public partial class LoginForm : Form
    {
        IConfiguration _config;
        IHost _host;
        public LoginForm()
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
                    services.AddSingleton<IEntityMapper<SystemUserDTO>, SystemUserDTOMapper>();
                });

            _host = builder.Build();
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

            using (var serviceScope = _host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var rep = services.GetRequiredService<IUserManagmentRepository>();
                var resp = rep.FindUser(login);
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
    }
}
