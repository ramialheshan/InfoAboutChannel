namespace InfoAboutChannel
{
    partial class FormShowVideoSite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShowVideoSite));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridPreviewList = new System.Windows.Forms.DataGridView();
            this.UnicalCounter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LinkPreviewFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToolsMenuChannels = new System.Windows.Forms.ToolStrip();
            this.PlayTV_ChannelButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPreviewList)).BeginInit();
            this.ToolsMenuChannels.SuspendLayout();
            this.SuspendLayout();
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(1, 209);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(638, 411);
            this.axWindowsMediaPlayer1.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Controls.Add(this.dataGridPreviewList);
            this.groupBox4.Controls.Add(this.ToolsMenuChannels);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(1, 1);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox4.Size = new System.Drawing.Size(638, 204);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "  Список видео анонсов телеканала";
            // 
            // dataGridPreviewList
            // 
            this.dataGridPreviewList.AllowUserToAddRows = false;
            this.dataGridPreviewList.AllowUserToDeleteRows = false;
            this.dataGridPreviewList.AllowUserToResizeColumns = false;
            this.dataGridPreviewList.AllowUserToResizeRows = false;
            this.dataGridPreviewList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridPreviewList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridPreviewList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridPreviewList.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridPreviewList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridPreviewList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridPreviewList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridPreviewList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridPreviewList.ColumnHeadersHeight = 30;
            this.dataGridPreviewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridPreviewList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UnicalCounter,
            this.LinkPreviewFile});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Format = "D";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridPreviewList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridPreviewList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridPreviewList.Location = new System.Drawing.Point(5, 49);
            this.dataGridPreviewList.MultiSelect = false;
            this.dataGridPreviewList.Name = "dataGridPreviewList";
            this.dataGridPreviewList.ReadOnly = true;
            this.dataGridPreviewList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridPreviewList.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "D";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridPreviewList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridPreviewList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridPreviewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridPreviewList.ShowEditingIcon = false;
            this.dataGridPreviewList.Size = new System.Drawing.Size(628, 147);
            this.dataGridPreviewList.TabIndex = 27;
            this.dataGridPreviewList.TabStop = false;
            // 
            // UnicalCounter
            // 
            this.UnicalCounter.HeaderText = "UnicalCounter";
            this.UnicalCounter.Name = "UnicalCounter";
            this.UnicalCounter.ReadOnly = true;
            this.UnicalCounter.ToolTipText = "UnicalCounter";
            this.UnicalCounter.Visible = false;
            // 
            // LinkPreviewFile
            // 
            this.LinkPreviewFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LinkPreviewFile.DefaultCellStyle = dataGridViewCellStyle2;
            this.LinkPreviewFile.HeaderText = "Ссылка на файл для просмотра видео анонса телеканала";
            this.LinkPreviewFile.MinimumWidth = 200;
            this.LinkPreviewFile.Name = "LinkPreviewFile";
            this.LinkPreviewFile.ReadOnly = true;
            this.LinkPreviewFile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LinkPreviewFile.ToolTipText = "Ссылка на файл для просмотра видео анонса телеканала";
            // 
            // ToolsMenuChannels
            // 
            this.ToolsMenuChannels.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolsMenuChannels.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ToolsMenuChannels.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlayTV_ChannelButton});
            this.ToolsMenuChannels.Location = new System.Drawing.Point(5, 18);
            this.ToolsMenuChannels.Name = "ToolsMenuChannels";
            this.ToolsMenuChannels.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ToolsMenuChannels.Size = new System.Drawing.Size(628, 31);
            this.ToolsMenuChannels.TabIndex = 30;
            // 
            // PlayTV_ChannelButton
            // 
            this.PlayTV_ChannelButton.Image = ((System.Drawing.Image)(resources.GetObject("PlayTV_ChannelButton.Image")));
            this.PlayTV_ChannelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PlayTV_ChannelButton.Name = "PlayTV_ChannelButton";
            this.PlayTV_ChannelButton.Size = new System.Drawing.Size(94, 28);
            this.PlayTV_ChannelButton.Text = "Проиграть";
            this.PlayTV_ChannelButton.Click += new System.EventHandler(this.PlayTV_ChannelButton_Click);
            // 
            // FormShowVideoSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 622);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(750, 660);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(657, 660);
            this.Name = "FormShowVideoSite";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Просмотр видео анонсов телеканала";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPreviewList)).EndInit();
            this.ToolsMenuChannels.ResumeLayout(false);
            this.ToolsMenuChannels.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridPreviewList;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnicalCounter;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkPreviewFile;
        private System.Windows.Forms.ToolStrip ToolsMenuChannels;
        private System.Windows.Forms.ToolStripButton PlayTV_ChannelButton;
    }
}