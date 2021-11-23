using AccessDB.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModelLogic.Models;
using Microsoft.Extensions.Logging;
using ModelLogic.Controllers;
using System.Linq;
using AccessDB.DTO;
using ModelLogic.Utilities;

namespace View
{
    public partial class LandingForm : Form
    {
        private readonly ILogger<LandingForm> _logger;
        private readonly AnalystController _analysts;
        private readonly AdminController _admin;
        private readonly GuestController _guest;
        private readonly UserController _userController;
        private readonly LoginInfo _currentLogin;
        public LandingForm(ILogger<LandingForm> logger, LoginInfo currentLogin, AnalystController analysts, AdminController admin, GuestController guest, UserController userController)
        {
            _analysts = analysts;
            _admin = admin;
            _guest = guest;
            _userController = userController;
            _logger = logger;
            _currentLogin = currentLogin;
            InitializeComponent();
            SettingView();
        }
        private void LandingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void SettingView()
        {
            labelCurrentLogin.Text = _currentLogin.Username;
            foreach (var role in _currentLogin.Roles)
            {
                if (role == Role.Guest)
                {
                    labelCurrentRole.Text += "Гость";
                }
                else if (role == Role.Analyst)
                {
                    labelCurrentRole.Text += " Аналитик";
                }
                else if (role == Role.Admin)
                {
                    labelCurrentRole.Text += " Админ";
                }

                if (role == Role.Admin)
                {
                    groupBoxAdmin.Enabled = true;
                    groupBoxAnalyst.Enabled = true;
                    groupBoxGuest.Enabled = true;
                }
                else if (role == Role.Analyst)
                {
                    groupBoxAnalyst.Enabled = true;
                    groupBoxGuest.Enabled = true;
                }
                else if (role == Role.Guest)
                {
                    groupBoxGuest.Enabled = true;
                }
            }
        }

        private void ChangeTableSumTypes()
        {
            mainDataGrid.Rows.Clear();
            mainDataGrid.Columns.Clear();
            mainDataGrid.Columns.Add("Ip", "IP");
            mainDataGrid.Columns.Add("Roles", "Сумма трафика");
        }
        
        private void ChangeTableUsers()
        {
            mainDataGrid.Rows.Clear();
            mainDataGrid.Columns.Clear();
            mainDataGrid.Columns.Add("Login", "Логин");
            mainDataGrid.Columns.Add("Roles", "Роли");
        }

        private void ChangeTableOwners()
        {
            mainDataGrid.Rows.Clear();
            mainDataGrid.Columns.Clear();
            mainDataGrid.Columns.Add("UUID", "UUID");
            mainDataGrid.Columns.Add("Name", "Имя");
            mainDataGrid.Columns.Add("Post", "Должность");
        }
        private void ChangeTableSources()
        {
            mainDataGrid.Rows.Clear();
            mainDataGrid.Columns.Clear();
            mainDataGrid.Columns.Add("Ip", "IP");
            mainDataGrid.Columns.Add("OwnerUUID", "UUID Владельца");
            mainDataGrid.Columns.Add("SourceType", "Тип источника");
        }

        private void ChangeTableDestinations()
        {
            mainDataGrid.Rows.Clear();
            mainDataGrid.Columns.Clear();
            mainDataGrid.Columns.Add("Ip", "IP");
            mainDataGrid.Columns.Add("SourceDest", "Тип узла назначения");
        }

        private void ChangeTableSourceTypes()
        {
            mainDataGrid.Rows.Clear();
            mainDataGrid.Columns.Clear();
            mainDataGrid.Columns.Add("Type", "Тип");
            mainDataGrid.Columns.Add("CommentString", "Комментарий");
        }
        private void ChangeTableDestTypes()
        {
            mainDataGrid.Rows.Clear();
            mainDataGrid.Columns.Clear();
            mainDataGrid.Columns.Add("Type", "Тип");
            mainDataGrid.Columns.Add("CommentString", "Комментарий");
        }

        private void ChangeTableMaxSpend()
        {
            mainDataGrid.Rows.Clear();
            mainDataGrid.Columns.Clear();
            mainDataGrid.Columns.Add("Type", "День");
            mainDataGrid.Columns.Add("CommentString", "Ip");
            mainDataGrid.Columns.Add("CommentString", "Потрачено трафика в байтах");
        }

        private void ChangeTableFlowDTOTypes()
        {
            mainDataGrid.Rows.Clear();
            mainDataGrid.Columns.Clear();
            mainDataGrid.Columns.Add("TimeReceived", "Время получения данных");
            mainDataGrid.Columns.Add("TimeFlowStart", "Время начала потока");
            mainDataGrid.Columns.Add("SequenceNum", "Номер в последовательности");
            // Кол-во "схлопнутых пакетов"
            mainDataGrid.Columns.Add("SamplingRate", "Sampling Rate");
            // Sampler info
            mainDataGrid.Columns.Add("SamplerAddress", "SamplerAddress");
            mainDataGrid.Columns.Add("SrcAddr", "Адрес источника");
            mainDataGrid.Columns.Add("DstAddr", "Адрес назначения");
            mainDataGrid.Columns.Add("SrcAS", "Автономная система (AS). Источник");
            mainDataGrid.Columns.Add("DstAS", "Автономная система (AS). Назначение");
            mainDataGrid.Columns.Add("EType", "Etype");
            mainDataGrid.Columns.Add("Proto", "Proto");
            mainDataGrid.Columns.Add("SrcPort", "Порт источника");
            mainDataGrid.Columns.Add("DstPort", "Порт назначения");

            // Size of the sampled packet
            mainDataGrid.Columns.Add("Bytes", "Размер в байтах");
            mainDataGrid.Columns.Add("Packets", "Кол-во пакетов");
        }

        #region Admin Functions
        private void buttonAdminGetUsers_Click(object sender, EventArgs e)
        {
            ChangeTableUsers();
            List<LoginInfo> users = null;
            try
            {
                users = _admin.FindAllSystemUsers();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
                    mainDataGrid.Rows.Add(user.Username, roleRepresent);
                }
            }
            else
            {
                MessageBox.Show("Пользователи не найдены");
            }
        }


        private void buttonAdminCreateUser_Click(object sender, EventArgs e)
        {
            string login = textBoxAdminLogin.Text.Trim();
            string pass = textBoxAdminPass.Text;
            bool valid = true;
            foreach (var charl in login)
            {
                if (!Char.IsLetterOrDigit(charl))
                {
                    valid = false;
                    break;
                }
            }
            if (login.Length == 0 || !valid || !Char.IsLetter(login[0]))
            {
                MessageBox.Show("Неверный формат логина");
                MessageBox.Show(login + "   '" + textBoxAdminLogin.Text + "'   " + textBoxAdminLogin.Text);
                return;
            }
            try
            {
                _admin.CreateSystemUser(login, pass);
                _admin.GrantUserRole(login, Role.Guest);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            string login = textBoxLoginDelete.Text.Trim();
            try
            {
                _admin.DeleteSystemUser(login);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttomGiveRole_Click(object sender, EventArgs e)
        {
            string login = textBoxRoleLogin.Text.Trim();
            try
            {
                _admin.GrantUserRole(login, RoleExtension.RoleStringToEnum(listBoxRole.SelectedItem.ToString()));
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonTakeRole_Click(object sender, EventArgs e)
        {
            string login = textBoxRoleLogin.Text.Trim();
            try
            {
                _admin.RevokeUserRole(login, RoleExtension.RoleStringToEnum(listBoxRole.SelectedItem.ToString()));
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void buttonOwners_Click(object sender, EventArgs e)
        {
            ChangeTableOwners();
            List<UserInfo> owners = null;
            try
            {
                owners = _userController.FindAllUserInfo();
            }
            catch (Exception error)
            {
                _logger.LogError("Error at {dateTime}. Error: {error}. Trace: {trace}", DateTime.UtcNow, error.Message, error.StackTrace);
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (owners != null)
            {
                foreach (var owner in owners)
                {
                    mainDataGrid.Rows.Add(owner.UUID, owner.Name, owner.Post);
                }
            }
            else
            {
                MessageBox.Show("Владельцы не найдены");
            }
        }

        private void buttonSources_Click(object sender, EventArgs e)
        {
            ChangeTableSources();
            List<DataSource> sources = null;
            try
            {
                sources = _userController.FindAllDataSources();
            }
            catch (Exception error)
            {
                _logger.LogError("Error at {dateTime}. Error: {error}. Trace: {trace}", DateTime.UtcNow, error.Message, error.StackTrace);
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (sources != null)
            {
                foreach (var source in sources)
                {
                    mainDataGrid.Rows.Add(source.Ip, source.OwnerUUID, source.SourceType);
                }
            }
            else
            {
                MessageBox.Show("Источники не найдены");
            }
        }

        private void buttonDestinations_Click(object sender, EventArgs e)
        {
            ChangeTableDestinations();
            List<Destination> destinations = null;
            try
            {
                destinations = _userController.FindAllDestinations();
            }
            catch (Exception error)
            {
                _logger.LogError("Error at {dateTime}. Error: {error}. Trace: {trace}", DateTime.UtcNow, error.Message, error.StackTrace);
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (destinations != null)
            {
                foreach (var destination in destinations)
                {
                    mainDataGrid.Rows.Add(destination.Ip, destination.Type);
                }
            }
            else
            {
                MessageBox.Show("Узлы назначения не найдены");
            }
        }

        private void buttonTypesSource_Click(object sender, EventArgs e)
        {
            ChangeTableSourceTypes();
            List<SourceType> types = null;
            try
            {
                types = _userController.FindAllSourceTypes();
            }
            catch (Exception error)
            {
                _logger.LogError("Error at {dateTime}. Error: {error}. Trace: {trace}", DateTime.UtcNow, error.Message, error.StackTrace);
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (types != null)
            {
                foreach (var type in types)
                {
                    mainDataGrid.Rows.Add(type.Type, type.CommentString);
                }
            }
            else
            {
                MessageBox.Show("Типы источников не найдены");
            }
        }

        private void buttonTypeDest_Click(object sender, EventArgs e)
        {
            ChangeTableDestTypes();
            List<DestinationType> types = null;
            try
            {
                types = _userController.FindAllDestinationTypes();
            }
            catch (Exception error)
            {
                _logger.LogError("Error at {dateTime}. Error: {error}. Trace: {trace}", DateTime.UtcNow, error.Message, error.StackTrace);
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (types != null)
            {
                foreach (var type in types)
                {
                    mainDataGrid.Rows.Add(type.Type, type.CommentString);
                }
            }
            else
            {
                MessageBox.Show("Типы узлов назначения не найдены");
            }
        }

        private void buttonTrafficMinutes_Click(object sender, EventArgs e)
        {
            int minutes = 0;
            try
            {
                minutes = Int32.Parse(textBoxMinutes.Text);
            }
            catch (Exception error)
            {
                _logger.LogError("Error at {dateTime}. Error: {error}. Trace: {trace}", DateTime.UtcNow, error.Message, error.StackTrace);
                MessageBox.Show("Введите корректное целое число", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ChangeTableFlowDTOTypes();
            List<Flow> flows = null;
            try
            {
                flows = _userController.FindFlowByMinutes(minutes).ToList();
            }
            catch (Exception error)
            {
                _logger.LogError("Error at {dateTime}. Error: {error}. Trace: {trace}", DateTime.UtcNow, error.Message, error.StackTrace);
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (flows != null)
            {
                foreach (var flow in flows)
                {
                    mainDataGrid.Rows.Add(  flow.TimeReceived.ToString(),
                                            flow.TimeFlowStart.ToString(),
                                            flow.SequenceNum.ToString(),
                                            flow.SamplingRate.ToString(),
                                            flow.SamplerAddress.ToString(),
                                            flow.SrcAddr.ToString(),
                                            flow.DstAddr.ToString(),
                                            flow.SrcAS.ToString(),
                                            flow.DstAS.ToString(),
                                            flow.EType.ToString(),
                                            flow.Proto.ToString(),
                                            flow.SrcPort.ToString(),
                                            flow.DstPort.ToString(),
                                            flow.Bytes.ToString(),
                                            flow.Packets.ToString());
                }
            }
            else
            {
                MessageBox.Show("Потоки не найдены");
            }
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Dispose();
            LoginForm form = new LoginForm();
            form.Show();
            this.Close();
        }

        private void buttonr_Click(object sender, EventArgs e)
        {
            int minutes = 0;
            try
            {
                minutes = Int32.Parse(textBoxMinutes2.Text);
            }
            catch (Exception error)
            {
                _logger.LogError("Error at {dateTime}. Error: {error}. Trace: {trace}", DateTime.UtcNow, error.Message, error.StackTrace);
                MessageBox.Show("Введите корректное целое число", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ChangeTableSumTypes();
            List<SumDTO> sum = null;
            try
            {
                sum = _analysts.FindSum(minutes);
            }
            catch (Exception error)
            {
                _logger.LogError("Error at {dateTime}. Error: {error}. Trace: {trace}", DateTime.UtcNow, error.Message, error.StackTrace);
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (sum != null)
            {
                foreach (var sumv in sum)
                {
                    mainDataGrid.Rows.Add(IpTransformer.MaskToString(Encoding.ASCII.GetBytes(sumv.SrcAddr)), sumv.Sum);
                }
            }
            else
            {
                MessageBox.Show("Типы узлов назначения не найдены");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeTableMaxSpend();
            List<MaxSpendingDTO> days = null;
            try
            {
                MessageBox.Show("В разработке", "Подождите", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                days = _analysts.GetMaxSpendingDay();
            }
            catch (Exception error)
            {
                _logger.LogError("Error at {dateTime}. Error: {error}. Trace: {trace}", DateTime.UtcNow, error.Message, error.StackTrace);
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (days != null)
            {
                foreach (var day in days)
                {
                    mainDataGrid.Rows.Add(day.Date.Date.ToString(), IpTransformer.MaskToString(Encoding.ASCII.GetBytes(day.SrcIp)), day.Spend);
                }
            }
            else
            {
                MessageBox.Show("Типы узлов назначения не найдены");
            }
        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int minutes = 0;
            try
            {
                minutes = Int32.Parse(textBoxMinutes2.Text);
            }
            catch (Exception error)
            {
                _logger.LogError("Error at {dateTime}. Error: {error}. Trace: {trace}", DateTime.UtcNow, error.Message, error.StackTrace);
                MessageBox.Show("Введите корректное целое число", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ChangeTableFlowDTOTypes();
            List<Flow> flows = null;
            try
            {
                flows = _userController.FindFlowByMinutes(minutes).ToList();
            }
            catch (Exception error)
            {
                _logger.LogError("Error at {dateTime}. Error: {error}. Trace: {trace}", DateTime.UtcNow, error.Message, error.StackTrace);
                MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (flows != null)
            {
                foreach (var flow in flows)
                {
                    mainDataGrid.Rows.Add(flow.TimeReceived.ToString(),
                                            flow.TimeFlowStart.ToString(),
                                            flow.SequenceNum.ToString(),
                                            flow.SamplingRate.ToString(),
                                            flow.SamplerAddress.ToString(),
                                            flow.SrcAddr.ToString(),
                                            flow.DstAddr.ToString(),
                                            flow.SrcAS.ToString(),
                                            flow.DstAS.ToString(),
                                            flow.EType.ToString(),
                                            flow.Proto.ToString(),
                                            flow.SrcPort.ToString(),
                                            flow.DstPort.ToString(),
                                            flow.Bytes.ToString(),
                                            flow.Packets.ToString());
                }
            }
            else
            {
                MessageBox.Show("Потоки не найдены");
            }
        }
    }
}
