namespace WatsEMIAnalyzer
{
    partial class NewTaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewTaskForm));
            this.label4 = new System.Windows.Forms.Label();
            this.TaskDescriptionEditor = new System.Windows.Forms.RichTextBox();
            this.TaskNameEditor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TaskAnalyzerList = new System.Windows.Forms.ListBox();
            this.RemoveSiteButton = new System.Windows.Forms.Button();
            this.AddSiteButton = new System.Windows.Forms.Button();
            this.TaskSiteList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ViewSiteButton = new System.Windows.Forms.Button();
            this.ViewAnalyzerButton = new System.Windows.Forms.Button();
            this.RemoveAnalyzerButton = new System.Windows.Forms.Button();
            this.AddAnalyzerButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.ChannelSettingEditor = new System.Windows.Forms.TextBox();
            this.ChannelSettingDetailButton = new System.Windows.Forms.Button();
            this.LinkConfigrationDetailButton = new System.Windows.Forms.Button();
            this.LinkConfigurationEditor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.EquipmentParameterDetailButton = new System.Windows.Forms.Button();
            this.EquipmentParameterEditor = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "Comment";
            // 
            // TaskDescriptionEditor
            // 
            this.TaskDescriptionEditor.Location = new System.Drawing.Point(13, 63);
            this.TaskDescriptionEditor.MaxLength = 2048;
            this.TaskDescriptionEditor.Name = "TaskDescriptionEditor";
            this.TaskDescriptionEditor.Size = new System.Drawing.Size(331, 61);
            this.TaskDescriptionEditor.TabIndex = 3;
            this.TaskDescriptionEditor.Text = "";
            this.TaskDescriptionEditor.Enter += new System.EventHandler(this.TaskDescriptionEditor_Enter);
            this.TaskDescriptionEditor.Leave += new System.EventHandler(this.TaskDescriptionEditor_Leave);
            // 
            // TaskNameEditor
            // 
            this.TaskNameEditor.Location = new System.Drawing.Point(55, 16);
            this.TaskNameEditor.Name = "TaskNameEditor";
            this.TaskNameEditor.Size = new System.Drawing.Size(289, 22);
            this.TaskNameEditor.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 395);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 14);
            this.label3.TabIndex = 9;
            this.label3.Text = "Task Analyzers";
            // 
            // TaskAnalyzerList
            // 
            this.TaskAnalyzerList.FormattingEnabled = true;
            this.TaskAnalyzerList.HorizontalScrollbar = true;
            this.TaskAnalyzerList.ItemHeight = 14;
            this.TaskAnalyzerList.Location = new System.Drawing.Point(12, 412);
            this.TaskAnalyzerList.Name = "TaskAnalyzerList";
            this.TaskAnalyzerList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.TaskAnalyzerList.Size = new System.Drawing.Size(258, 88);
            this.TaskAnalyzerList.TabIndex = 10;
            this.TaskAnalyzerList.SelectedIndexChanged += new System.EventHandler(this.AnalyzerList_SelectedIndexChanged);
            // 
            // RemoveSiteButton
            // 
            this.RemoveSiteButton.Enabled = false;
            this.RemoveSiteButton.Location = new System.Drawing.Point(276, 321);
            this.RemoveSiteButton.Name = "RemoveSiteButton";
            this.RemoveSiteButton.Size = new System.Drawing.Size(67, 23);
            this.RemoveSiteButton.TabIndex = 7;
            this.RemoveSiteButton.Text = "Remove";
            this.RemoveSiteButton.UseVisualStyleBackColor = true;
            this.RemoveSiteButton.Click += new System.EventHandler(this.RemoveSiteButton_Click);
            // 
            // AddSiteButton
            // 
            this.AddSiteButton.Location = new System.Drawing.Point(276, 294);
            this.AddSiteButton.Name = "AddSiteButton";
            this.AddSiteButton.Size = new System.Drawing.Size(67, 23);
            this.AddSiteButton.TabIndex = 6;
            this.AddSiteButton.Text = "Add ...";
            this.AddSiteButton.UseVisualStyleBackColor = true;
            this.AddSiteButton.Click += new System.EventHandler(this.AddSiteButton_Click);
            // 
            // TaskSiteList
            // 
            this.TaskSiteList.FormattingEnabled = true;
            this.TaskSiteList.HorizontalScrollbar = true;
            this.TaskSiteList.ItemHeight = 14;
            this.TaskSiteList.Location = new System.Drawing.Point(12, 294);
            this.TaskSiteList.Name = "TaskSiteList";
            this.TaskSiteList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.TaskSiteList.Size = new System.Drawing.Size(258, 88);
            this.TaskSiteList.TabIndex = 5;
            this.TaskSiteList.SelectedIndexChanged += new System.EventHandler(this.TaskSiteList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Task Sites";
            // 
            // ViewSiteButton
            // 
            this.ViewSiteButton.Enabled = false;
            this.ViewSiteButton.Location = new System.Drawing.Point(276, 348);
            this.ViewSiteButton.Name = "ViewSiteButton";
            this.ViewSiteButton.Size = new System.Drawing.Size(67, 23);
            this.ViewSiteButton.TabIndex = 8;
            this.ViewSiteButton.Text = "View";
            this.ViewSiteButton.UseVisualStyleBackColor = true;
            this.ViewSiteButton.Click += new System.EventHandler(this.ViewSiteButton_Click);
            // 
            // ViewAnalyzerButton
            // 
            this.ViewAnalyzerButton.Enabled = false;
            this.ViewAnalyzerButton.Location = new System.Drawing.Point(276, 466);
            this.ViewAnalyzerButton.Name = "ViewAnalyzerButton";
            this.ViewAnalyzerButton.Size = new System.Drawing.Size(67, 23);
            this.ViewAnalyzerButton.TabIndex = 13;
            this.ViewAnalyzerButton.Text = "View";
            this.ViewAnalyzerButton.UseVisualStyleBackColor = true;
            this.ViewAnalyzerButton.Click += new System.EventHandler(this.ViewAnalyzerButton_Click);
            // 
            // RemoveAnalyzerButton
            // 
            this.RemoveAnalyzerButton.Enabled = false;
            this.RemoveAnalyzerButton.Location = new System.Drawing.Point(276, 439);
            this.RemoveAnalyzerButton.Name = "RemoveAnalyzerButton";
            this.RemoveAnalyzerButton.Size = new System.Drawing.Size(67, 23);
            this.RemoveAnalyzerButton.TabIndex = 12;
            this.RemoveAnalyzerButton.Text = "Remove";
            this.RemoveAnalyzerButton.UseVisualStyleBackColor = true;
            this.RemoveAnalyzerButton.Click += new System.EventHandler(this.RemoveAnalyzerButton_Click);
            // 
            // AddAnalyzerButton
            // 
            this.AddAnalyzerButton.Location = new System.Drawing.Point(276, 412);
            this.AddAnalyzerButton.Name = "AddAnalyzerButton";
            this.AddAnalyzerButton.Size = new System.Drawing.Size(67, 23);
            this.AddAnalyzerButton.TabIndex = 11;
            this.AddAnalyzerButton.Text = "Add ...";
            this.AddAnalyzerButton.UseVisualStyleBackColor = true;
            this.AddAnalyzerButton.Click += new System.EventHandler(this.AddAnalyzerButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(276, 510);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 26);
            this.OKButton.TabIndex = 14;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(195, 510);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 26);
            this.CancelButton.TabIndex = 15;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 14);
            this.label5.TabIndex = 51;
            this.label5.Text = "Channel Setting";
            // 
            // ChannelSettingEditor
            // 
            this.ChannelSettingEditor.Location = new System.Drawing.Point(13, 150);
            this.ChannelSettingEditor.Name = "ChannelSettingEditor";
            this.ChannelSettingEditor.ReadOnly = true;
            this.ChannelSettingEditor.Size = new System.Drawing.Size(257, 22);
            this.ChannelSettingEditor.TabIndex = 52;
            // 
            // ChannelSettingDetailButton
            // 
            this.ChannelSettingDetailButton.Location = new System.Drawing.Point(277, 150);
            this.ChannelSettingDetailButton.Name = "ChannelSettingDetailButton";
            this.ChannelSettingDetailButton.Size = new System.Drawing.Size(67, 23);
            this.ChannelSettingDetailButton.TabIndex = 53;
            this.ChannelSettingDetailButton.Text = "Detail";
            this.ChannelSettingDetailButton.UseVisualStyleBackColor = true;
            this.ChannelSettingDetailButton.Click += new System.EventHandler(this.ChannelSettingDetailButton_Click);
            // 
            // LinkConfigrationDetailButton
            // 
            this.LinkConfigrationDetailButton.Location = new System.Drawing.Point(276, 194);
            this.LinkConfigrationDetailButton.Name = "LinkConfigrationDetailButton";
            this.LinkConfigrationDetailButton.Size = new System.Drawing.Size(67, 23);
            this.LinkConfigrationDetailButton.TabIndex = 56;
            this.LinkConfigrationDetailButton.Text = "Detail";
            this.LinkConfigrationDetailButton.UseVisualStyleBackColor = true;
            this.LinkConfigrationDetailButton.Click += new System.EventHandler(this.LinkConfigrationDetailButton_Click);
            // 
            // LinkConfigurationEditor
            // 
            this.LinkConfigurationEditor.Location = new System.Drawing.Point(13, 195);
            this.LinkConfigurationEditor.Name = "LinkConfigurationEditor";
            this.LinkConfigurationEditor.ReadOnly = true;
            this.LinkConfigurationEditor.Size = new System.Drawing.Size(257, 22);
            this.LinkConfigurationEditor.TabIndex = 55;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 14);
            this.label6.TabIndex = 54;
            this.label6.Text = "Link Configuration";
            // 
            // EquipmentParameterDetailButton
            // 
            this.EquipmentParameterDetailButton.Location = new System.Drawing.Point(277, 242);
            this.EquipmentParameterDetailButton.Name = "EquipmentParameterDetailButton";
            this.EquipmentParameterDetailButton.Size = new System.Drawing.Size(67, 23);
            this.EquipmentParameterDetailButton.TabIndex = 59;
            this.EquipmentParameterDetailButton.Text = "Detail";
            this.EquipmentParameterDetailButton.UseVisualStyleBackColor = true;
            this.EquipmentParameterDetailButton.Click += new System.EventHandler(this.EquipmentParameterDetailButton_Click);
            // 
            // EquipmentParameterEditor
            // 
            this.EquipmentParameterEditor.Location = new System.Drawing.Point(13, 243);
            this.EquipmentParameterEditor.Name = "EquipmentParameterEditor";
            this.EquipmentParameterEditor.ReadOnly = true;
            this.EquipmentParameterEditor.Size = new System.Drawing.Size(257, 22);
            this.EquipmentParameterEditor.TabIndex = 58;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 225);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 14);
            this.label7.TabIndex = 57;
            this.label7.Text = "Equipment Parameter";
            // 
            // NewTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 547);
            this.Controls.Add(this.EquipmentParameterDetailButton);
            this.Controls.Add(this.EquipmentParameterEditor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.LinkConfigrationDetailButton);
            this.Controls.Add(this.LinkConfigurationEditor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ChannelSettingDetailButton);
            this.Controls.Add(this.ChannelSettingEditor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.ViewAnalyzerButton);
            this.Controls.Add(this.RemoveAnalyzerButton);
            this.Controls.Add(this.AddAnalyzerButton);
            this.Controls.Add(this.ViewSiteButton);
            this.Controls.Add(this.TaskSiteList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RemoveSiteButton);
            this.Controls.Add(this.AddSiteButton);
            this.Controls.Add(this.TaskAnalyzerList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TaskDescriptionEditor);
            this.Controls.Add(this.TaskNameEditor);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NewTaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Task";
            this.Load += new System.EventHandler(this.NewTaskForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewTaskForm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewTaskForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox TaskDescriptionEditor;
        private System.Windows.Forms.TextBox TaskNameEditor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox TaskAnalyzerList;
        private System.Windows.Forms.Button RemoveSiteButton;
        private System.Windows.Forms.Button AddSiteButton;
        private System.Windows.Forms.ListBox TaskSiteList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ViewSiteButton;
        private System.Windows.Forms.Button ViewAnalyzerButton;
        private System.Windows.Forms.Button RemoveAnalyzerButton;
        private System.Windows.Forms.Button AddAnalyzerButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ChannelSettingEditor;
        private System.Windows.Forms.Button ChannelSettingDetailButton;
        private System.Windows.Forms.Button LinkConfigrationDetailButton;
        private System.Windows.Forms.TextBox LinkConfigurationEditor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button EquipmentParameterDetailButton;
        private System.Windows.Forms.TextBox EquipmentParameterEditor;
        private System.Windows.Forms.Label label7;

    }
}