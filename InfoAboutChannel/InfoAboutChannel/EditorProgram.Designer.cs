namespace InfoAboutChannel
{
    partial class EditorProgram
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorProgram));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TimeOfProgrammTextBox = new System.Windows.Forms.MaskedTextBox();
            this.DateOfProgramm = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.Programm_Description_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.GerneComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.openDialogLogoGetButton = new System.Windows.Forms.Button();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.nameProgrammTextBox = new System.Windows.Forms.TextBox();
            this.LogoProgrammTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.Label();
            this.DirectorProgrammTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TimeOfProgrammTextBox);
            this.groupBox1.Controls.Add(this.DateOfProgramm);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.Programm_Description_RichTextBox);
            this.groupBox1.Controls.Add(this.GerneComboBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.openDialogLogoGetButton);
            this.groupBox1.Controls.Add(this.LogoPictureBox);
            this.groupBox1.Controls.Add(this.nameProgrammTextBox);
            this.groupBox1.Controls.Add(this.LogoProgrammTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Logo);
            this.groupBox1.Controls.Add(this.DirectorProgrammTextBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(8, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 198);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация о телепередаче";
            // 
            // TimeOfProgrammTextBox
            // 
            this.TimeOfProgrammTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeOfProgrammTextBox.BeepOnError = true;
            this.TimeOfProgrammTextBox.Location = new System.Drawing.Point(322, 80);
            this.TimeOfProgrammTextBox.Mask = "00:00";
            this.TimeOfProgrammTextBox.Name = "TimeOfProgrammTextBox";
            this.TimeOfProgrammTextBox.Size = new System.Drawing.Size(51, 20);
            this.TimeOfProgrammTextBox.TabIndex = 34;
            this.TimeOfProgrammTextBox.Text = "0000";
            this.TimeOfProgrammTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TimeOfProgrammTextBox.ValidatingType = typeof(System.DateTime);
            // 
            // DateOfProgramm
            // 
            this.DateOfProgramm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DateOfProgramm.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.DateOfProgramm.Location = new System.Drawing.Point(85, 79);
            this.DateOfProgramm.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.DateOfProgramm.Name = "DateOfProgramm";
            this.DateOfProgramm.Size = new System.Drawing.Size(181, 20);
            this.DateOfProgramm.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Время:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(45, 82);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(37, 13);
            this.label24.TabIndex = 32;
            this.label24.Text = "Эфир:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Programm_Description_RichTextBox
            // 
            this.Programm_Description_RichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Programm_Description_RichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.Programm_Description_RichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Programm_Description_RichTextBox.Location = new System.Drawing.Point(85, 135);
            this.Programm_Description_RichTextBox.Name = "Programm_Description_RichTextBox";
            this.Programm_Description_RichTextBox.Size = new System.Drawing.Size(289, 50);
            this.Programm_Description_RichTextBox.TabIndex = 27;
            this.Programm_Description_RichTextBox.Text = "";
            // 
            // GerneComboBox
            // 
            this.GerneComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GerneComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GerneComboBox.FormattingEnabled = true;
            this.GerneComboBox.Items.AddRange(new object[] {
            "Новости",
            "Шоу и программы",
            "Художественные фильмы",
            "Комедии",
            "Сериалы",
            "Информационные",
            "Аналитические"});
            this.GerneComboBox.Location = new System.Drawing.Point(254, 22);
            this.GerneComboBox.Name = "GerneComboBox";
            this.GerneComboBox.Size = new System.Drawing.Size(120, 21);
            this.GerneComboBox.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(209, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Жанр:";
            // 
            // openDialogLogoGetButton
            // 
            this.openDialogLogoGetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openDialogLogoGetButton.Location = new System.Drawing.Point(348, 105);
            this.openDialogLogoGetButton.Name = "openDialogLogoGetButton";
            this.openDialogLogoGetButton.Size = new System.Drawing.Size(26, 22);
            this.openDialogLogoGetButton.TabIndex = 22;
            this.openDialogLogoGetButton.Text = "...";
            this.openDialogLogoGetButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.openDialogLogoGetButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.openDialogLogoGetButton.UseVisualStyleBackColor = true;
            this.openDialogLogoGetButton.Click += new System.EventHandler(this.openDialogLogoGetButton_Click);
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LogoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("LogoPictureBox.Image")));
            this.LogoPictureBox.Location = new System.Drawing.Point(387, 24);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(127, 154);
            this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoPictureBox.TabIndex = 21;
            this.LogoPictureBox.TabStop = false;
            // 
            // nameProgrammTextBox
            // 
            this.nameProgrammTextBox.Location = new System.Drawing.Point(85, 23);
            this.nameProgrammTextBox.Name = "nameProgrammTextBox";
            this.nameProgrammTextBox.Size = new System.Drawing.Size(108, 20);
            this.nameProgrammTextBox.TabIndex = 14;
            // 
            // LogoProgrammTextBox
            // 
            this.LogoProgrammTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LogoProgrammTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogoProgrammTextBox.Location = new System.Drawing.Point(85, 106);
            this.LogoProgrammTextBox.Name = "LogoProgrammTextBox";
            this.LogoProgrammTextBox.ReadOnly = true;
            this.LogoProgrammTextBox.Size = new System.Drawing.Size(259, 20);
            this.LogoProgrammTextBox.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Описание:";
            // 
            // Logo
            // 
            this.Logo.AutoSize = true;
            this.Logo.Location = new System.Drawing.Point(30, 109);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(52, 13);
            this.Logo.TabIndex = 6;
            this.Logo.Text = "Логотип:";
            // 
            // DirectorProgrammTextBox
            // 
            this.DirectorProgrammTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectorProgrammTextBox.Location = new System.Drawing.Point(85, 51);
            this.DirectorProgrammTextBox.Name = "DirectorProgrammTextBox";
            this.DirectorProgrammTextBox.Size = new System.Drawing.Size(289, 20);
            this.DirectorProgrammTextBox.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Название:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(11, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 32);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ведущий\r\n(Режиссер)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(346, 216);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(90, 29);
            this.okButton.TabIndex = 23;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(442, 216);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 29);
            this.cancelButton.TabIndex = 22;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // EditorProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 257);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(555, 295);
            this.Name = "EditorProgram";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button openDialogLogoGetButton;
        private System.Windows.Forms.PictureBox LogoPictureBox;
        private System.Windows.Forms.TextBox LogoProgrammTextBox;
        private System.Windows.Forms.Label Logo;
        private System.Windows.Forms.TextBox DirectorProgrammTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox GerneComboBox;
        private System.Windows.Forms.RichTextBox Programm_Description_RichTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox TimeOfProgrammTextBox;
        private System.Windows.Forms.DateTimePicker DateOfProgramm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox nameProgrammTextBox;
    }
}