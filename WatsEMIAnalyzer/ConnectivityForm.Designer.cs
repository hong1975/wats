namespace WatsEMIAnalyzer
{
    partial class ConnectivityForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectivityForm));
            this.label1 = new System.Windows.Forms.Label();
            this.ServerHostEditor = new System.Windows.Forms.TextBox();
            this.HttpProxyCheckBox = new System.Windows.Forms.CheckBox();
            this.HttpProxyEditor = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server host:";
            // 
            // ServerHostEditor
            // 
            this.ServerHostEditor.Location = new System.Drawing.Point(108, 13);
            this.ServerHostEditor.Name = "ServerHostEditor";
            this.ServerHostEditor.Size = new System.Drawing.Size(185, 20);
            this.ServerHostEditor.TabIndex = 1;
            // 
            // HttpProxyCheckBox
            // 
            this.HttpProxyCheckBox.AutoSize = true;
            this.HttpProxyCheckBox.Location = new System.Drawing.Point(16, 41);
            this.HttpProxyCheckBox.Name = "HttpProxyCheckBox";
            this.HttpProxyCheckBox.Size = new System.Drawing.Size(86, 17);
            this.HttpProxyCheckBox.TabIndex = 4;
            this.HttpProxyCheckBox.Text = "HTTP proxy:";
            this.HttpProxyCheckBox.UseVisualStyleBackColor = true;
            this.HttpProxyCheckBox.CheckedChanged += new System.EventHandler(this.HttpProxyCheckBox_CheckedChanged);
            // 
            // HttpProxyEditor
            // 
            this.HttpProxyEditor.Location = new System.Drawing.Point(108, 39);
            this.HttpProxyEditor.Name = "HttpProxyEditor";
            this.HttpProxyEditor.Size = new System.Drawing.Size(185, 20);
            this.HttpProxyEditor.TabIndex = 5;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(137, 65);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 33);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(218, 65);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 33);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // ConnectivityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 109);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.HttpProxyEditor);
            this.Controls.Add(this.HttpProxyCheckBox);
            this.Controls.Add(this.ServerHostEditor);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConnectivityForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connectivity";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectivityForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ServerHostEditor;
        private System.Windows.Forms.CheckBox HttpProxyCheckBox;
        private System.Windows.Forms.TextBox HttpProxyEditor;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
    }
}