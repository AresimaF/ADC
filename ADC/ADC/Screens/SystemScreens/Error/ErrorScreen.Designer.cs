namespace ADC.Screens.Error
{
    partial class ErrorScreen
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
            this.buttonContinue = new System.Windows.Forms.Button();
            this.buttonSendReport = new System.Windows.Forms.Button();
            this.textErrorMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonContinue
            // 
            this.buttonContinue.Location = new System.Drawing.Point(277, 210);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(90, 39);
            this.buttonContinue.TabIndex = 0;
            this.buttonContinue.Text = "Continue";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonContinue_MouseClick);
            // 
            // buttonSendReport
            // 
            this.buttonSendReport.Enabled = false;
            this.buttonSendReport.Location = new System.Drawing.Point(382, 210);
            this.buttonSendReport.Name = "buttonSendReport";
            this.buttonSendReport.Size = new System.Drawing.Size(90, 39);
            this.buttonSendReport.TabIndex = 1;
            this.buttonSendReport.Text = "Send Report";
            this.buttonSendReport.UseVisualStyleBackColor = true;
            // 
            // textErrorMessage
            // 
            this.textErrorMessage.Location = new System.Drawing.Point(44, 27);
            this.textErrorMessage.Multiline = true;
            this.textErrorMessage.Name = "textErrorMessage";
            this.textErrorMessage.ReadOnly = true;
            this.textErrorMessage.Size = new System.Drawing.Size(395, 162);
            this.textErrorMessage.TabIndex = 2;
            // 
            // ErrorScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.ControlBox = false;
            this.Controls.Add(this.textErrorMessage);
            this.Controls.Add(this.buttonSendReport);
            this.Controls.Add(this.buttonContinue);
            this.Name = "ErrorScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Error";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Button buttonSendReport;
        private System.Windows.Forms.TextBox textErrorMessage;
    }
}