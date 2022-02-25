namespace InfoAboutChannel
{
    partial class ShowOnlineTVForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowOnlineTVForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ChangeFormStateButton = new System.Windows.Forms.Button();
            this.GoMainPage = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label1 = new System.Windows.Forms.Label();
            this.AdressOfOnlinePage = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AdressOfOnlinePage);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.ChangeFormStateButton);
            this.panel1.Controls.Add(this.GoMainPage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(656, 47);
            this.panel1.TabIndex = 0;
            // 
            // ChangeFormStateButton
            // 
            this.ChangeFormStateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChangeFormStateButton.Image = ((System.Drawing.Image)(resources.GetObject("ChangeFormStateButton.Image")));
            this.ChangeFormStateButton.Location = new System.Drawing.Point(607, 6);
            this.ChangeFormStateButton.Name = "ChangeFormStateButton";
            this.ChangeFormStateButton.Size = new System.Drawing.Size(43, 35);
            this.ChangeFormStateButton.TabIndex = 0;
            this.ChangeFormStateButton.UseVisualStyleBackColor = true;
            this.ChangeFormStateButton.Click += new System.EventHandler(this.ChangeFormStateButton_Click);
            // 
            // GoMainPage
            // 
            this.GoMainPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GoMainPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GoMainPage.Location = new System.Drawing.Point(484, 6);
            this.GoMainPage.Name = "GoMainPage";
            this.GoMainPage.Size = new System.Drawing.Size(68, 35);
            this.GoMainPage.TabIndex = 0;
            this.GoMainPage.Text = "Главная";
            this.GoMainPage.UseVisualStyleBackColor = true;
            this.GoMainPage.Click += new System.EventHandler(this.GoMainPage_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 47);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(656, 536);
            this.webBrowser1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Домашняя страница: ";
            // 
            // AdressOfOnlinePage
            // 
            this.AdressOfOnlinePage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AdressOfOnlinePage.Location = new System.Drawing.Point(136, 14);
            this.AdressOfOnlinePage.Name = "AdressOfOnlinePage";
            this.AdressOfOnlinePage.ReadOnly = true;
            this.AdressOfOnlinePage.Size = new System.Drawing.Size(342, 20);
            this.AdressOfOnlinePage.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(558, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 35);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ChangeFormStateButton_Click);
            // 
            // ShowOnlineTVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(656, 583);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowOnlineTVForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Онлайн просмотр канала";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.ShowOnlineTVForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ChangeFormStateButton;
        private System.Windows.Forms.Button GoMainPage;
        private System.Windows.Forms.TextBox AdressOfOnlinePage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button button1;
    }
}