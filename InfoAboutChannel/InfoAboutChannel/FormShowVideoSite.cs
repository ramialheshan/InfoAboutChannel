using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InfoAboutChannel
{
    public partial class FormShowVideoSite : Form
    {
        #region Переменные

        DataTable TableWithVideoPreview;

        #endregion


        #region Конструктор

        public FormShowVideoSite(DataRow IncomingChannelRow)
        {
            InitializeComponent();

            #region Создание таблицы для хранения списка ссылок на видео анонсы

            TableWithVideoPreview = new DataTable();
            TableWithVideoPreview.Columns.Add("UnicalCounter", System.Type.GetType("System.Int32"));
            TableWithVideoPreview.Columns.Add("LinkPreviewFile", System.Type.GetType("System.String"));

            #region Загрузка списка ссылок для телеканала

            if (!string.IsNullOrEmpty(IncomingChannelRow["TV_Channel_List_Of_VideoPreview"].ToString()))
            {
                string[] strArray = IncomingChannelRow["TV_Channel_List_Of_VideoPreview"].ToString().Split(new string[] { " , " }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length > 0)
                {
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        DataRow TMP = TableWithVideoPreview.NewRow();
                        TMP["UnicalCounter"] = i;
                        TMP["LinkPreviewFile"] = strArray[i].ToString();
                        TableWithVideoPreview.Rows.Add(TMP);
                    }
                }
            }

            UpdateListOfVideoPriview();

            if (TableWithVideoPreview.Rows.Count>0) { dataGridPreviewList.Rows[0].Selected = true; }

            #endregion

            #endregion

        }
        
        #endregion

        #region События

        private void PlayTV_ChannelButton_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = dataGridPreviewList.SelectedRows[0].Cells[1].Value.ToString();
        }

        #endregion

        #region Метод: Обновление списка телепередач текущего канала

        void UpdateListOfVideoPriview()
        {
            #region Заполнения списка сигналов выбранного объекта

            dataGridPreviewList.AutoGenerateColumns = false;
            dataGridPreviewList.DataSource = TableWithVideoPreview;

            dataGridPreviewList.Columns["UnicalCounter"].DataPropertyName = "UnicalCounter";
            dataGridPreviewList.Columns["LinkPreviewFile"].DataPropertyName = "LinkPreviewFile";

            dataGridPreviewList.RefreshEdit();
            dataGridPreviewList.ClearSelection();

            #endregion
        }

        #endregion




        #region Методы

        #endregion
       

        
    }
}
