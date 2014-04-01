namespace WatsEMIAnalyzer
{
    partial class LogInForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogInForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UserNameEditor = new System.Windows.Forms.TextBox();
            this.PasswordEditor = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LogInButton = new System.Windows.Forms.Button();
            this.ConnectivityButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // UserNameEditor
            // 
            this.UserNameEditor.Location = new System.Drawing.Point(86, 28);
            this.UserNameEditor.Name = "UserNameEditor";
            this.UserNameEditor.Size = new System.Drawing.Size(135, 20);
            this.UserNameEditor.TabIndex = 2;
            // 
            // PasswordEditor
            // 
            this.PasswordEditor.Location = new System.Drawing.Point(86, 59);
            this.PasswordEditor.Name = "PasswordEditor";
            this.PasswordEditor.PasswordChar = '*';
            this.PasswordEditor.Size = new System.Drawing.Size(135, 20);
            this.PasswordEditor.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.PasswordEditor);
            this.groupBox1.Controls.Add(this.UserNameEditor);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 103);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // LogInButton
            // 
            this.LogInButton.Location = new System.Drawing.Point(12, 111);
            this.LogInButton.Name = "LogInButton";
            this.LogInButton.Size = new System.Drawing.Size(65, 27);
            this.LogInButton.TabIndex = 8;
            this.LogInButton.Text = "Log &In";
            this.LogInButton.UseVisualStyleBackColor = true;
            this.LogInButton.Click += new System.EventHandler(this.LogInButton_Click);
            // 
            // ConnectivityButton
            // 
            this.ConnectivityButton.Location = new System.Drawing.Point(154, 112);
            this.ConnectivityButton.Name = "ConnectivityButton";
            this.ConnectivityButton.Size = new System.Drawing.Size(98, 27);
            this.ConnectivityButton.TabIndex = 9;
            this.ConnectivityButton.Text = "Connectivity ...";
            this.ConnectivityButton.UseVisualStyleBackColor = true;
            this.ConnectivityButton.Visible = false;
            this.ConnectivityButton.Click += new System.EventHandler(this.ConnectivityButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(83, 111);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(65, 27);
            this.CancelButton.TabIndex = 10;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // LogInForm
            // 
            this.AcceptButton = this.LogInButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 151);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ConnectivityButton);
            this.Controls.Add(this.LogInButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LogInForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WATS EMI Analyzer 2.0";
            this.Load += new System.EventHandler(this.LogInForm_Load);
            this.VisibleChanged += new System.EventHandler(this.LogInForm_VisibleChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LogInForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UserNameEditor;
        private System.Windows.Forms.TextBox PasswordEditor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button LogInButton;
        private System.Windows.Forms.Button ConnectivityButton;
        private System.Windows.Forms.Button CancelButton;
    }
}

