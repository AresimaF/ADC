namespace ADC.Screens.LoginScreen
{
    partial class LoginScreen
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
            this.label1 = new System.Windows.Forms.Label();
            this.textUsername = new System.Windows.Forms.TextBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonLogIn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // textUsername
            // 
            this.textUsername.Location = new System.Drawing.Point(23, 215);
            this.textUsername.MaximumSize = new System.Drawing.Size(169, 20);
            this.textUsername.MinimumSize = new System.Drawing.Size(169, 20);
            this.textUsername.Name = "textUsername";
            this.textUsername.Size = new System.Drawing.Size(169, 20);
            this.textUsername.TabIndex = 1;
            this.textUsername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textUsername_KeyPress);
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(23, 275);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(169, 20);
            this.textPassword.TabIndex = 2;
            this.textPassword.UseSystemPasswordChar = true;
            this.textPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textPassword_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // buttonLogIn
            // 
            this.buttonLogIn.Location = new System.Drawing.Point(229, 215);
            this.buttonLogIn.Name = "buttonLogIn";
            this.buttonLogIn.Size = new System.Drawing.Size(108, 80);
            this.buttonLogIn.TabIndex = 3;
            this.buttonLogIn.Text = "Log In";
            this.buttonLogIn.UseVisualStyleBackColor = true;
            this.buttonLogIn.Click += new System.EventHandler(this.buttonLogIn_Click);
            // 
            // LoginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 321);
            this.Controls.Add(this.buttonLogIn);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textUsername);
            this.Controls.Add(this.label1);
            this.Name = "LoginScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginScreen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textUsername;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonLogIn;
    }
}