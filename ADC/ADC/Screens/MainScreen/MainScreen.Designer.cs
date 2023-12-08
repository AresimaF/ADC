namespace ADC
{
    partial class MainScreen
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripManagementUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripManagementRoles = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripManagementModules = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripModules = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userToolStripMenuItem,
            this.menuStripManagement,
            this.menuStripModules});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(822, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuMain";
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePasswordToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.userToolStripMenuItem.Text = "User";
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.logOutToolStripMenuItem.Text = "Log Out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // menuStripManagement
            // 
            this.menuStripManagement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripManagementUsers,
            this.menuStripManagementRoles,
            this.menuStripManagementModules});
            this.menuStripManagement.Name = "menuStripManagement";
            this.menuStripManagement.Size = new System.Drawing.Size(90, 20);
            this.menuStripManagement.Text = "Management";
            // 
            // menuStripManagementUsers
            // 
            this.menuStripManagementUsers.Name = "menuStripManagementUsers";
            this.menuStripManagementUsers.Size = new System.Drawing.Size(180, 22);
            this.menuStripManagementUsers.Text = "Users";
            this.menuStripManagementUsers.Click += new System.EventHandler(this.menuStripManagementUsers_Click);
            // 
            // menuStripManagementRoles
            // 
            this.menuStripManagementRoles.Name = "menuStripManagementRoles";
            this.menuStripManagementRoles.Size = new System.Drawing.Size(180, 22);
            this.menuStripManagementRoles.Text = "Roles";
            this.menuStripManagementRoles.Click += new System.EventHandler(this.menuStripManagementRoles_Click);
            // 
            // menuStripManagementModules
            // 
            this.menuStripManagementModules.Name = "menuStripManagementModules";
            this.menuStripManagementModules.Size = new System.Drawing.Size(180, 22);
            this.menuStripManagementModules.Text = "Modules";
            // 
            // menuStripModules
            // 
            this.menuStripModules.Name = "menuStripModules";
            this.menuStripModules.Size = new System.Drawing.Size(65, 20);
            this.menuStripModules.Text = "Modules";
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 462);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainScreen";
            this.Text = "ADC - Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuStripManagement;
        private System.Windows.Forms.ToolStripMenuItem menuStripManagementUsers;
        private System.Windows.Forms.ToolStripMenuItem menuStripManagementRoles;
        private System.Windows.Forms.ToolStripMenuItem menuStripManagementModules;
        private System.Windows.Forms.ToolStripMenuItem menuStripModules;
    }
}

