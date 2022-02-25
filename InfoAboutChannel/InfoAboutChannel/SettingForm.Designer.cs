namespace InfoAboutChannel
{
    partial class SettingForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelSettingTimerUpdate = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxOriginalTimeMinute = new System.Windows.Forms.ComboBox();
            this.comboBoxOriginalTime = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UpdateListOfProgrammButton = new System.Windows.Forms.Button();
            this.ProgrammsGridForUpdate = new System.Windows.Forms.DataGridView();
            this.UnicalCounterProgramm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Channel_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateOfUpdateChannel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateOfUpdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.HourBoxForUpdate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MinuteBoxForUpdate = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.panelSettingTimerUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgrammsGridForUpdate)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(317, 307);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(90, 29);
            this.okButton.TabIndex = 25;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(413, 307);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 29);
            this.cancelButton.TabIndex = 24;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.panelSettingTimerUpdate);
            this.groupBox2.Controls.Add(this.UpdateListOfProgrammButton);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(9, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox2.Size = new System.Drawing.Size(492, 62);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "  Параметры обновления списка телепередач ";
            // 
            // panelSettingTimerUpdate
            // 
            this.panelSettingTimerUpdate.Controls.Add(this.label4);
            this.panelSettingTimerUpdate.Controls.Add(this.label3);
            this.panelSettingTimerUpdate.Controls.Add(this.comboBoxOriginalTimeMinute);
            this.panelSettingTimerUpdate.Controls.Add(this.comboBoxOriginalTime);
            this.panelSettingTimerUpdate.Controls.Add(this.label2);
            this.panelSettingTimerUpdate.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSettingTimerUpdate.Location = new System.Drawing.Point(0, 13);
            this.panelSettingTimerUpdate.Margin = new System.Windows.Forms.Padding(0);
            this.panelSettingTimerUpdate.Name = "panelSettingTimerUpdate";
            this.panelSettingTimerUpdate.Size = new System.Drawing.Size(330, 49);
            this.panelSettingTimerUpdate.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "минут";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "часов";
            // 
            // comboBoxOriginalTimeMinute
            // 
            this.comboBoxOriginalTimeMinute.FormattingEnabled = true;
            this.comboBoxOriginalTimeMinute.Items.AddRange(new object[] {
            "0",
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "55"});
            this.comboBoxOriginalTimeMinute.Location = new System.Drawing.Point(228, 11);
            this.comboBoxOriginalTimeMinute.Name = "comboBoxOriginalTimeMinute";
            this.comboBoxOriginalTimeMinute.Size = new System.Drawing.Size(53, 21);
            this.comboBoxOriginalTimeMinute.TabIndex = 4;
            this.comboBoxOriginalTimeMinute.TextChanged += new System.EventHandler(this.comboBoxOriginalTime_TextChanged);
            // 
            // comboBoxOriginalTime
            // 
            this.comboBoxOriginalTime.AutoCompleteCustomSource.AddRange(new string[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboBoxOriginalTime.FormattingEnabled = true;
            this.comboBoxOriginalTime.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.comboBoxOriginalTime.Location = new System.Drawing.Point(119, 11);
            this.comboBoxOriginalTime.Name = "comboBoxOriginalTime";
            this.comboBoxOriginalTime.Size = new System.Drawing.Size(53, 21);
            this.comboBoxOriginalTime.TabIndex = 5;
            this.comboBoxOriginalTime.TextChanged += new System.EventHandler(this.comboBoxOriginalTime_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Период обновления:";
            // 
            // UpdateListOfProgrammButton
            // 
            this.UpdateListOfProgrammButton.Location = new System.Drawing.Point(339, 19);
            this.UpdateListOfProgrammButton.Name = "UpdateListOfProgrammButton";
            this.UpdateListOfProgrammButton.Size = new System.Drawing.Size(146, 29);
            this.UpdateListOfProgrammButton.TabIndex = 25;
            this.UpdateListOfProgrammButton.Text = "Запуск обновления";
            this.UpdateListOfProgrammButton.UseVisualStyleBackColor = true;
            this.UpdateListOfProgrammButton.Click += new System.EventHandler(this.UpdateListOfProgrammButton_Click);
            // 
            // ProgrammsGridForUpdate
            // 
            this.ProgrammsGridForUpdate.AllowUserToAddRows = false;
            this.ProgrammsGridForUpdate.AllowUserToDeleteRows = false;
            this.ProgrammsGridForUpdate.AllowUserToResizeColumns = false;
            this.ProgrammsGridForUpdate.AllowUserToResizeRows = false;
            this.ProgrammsGridForUpdate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProgrammsGridForUpdate.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.ProgrammsGridForUpdate.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.ProgrammsGridForUpdate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProgrammsGridForUpdate.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.ProgrammsGridForUpdate.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProgrammsGridForUpdate.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ProgrammsGridForUpdate.ColumnHeadersHeight = 30;
            this.ProgrammsGridForUpdate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ProgrammsGridForUpdate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UnicalCounterProgramm,
            this.Channel_Name,
            this.DateOfUpdateChannel,
            this.StateOfUpdate});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.Format = "D";
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProgrammsGridForUpdate.DefaultCellStyle = dataGridViewCellStyle5;
            this.ProgrammsGridForUpdate.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ProgrammsGridForUpdate.Location = new System.Drawing.Point(6, 19);
            this.ProgrammsGridForUpdate.MultiSelect = false;
            this.ProgrammsGridForUpdate.Name = "ProgrammsGridForUpdate";
            this.ProgrammsGridForUpdate.ReadOnly = true;
            this.ProgrammsGridForUpdate.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.ProgrammsGridForUpdate.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "D";
            dataGridViewCellStyle6.NullValue = null;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProgrammsGridForUpdate.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.ProgrammsGridForUpdate.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ProgrammsGridForUpdate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProgrammsGridForUpdate.ShowEditingIcon = false;
            this.ProgrammsGridForUpdate.Size = new System.Drawing.Size(483, 200);
            this.ProgrammsGridForUpdate.TabIndex = 27;
            this.ProgrammsGridForUpdate.TabStop = false;
            // 
            // UnicalCounterProgramm
            // 
            this.UnicalCounterProgramm.HeaderText = "UnicalCounterProgramm";
            this.UnicalCounterProgramm.Name = "UnicalCounterProgramm";
            this.UnicalCounterProgramm.ReadOnly = true;
            this.UnicalCounterProgramm.ToolTipText = "UnicalCounterProgramm";
            this.UnicalCounterProgramm.Visible = false;
            // 
            // Channel_Name
            // 
            this.Channel_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Channel_Name.DefaultCellStyle = dataGridViewCellStyle2;
            this.Channel_Name.FillWeight = 80F;
            this.Channel_Name.HeaderText = "Название канала";
            this.Channel_Name.MinimumWidth = 150;
            this.Channel_Name.Name = "Channel_Name";
            this.Channel_Name.ReadOnly = true;
            this.Channel_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Channel_Name.ToolTipText = "Название канала";
            // 
            // DateOfUpdateChannel
            // 
            this.DateOfUpdateChannel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateOfUpdateChannel.DefaultCellStyle = dataGridViewCellStyle3;
            this.DateOfUpdateChannel.HeaderText = "Дата загрузки программ";
            this.DateOfUpdateChannel.Name = "DateOfUpdateChannel";
            this.DateOfUpdateChannel.ReadOnly = true;
            this.DateOfUpdateChannel.Width = 180;
            // 
            // StateOfUpdate
            // 
            this.StateOfUpdate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.StateOfUpdate.DefaultCellStyle = dataGridViewCellStyle4;
            this.StateOfUpdate.HeaderText = "Состояние";
            this.StateOfUpdate.Name = "StateOfUpdate";
            this.StateOfUpdate.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ProgrammsGridForUpdate);
            this.groupBox1.Location = new System.Drawing.Point(9, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(494, 225);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Список каналов для обновления";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 315);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "До следующего обновления:";
            // 
            // HourBoxForUpdate
            // 
            this.HourBoxForUpdate.Location = new System.Drawing.Point(162, 312);
            this.HourBoxForUpdate.Name = "HourBoxForUpdate";
            this.HourBoxForUpdate.ReadOnly = true;
            this.HourBoxForUpdate.Size = new System.Drawing.Size(29, 20);
            this.HourBoxForUpdate.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(197, 315);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "часов";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(276, 315);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "минут";
            // 
            // MinuteBoxForUpdate
            // 
            this.MinuteBoxForUpdate.Location = new System.Drawing.Point(237, 312);
            this.MinuteBoxForUpdate.Name = "MinuteBoxForUpdate";
            this.MinuteBoxForUpdate.ReadOnly = true;
            this.MinuteBoxForUpdate.Size = new System.Drawing.Size(37, 20);
            this.MinuteBoxForUpdate.TabIndex = 29;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(510, 347);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MinuteBoxForUpdate);
            this.Controls.Add(this.HourBoxForUpdate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(516, 375);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(516, 375);
            this.Name = "SettingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки обновления списка программ";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.SettingForm_Shown);
            this.groupBox2.ResumeLayout(false);
            this.panelSettingTimerUpdate.ResumeLayout(false);
            this.panelSettingTimerUpdate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgrammsGridForUpdate)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView ProgrammsGridForUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelSettingTimerUpdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxOriginalTimeMinute;
        private System.Windows.Forms.ComboBox comboBoxOriginalTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnicalCounterProgramm;
        private System.Windows.Forms.DataGridViewTextBoxColumn Channel_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateOfUpdateChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateOfUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox HourBoxForUpdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox MinuteBoxForUpdate;
        public System.Windows.Forms.Button UpdateListOfProgrammButton;
    }
}