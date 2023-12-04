namespace ADC.Screens.NewPasswordScreen
{
    partial class NewPasswordScreen
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
            this.buttonShowHideOldPassword = new System.Windows.Forms.Button();
            this.textOldPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonShowHideNewPassword = new System.Windows.Forms.Button();
            this.textNewPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textRepeatNewPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonShowHideOldPassword
            // 
            this.buttonShowHideOldPassword.Location = new System.Drawing.Point(202, 34);
            this.buttonShowHideOldPassword.Name = "buttonShowHideOldPassword";
            this.buttonShowHideOldPassword.Size = new System.Drawing.Size(23, 19);
            this.buttonShowHideOldPassword.TabIndex = 9;
            this.buttonShowHideOldPassword.Text = "👁";
            this.buttonShowHideOldPassword.UseVisualStyleBackColor = true;
            this.buttonShowHideOldPassword.Click += new System.EventHandler(this.buttonShowHideOldPassword_Click);
            // 
            // textOldPassword
            // 
            this.textOldPassword.Location = new System.Drawing.Point(23, 34);
            this.textOldPassword.Name = "textOldPassword";
            this.textOldPassword.Size = new System.Drawing.Size(172, 20);
            this.textOldPassword.TabIndex = 0;
            this.textOldPassword.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Old Password";
            // 
            // buttonShowHideNewPassword
            // 
            this.buttonShowHideNewPassword.Location = new System.Drawing.Point(202, 109);
            this.buttonShowHideNewPassword.Name = "buttonShowHideNewPassword";
            this.buttonShowHideNewPassword.Size = new System.Drawing.Size(23, 19);
            this.buttonShowHideNewPassword.TabIndex = 12;
            this.buttonShowHideNewPassword.Text = "👁";
            this.buttonShowHideNewPassword.UseVisualStyleBackColor = true;
            this.buttonShowHideNewPassword.Click += new System.EventHandler(this.buttonShowHideNewPassword_Click);
            // 
            // textNewPassword
            // 
            this.textNewPassword.Location = new System.Drawing.Point(23, 109);
            this.textNewPassword.Name = "textNewPassword";
            this.textNewPassword.Size = new System.Drawing.Size(172, 20);
            this.textNewPassword.TabIndex = 1;
            this.textNewPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "New Password";
            // 
            // textRepeatNewPassword
            // 
            this.textRepeatNewPassword.Location = new System.Drawing.Point(23, 161);
            this.textRepeatNewPassword.Name = "textRepeatNewPassword";
            this.textRepeatNewPassword.Size = new System.Drawing.Size(172, 20);
            this.textRepeatNewPassword.TabIndex = 2;
            this.textRepeatNewPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Repeat New Password";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(80, 211);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(86, 42);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // NewPasswordScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 288);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textRepeatNewPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonShowHideNewPassword);
            this.Controls.Add(this.textNewPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonShowHideOldPassword);
            this.Controls.Add(this.textOldPassword);
            this.Controls.Add(this.label3);
            this.MaximumSize = new System.Drawing.Size(264, 327);
            this.MinimumSize = new System.Drawing.Size(264, 327);
            this.Name = "NewPasswordScreen";
            this.Text = "NewPasswordScreen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonShowHideOldPassword;
        private System.Windows.Forms.TextBox textOldPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonShowHideNewPassword;
        private System.Windows.Forms.TextBox textNewPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textRepeatNewPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSave;
    }
}