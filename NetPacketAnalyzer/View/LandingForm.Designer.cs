
namespace View
{
    partial class LandingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainDataGrid = new System.Windows.Forms.DataGridView();
            this.groupBoxAdmin = new System.Windows.Forms.GroupBox();
            this.buttonTakeRole = new System.Windows.Forms.Button();
            this.listBoxRole = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRoleLogin = new System.Windows.Forms.TextBox();
            this.buttomGiveRole = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLoginDelete = new System.Windows.Forms.TextBox();
            this.buttonDeleteUser = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAdminLogin = new System.Windows.Forms.TextBox();
            this.textBoxAdminPass = new System.Windows.Forms.TextBox();
            this.buttonAdminCreateUser = new System.Windows.Forms.Button();
            this.buttonAdminGetUsers = new System.Windows.Forms.Button();
            this.labelCurrentLogin = new System.Windows.Forms.Label();
            this.labelCurrentRole = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBoxGuest = new System.Windows.Forms.GroupBox();
            this.buttonTypeDest = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxMinutes = new System.Windows.Forms.TextBox();
            this.buttonTrafficMinutes = new System.Windows.Forms.Button();
            this.buttonTypesSource = new System.Windows.Forms.Button();
            this.buttonDestinations = new System.Windows.Forms.Button();
            this.buttonSources = new System.Windows.Forms.Button();
            this.buttonOwners = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBoxAnalyst = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxMinutes2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonr = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonLogout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGrid)).BeginInit();
            this.groupBoxAdmin.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBoxGuest.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBoxAnalyst.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainDataGrid
            // 
            this.mainDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainDataGrid.Location = new System.Drawing.Point(12, 12);
            this.mainDataGrid.Name = "mainDataGrid";
            this.mainDataGrid.Size = new System.Drawing.Size(606, 426);
            this.mainDataGrid.TabIndex = 0;
            // 
            // groupBoxAdmin
            // 
            this.groupBoxAdmin.Controls.Add(this.buttonTakeRole);
            this.groupBoxAdmin.Controls.Add(this.listBoxRole);
            this.groupBoxAdmin.Controls.Add(this.label5);
            this.groupBoxAdmin.Controls.Add(this.label4);
            this.groupBoxAdmin.Controls.Add(this.textBoxRoleLogin);
            this.groupBoxAdmin.Controls.Add(this.buttomGiveRole);
            this.groupBoxAdmin.Controls.Add(this.label3);
            this.groupBoxAdmin.Controls.Add(this.textBoxLoginDelete);
            this.groupBoxAdmin.Controls.Add(this.buttonDeleteUser);
            this.groupBoxAdmin.Controls.Add(this.label2);
            this.groupBoxAdmin.Controls.Add(this.label1);
            this.groupBoxAdmin.Controls.Add(this.textBoxAdminLogin);
            this.groupBoxAdmin.Controls.Add(this.textBoxAdminPass);
            this.groupBoxAdmin.Controls.Add(this.buttonAdminCreateUser);
            this.groupBoxAdmin.Controls.Add(this.buttonAdminGetUsers);
            this.groupBoxAdmin.Enabled = false;
            this.groupBoxAdmin.Location = new System.Drawing.Point(12, 3);
            this.groupBoxAdmin.Name = "groupBoxAdmin";
            this.groupBoxAdmin.Size = new System.Drawing.Size(170, 344);
            this.groupBoxAdmin.TabIndex = 1;
            this.groupBoxAdmin.TabStop = false;
            this.groupBoxAdmin.Text = "Панель управления";
            // 
            // buttonTakeRole
            // 
            this.buttonTakeRole.Location = new System.Drawing.Point(89, 296);
            this.buttonTakeRole.Name = "buttonTakeRole";
            this.buttonTakeRole.Size = new System.Drawing.Size(75, 42);
            this.buttonTakeRole.TabIndex = 14;
            this.buttonTakeRole.Text = "Забрать роль";
            this.buttonTakeRole.UseVisualStyleBackColor = true;
            this.buttonTakeRole.Click += new System.EventHandler(this.buttonTakeRole_Click);
            // 
            // listBoxRole
            // 
            this.listBoxRole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxRole.FormattingEnabled = true;
            this.listBoxRole.ItemHeight = 15;
            this.listBoxRole.Items.AddRange(new object[] {
            "guest",
            "analyst",
            "admin"});
            this.listBoxRole.Location = new System.Drawing.Point(70, 273);
            this.listBoxRole.Name = "listBoxRole";
            this.listBoxRole.Size = new System.Drawing.Size(94, 17);
            this.listBoxRole.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 273);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Роль";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 245);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Логин";
            // 
            // textBoxRoleLogin
            // 
            this.textBoxRoleLogin.Location = new System.Drawing.Point(70, 242);
            this.textBoxRoleLogin.Name = "textBoxRoleLogin";
            this.textBoxRoleLogin.Size = new System.Drawing.Size(93, 23);
            this.textBoxRoleLogin.TabIndex = 10;
            // 
            // buttomGiveRole
            // 
            this.buttomGiveRole.Location = new System.Drawing.Point(6, 296);
            this.buttomGiveRole.Name = "buttomGiveRole";
            this.buttomGiveRole.Size = new System.Drawing.Size(75, 42);
            this.buttomGiveRole.TabIndex = 9;
            this.buttomGiveRole.Text = "Выдать роль";
            this.buttomGiveRole.UseVisualStyleBackColor = true;
            this.buttomGiveRole.Click += new System.EventHandler(this.buttomGiveRole_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Логин";
            // 
            // textBoxLoginDelete
            // 
            this.textBoxLoginDelete.Location = new System.Drawing.Point(71, 181);
            this.textBoxLoginDelete.Name = "textBoxLoginDelete";
            this.textBoxLoginDelete.Size = new System.Drawing.Size(93, 23);
            this.textBoxLoginDelete.TabIndex = 7;
            // 
            // buttonDeleteUser
            // 
            this.buttonDeleteUser.Location = new System.Drawing.Point(6, 210);
            this.buttonDeleteUser.Name = "buttonDeleteUser";
            this.buttonDeleteUser.Size = new System.Drawing.Size(158, 26);
            this.buttonDeleteUser.TabIndex = 6;
            this.buttonDeleteUser.Text = "Удалить пользователя";
            this.buttonDeleteUser.UseVisualStyleBackColor = true;
            this.buttonDeleteUser.Click += new System.EventHandler(this.buttonDeleteUser_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Пароль";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Логин";
            // 
            // textBoxAdminLogin
            // 
            this.textBoxAdminLogin.Location = new System.Drawing.Point(71, 73);
            this.textBoxAdminLogin.Name = "textBoxAdminLogin";
            this.textBoxAdminLogin.Size = new System.Drawing.Size(93, 23);
            this.textBoxAdminLogin.TabIndex = 3;
            // 
            // textBoxAdminPass
            // 
            this.textBoxAdminPass.Location = new System.Drawing.Point(71, 111);
            this.textBoxAdminPass.Name = "textBoxAdminPass";
            this.textBoxAdminPass.Size = new System.Drawing.Size(93, 23);
            this.textBoxAdminPass.TabIndex = 2;
            // 
            // buttonAdminCreateUser
            // 
            this.buttonAdminCreateUser.Location = new System.Drawing.Point(6, 140);
            this.buttonAdminCreateUser.Name = "buttonAdminCreateUser";
            this.buttonAdminCreateUser.Size = new System.Drawing.Size(158, 26);
            this.buttonAdminCreateUser.TabIndex = 1;
            this.buttonAdminCreateUser.Text = "Добавить пользователя";
            this.buttonAdminCreateUser.UseVisualStyleBackColor = true;
            this.buttonAdminCreateUser.Click += new System.EventHandler(this.buttonAdminCreateUser_Click);
            // 
            // buttonAdminGetUsers
            // 
            this.buttonAdminGetUsers.Location = new System.Drawing.Point(6, 22);
            this.buttonAdminGetUsers.Name = "buttonAdminGetUsers";
            this.buttonAdminGetUsers.Size = new System.Drawing.Size(158, 40);
            this.buttonAdminGetUsers.TabIndex = 0;
            this.buttonAdminGetUsers.Text = "Отобразить всех пользователей";
            this.buttonAdminGetUsers.UseVisualStyleBackColor = true;
            this.buttonAdminGetUsers.Click += new System.EventHandler(this.buttonAdminGetUsers_Click);
            // 
            // labelCurrentLogin
            // 
            this.labelCurrentLogin.AutoSize = true;
            this.labelCurrentLogin.Location = new System.Drawing.Point(686, 402);
            this.labelCurrentLogin.Name = "labelCurrentLogin";
            this.labelCurrentLogin.Size = new System.Drawing.Size(0, 15);
            this.labelCurrentLogin.TabIndex = 2;
            // 
            // labelCurrentRole
            // 
            this.labelCurrentRole.AutoSize = true;
            this.labelCurrentRole.Location = new System.Drawing.Point(686, 425);
            this.labelCurrentRole.Name = "labelCurrentRole";
            this.labelCurrentRole.Size = new System.Drawing.Size(0, 15);
            this.labelCurrentRole.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(640, 402);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "Логин";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(645, 423);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 15);
            this.label9.TabIndex = 5;
            this.label9.Text = "Роли";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Controls.Add(this.tabPage3);
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Location = new System.Drawing.Point(624, 12);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(202, 378);
            this.tabControlMain.TabIndex = 6;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControlMain_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBoxGuest);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(194, 350);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Просмотр";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBoxGuest
            // 
            this.groupBoxGuest.Controls.Add(this.buttonTypeDest);
            this.groupBoxGuest.Controls.Add(this.label6);
            this.groupBoxGuest.Controls.Add(this.textBoxMinutes);
            this.groupBoxGuest.Controls.Add(this.buttonTrafficMinutes);
            this.groupBoxGuest.Controls.Add(this.buttonTypesSource);
            this.groupBoxGuest.Controls.Add(this.buttonDestinations);
            this.groupBoxGuest.Controls.Add(this.buttonSources);
            this.groupBoxGuest.Controls.Add(this.buttonOwners);
            this.groupBoxGuest.Enabled = false;
            this.groupBoxGuest.Location = new System.Drawing.Point(12, 3);
            this.groupBoxGuest.Name = "groupBoxGuest";
            this.groupBoxGuest.Size = new System.Drawing.Size(179, 344);
            this.groupBoxGuest.TabIndex = 2;
            this.groupBoxGuest.TabStop = false;
            this.groupBoxGuest.Text = "Панель управления";
            // 
            // buttonTypeDest
            // 
            this.buttonTypeDest.Location = new System.Drawing.Point(6, 206);
            this.buttonTypeDest.Name = "buttonTypeDest";
            this.buttonTypeDest.Size = new System.Drawing.Size(165, 40);
            this.buttonTypeDest.TabIndex = 14;
            this.buttonTypeDest.Text = "Отобразить расшифровку типов назначения";
            this.buttonTypeDest.UseVisualStyleBackColor = true;
            this.buttonTypeDest.Click += new System.EventHandler(this.buttonTypeDest_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 269);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Минуты";
            // 
            // textBoxMinutes
            // 
            this.textBoxMinutes.Location = new System.Drawing.Point(71, 266);
            this.textBoxMinutes.Name = "textBoxMinutes";
            this.textBoxMinutes.Size = new System.Drawing.Size(93, 23);
            this.textBoxMinutes.TabIndex = 12;
            // 
            // buttonTrafficMinutes
            // 
            this.buttonTrafficMinutes.Location = new System.Drawing.Point(6, 295);
            this.buttonTrafficMinutes.Name = "buttonTrafficMinutes";
            this.buttonTrafficMinutes.Size = new System.Drawing.Size(158, 40);
            this.buttonTrafficMinutes.TabIndex = 4;
            this.buttonTrafficMinutes.Text = "Отобразить трафик за последние n минут";
            this.buttonTrafficMinutes.UseVisualStyleBackColor = true;
            this.buttonTrafficMinutes.Click += new System.EventHandler(this.buttonTrafficMinutes_Click);
            // 
            // buttonTypesSource
            // 
            this.buttonTypesSource.Location = new System.Drawing.Point(6, 160);
            this.buttonTypesSource.Name = "buttonTypesSource";
            this.buttonTypesSource.Size = new System.Drawing.Size(165, 40);
            this.buttonTypesSource.TabIndex = 3;
            this.buttonTypesSource.Text = "Отобразить расшифровку типов источников";
            this.buttonTypesSource.UseVisualStyleBackColor = true;
            this.buttonTypesSource.Click += new System.EventHandler(this.buttonTypesSource_Click);
            // 
            // buttonDestinations
            // 
            this.buttonDestinations.Location = new System.Drawing.Point(6, 114);
            this.buttonDestinations.Name = "buttonDestinations";
            this.buttonDestinations.Size = new System.Drawing.Size(165, 40);
            this.buttonDestinations.TabIndex = 2;
            this.buttonDestinations.Text = "Отобразить все узлы назначения";
            this.buttonDestinations.UseVisualStyleBackColor = true;
            this.buttonDestinations.Click += new System.EventHandler(this.buttonDestinations_Click);
            // 
            // buttonSources
            // 
            this.buttonSources.Location = new System.Drawing.Point(6, 68);
            this.buttonSources.Name = "buttonSources";
            this.buttonSources.Size = new System.Drawing.Size(164, 40);
            this.buttonSources.TabIndex = 1;
            this.buttonSources.Text = "Отобразить все источники";
            this.buttonSources.UseVisualStyleBackColor = true;
            this.buttonSources.Click += new System.EventHandler(this.buttonSources_Click);
            // 
            // buttonOwners
            // 
            this.buttonOwners.Location = new System.Drawing.Point(6, 22);
            this.buttonOwners.Name = "buttonOwners";
            this.buttonOwners.Size = new System.Drawing.Size(164, 40);
            this.buttonOwners.TabIndex = 0;
            this.buttonOwners.Text = "Отобразить всех владельцев";
            this.buttonOwners.UseVisualStyleBackColor = true;
            this.buttonOwners.Click += new System.EventHandler(this.buttonOwners_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBoxAnalyst);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(194, 350);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Аналитика";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBoxAnalyst
            // 
            this.groupBoxAnalyst.Controls.Add(this.label7);
            this.groupBoxAnalyst.Controls.Add(this.textBoxMinutes2);
            this.groupBoxAnalyst.Controls.Add(this.button2);
            this.groupBoxAnalyst.Controls.Add(this.buttonr);
            this.groupBoxAnalyst.Enabled = false;
            this.groupBoxAnalyst.Location = new System.Drawing.Point(8, 3);
            this.groupBoxAnalyst.Name = "groupBoxAnalyst";
            this.groupBoxAnalyst.Size = new System.Drawing.Size(179, 344);
            this.groupBoxAnalyst.TabIndex = 3;
            this.groupBoxAnalyst.TabStop = false;
            this.groupBoxAnalyst.Text = "Панель управления";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "Минуты";
            // 
            // textBoxMinutes2
            // 
            this.textBoxMinutes2.Location = new System.Drawing.Point(71, 22);
            this.textBoxMinutes2.Name = "textBoxMinutes2";
            this.textBoxMinutes2.Size = new System.Drawing.Size(93, 23);
            this.textBoxMinutes2.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 295);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(158, 40);
            this.button2.TabIndex = 4;
            this.button2.Text = "Отобразить трафик за последние n минут";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonr
            // 
            this.buttonr.Location = new System.Drawing.Point(4, 65);
            this.buttonr.Name = "buttonr";
            this.buttonr.Size = new System.Drawing.Size(164, 40);
            this.buttonr.TabIndex = 0;
            this.buttonr.Text = "Отобразить сумму трафика";
            this.buttonr.UseVisualStyleBackColor = true;
            this.buttonr.Click += new System.EventHandler(this.buttonr_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBoxAdmin);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(194, 350);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Админ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonLogout
            // 
            this.buttonLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonLogout.Location = new System.Drawing.Point(807, 403);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonLogout.Size = new System.Drawing.Size(19, 37);
            this.buttonLogout.TabIndex = 7;
            this.buttonLogout.UseVisualStyleBackColor = false;
            this.buttonLogout.Click += new System.EventHandler(this.logout_Click);
            // 
            // LandingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 450);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labelCurrentRole);
            this.Controls.Add(this.labelCurrentLogin);
            this.Controls.Add(this.mainDataGrid);
            this.Name = "LandingForm";
            this.Text = "Рабочее окно";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LandingForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGrid)).EndInit();
            this.groupBoxAdmin.ResumeLayout(false);
            this.groupBoxAdmin.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBoxGuest.ResumeLayout(false);
            this.groupBoxGuest.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBoxAnalyst.ResumeLayout(false);
            this.groupBoxAnalyst.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBoxAdmin;
        private System.Windows.Forms.TextBox textBoxAdminLogin;
        private System.Windows.Forms.TextBox textBoxAdminPass;
        private System.Windows.Forms.Button buttonAdminCreateUser;
        private System.Windows.Forms.Button buttonAdminGetUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLoginDelete;
        private System.Windows.Forms.Button buttonDeleteUser;
        private System.Windows.Forms.Button buttomGiveRole;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRoleLogin;
        private System.Windows.Forms.Button buttonTakeRole;
        private System.Windows.Forms.ListBox listBoxRole;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelCurrentLogin;
        private System.Windows.Forms.Label labelCurrentRole;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView mainDataGrid;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBoxGuest;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxMinutes;
        private System.Windows.Forms.Button buttonTrafficMinutes;
        private System.Windows.Forms.Button buttonTypesSource;
        private System.Windows.Forms.Button buttonDestinations;
        private System.Windows.Forms.Button buttonSources;
        private System.Windows.Forms.Button buttonOwners;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonTypeDest;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.GroupBox groupBoxAnalyst;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxMinutes2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonr;
    }
}