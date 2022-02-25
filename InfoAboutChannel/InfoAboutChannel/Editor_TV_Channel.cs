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
    public partial class Editor_TV_Channel : Form
    {
        //Блок инициализации и описания 

        #region Описание переменных 

        /// <summary>
        /// Хранит информацию о канале 
        /// </summary>
        public DataRow TV_Channel_Data;
        /// <summary>
        /// Хранит информацию о канале передаю для обработки 
        /// </summary>
        public DataRow TV_Channel_Data_Incoming;
        /// <summary>
        /// Тип редактора 
        /// </summary>
        mainForm.TypeEditor typeForm;
        /// <summary>
        /// Таблица с телеканалами 
        /// </summary>
        DataTable TableWithChannels;
        /// <summary>
        /// Таблица с ссылками на видео анонсы  
        /// </summary>
        DataTable TableWithVideoPreview;

        #endregion

        #region Конструктор формы 

        public Editor_TV_Channel(mainForm.TypeEditor typeEdit,DataRow IncomingChannelRow, DataTable SourceChannel)
        {
            InitializeComponent();

            switch (typeEdit)
            {
                case mainForm.TypeEditor.Add:
                    this.Text = "Добавление канала";
                    okButton.Text = "Создать";
                    typeForm = mainForm.TypeEditor.Add;
                    break;
                case mainForm.TypeEditor.Edit:
                    this.Text = "Редактивароние канала";
                    ReadSetParamsChannel(IncomingChannelRow);
                    okButton.Text = "Применить";
                    typeForm = mainForm.TypeEditor.Edit;
                    break;
            }

            TV_Channel_Data_Incoming = IncomingChannelRow;
            TableWithChannels = SourceChannel;

            label_ID.Enabled = СheckBoxINOFFSINHChannel.Checked;
            TV_Channel_Synchronize_ID_LinkTextBox.Enabled = СheckBoxINOFFSINHChannel.Checked;

            #region Создание таблицы для хранения списка ссылок на видео анонсы 

            TableWithVideoPreview = new DataTable();
            TableWithVideoPreview.Columns.Add("UnicalCounter", System.Type.GetType("System.Int32"));
            TableWithVideoPreview.Columns.Add("LinkPreviewFile", System.Type.GetType("System.String"));

            #region Загрузка списка ссылок для телеканала 

            if (!string.IsNullOrEmpty(IncomingChannelRow["TV_Channel_List_Of_VideoPreview"].ToString()))
            {
                string[] strArray = IncomingChannelRow["TV_Channel_List_Of_VideoPreview"].ToString().Split(new string[]{" , "}, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length>0)
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

            #endregion

            #endregion
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //Методы
         
        #region Метод: Чтение - Загрузка значений об канале 

        protected void ReadSetParamsChannel(DataRow CurrentInfo)
        {
            nameChannelTextBox.Text = CurrentInfo["TV_Channel_Name"].ToString();
            CategoryChannelTextBox.Text = CurrentInfo["TV_Channel_Genre"].ToString();
            SiteOfChannelTextBox.Text = CurrentInfo["TV_Channel_Site"].ToString();
            StateOfChannelTextBox.Text = CurrentInfo["TV_Channel_Rating"].ToString();
            LanguageOfChannelTextBox.Text = CurrentInfo["TV_Channel_Language"].ToString();
            CountryTextBox.Text = CurrentInfo["TV_Channel_Country"].ToString();
            AdressTextBox.Text = CurrentInfo["TV_Channel_Adress"].ToString();
            PhoneTextBox.Text = CurrentInfo["TV_Channel_Phone"].ToString();
            LogoTextBox.Text = CurrentInfo["TV_Channel_LOGO"].ToString();

            TextBoxTV_Channel_Site_Online.Text = CurrentInfo["TV_Channel_Site_Online"].ToString();
            СheckBoxINOFFSINHChannel.Checked = bool.Parse(CurrentInfo["TV_Channel_Synchronize"].ToString());
            TV_Channel_Synchronize_ID_LinkTextBox.Text = CurrentInfo["TV_Channel_Synchronize_ID_Link"].ToString();

        }

        #endregion

        #region Метод: Корректировка адресса, с которого запускается приложение 

        public string RightPathFile()
        {
            string str = Convert.ToString(Application.StartupPath[0]);
            return str.ToUpper() + Application.StartupPath.Substring(1, Application.StartupPath.Length - 1);
        }

        #endregion

        #region Метод: Получение пути к файлу 

        protected string PathLogoFile()
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.InitialDirectory = RightPathFile();
            OpenFile.Filter = "Изображения (*.jpg,*.png,*.gif) | *.jpg; *.png; *.gif"; 
            OpenFile.AddExtension = true;
            OpenFile.Title = "Загрузка логотипа для телеканала";
            OpenFile.CheckFileExists = true;
            OpenFile.CheckPathExists = true;
            OpenFile.FilterIndex = 1;
            OpenFile.Multiselect = false;

            switch (OpenFile.ShowDialog())
            {
                case DialogResult.OK:

                    if (!System.IO.Directory.Exists(RightPathFile() + "\\" + "LOGO_OF_CHANNEL")) { System.IO.Directory.CreateDirectory(RightPathFile() + "\\" + "LOGO_OF_CHANNEL"); }

                    #region Удаляем файл если он есть и копируем новый 

                    try
                    {
                        if (System.IO.File.Exists(RightPathFile() + "\\" + "LOGO_OF_CHANNEL" +"\\"+ System.IO.Path.GetFileName(OpenFile.FileName)))
                        {
                            System.IO.File.Delete(RightPathFile() + "\\" + "LOGO_OF_CHANNEL" +"\\"+ System.IO.Path.GetFileName(OpenFile.FileName));
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        System.IO.File.Copy(OpenFile.FileName, RightPathFile() + "\\" + "LOGO_OF_CHANNEL" +"\\"+ System.IO.Path.GetFileName(OpenFile.FileName));
                    }
                    catch (Exception) { }

                    #endregion

                    string rezult = "LOGO_OF_CHANNEL\\" + System.IO.Path.GetFileName(OpenFile.FileName);
                    OpenFile.Dispose(); GC.Collect();
                    return rezult;
                    break;
            }
            OpenFile.Dispose(); GC.Collect();
            return string.Empty;
        }

        #endregion

        #region Метод: Получение пути к файлу видеоанонса 

        protected string PathVideoFile()
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.InitialDirectory = RightPathFile();
            OpenFile.Filter = "Видео файлы (*.avi,*.mpeg)|*.mpeg;*.mpeg|Все файлы (*.*)|*.*";
            OpenFile.AddExtension = true;
            OpenFile.Title = "Видеоанонсы телеканала";
            OpenFile.CheckFileExists = true;
            OpenFile.CheckPathExists = true;
            OpenFile.FilterIndex = 1;
            OpenFile.Multiselect = false;

            switch (OpenFile.ShowDialog())
            {
                case DialogResult.OK:
                    string rezult = System.IO.Path.GetFullPath(OpenFile.FileName);
                    OpenFile.Dispose(); GC.Collect();
                    return rezult;
                    break;
            }
            OpenFile.Dispose(); GC.Collect();
            return string.Empty;
        }

        #endregion

        #region Метод: Создаем запись о канале 

        protected DataRow CreateRecordInfo(DataTable StandartTable, DataRow RowForEdit)
        {
            if (RowForEdit==null)
            {
                DataRow infoRow = StandartTable.NewRow();

                infoRow["TV_Channel_Name"] = (object)nameChannelTextBox.Text.Trim();
                infoRow["TV_Channel_Genre"] = (object)CategoryChannelTextBox.Text.Trim();
                infoRow["TV_Channel_Site"] = (object)SiteOfChannelTextBox.Text.Trim();
                infoRow["TV_Channel_Rating"] = (object)StateOfChannelTextBox.Text.Trim();
                infoRow["TV_Channel_Language"] = (object)LanguageOfChannelTextBox.Text.Trim();
                infoRow["TV_Channel_Country"] = (object)CountryTextBox.Text.Trim();
                infoRow["TV_Channel_Adress"] = (object)AdressTextBox.Text.Trim();
                infoRow["TV_Channel_Phone"] = (object)PhoneTextBox.Text.Trim();
                infoRow["TV_Channel_LOGO"] = (object)LogoTextBox.Text.Trim();

                infoRow["TV_Channel_Site_Online"] = (object)TextBoxTV_Channel_Site_Online.Text.Trim();
                infoRow["TV_Channel_Synchronize"] = (object)СheckBoxINOFFSINHChannel.Checked;
                infoRow["TV_Channel_Synchronize_ID_Link"] = (object)TV_Channel_Synchronize_ID_LinkTextBox.Text.Trim();

                infoRow["TV_Channel_List_Of_VideoPreview"] = (object)ListVideoPriview();

                return infoRow;
            }
            else
            {
                RowForEdit["TV_Channel_Name"] = (object)nameChannelTextBox.Text.Trim();
                RowForEdit["TV_Channel_Genre"] = (object)CategoryChannelTextBox.Text.Trim();
                RowForEdit["TV_Channel_Site"] = (object)SiteOfChannelTextBox.Text.Trim();
                RowForEdit["TV_Channel_Rating"] = (object)StateOfChannelTextBox.Text.Trim();
                RowForEdit["TV_Channel_Language"] = (object)LanguageOfChannelTextBox.Text.Trim();
                RowForEdit["TV_Channel_Country"] = (object)CountryTextBox.Text.Trim();
                RowForEdit["TV_Channel_Adress"] = (object)AdressTextBox.Text.Trim();
                RowForEdit["TV_Channel_Phone"] = (object)PhoneTextBox.Text.Trim();
                RowForEdit["TV_Channel_LOGO"] = (object)LogoTextBox.Text.Trim();

                RowForEdit["TV_Channel_Site_Online"] = (object)TextBoxTV_Channel_Site_Online.Text.Trim();
                RowForEdit["TV_Channel_Synchronize"] = (object)СheckBoxINOFFSINHChannel.Checked;
                RowForEdit["TV_Channel_Synchronize_ID_Link"] = (object)TV_Channel_Synchronize_ID_LinkTextBox.Text.Trim();

                RowForEdit["TV_Channel_List_Of_VideoPreview"] = (object)ListVideoPriview();

                return RowForEdit;
            }
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


        #region Метод: Список видео анонсов при закрытии формы

        private string ListVideoPriview()
        {
            if (TableWithVideoPreview.Rows.Count>0)
            {
                string strArray = string.Empty;
                for (int i = 0; i < TableWithVideoPreview.Rows.Count; i++)
                {
                    strArray += TableWithVideoPreview.Rows[i]["LinkPreviewFile"].ToString().Trim()+" , ";
                }
                return strArray;
            }
            return string.Empty;
        }
        
        #endregion


        #region Метод:



        #endregion

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //События

        #region Событие: Вызов окна для загрузки 

        private void openDialogLogoGetButton_Click(object sender, EventArgs e)
        {
            string temp = PathLogoFile();
            if (temp != string.Empty) 
            {
                LogoTextBox.ReadOnly = false;
                LogoTextBox.Text = temp;
                LogoTextBox.ReadOnly = true;
            }
        }

        #endregion

        #region Событие: Нажатие кнопки "ОК" 

        private void okButton_Click(object sender, EventArgs e)
        {
            switch (typeForm)
            {
                case mainForm.TypeEditor.Add:
                    #region Передача параметров канала 

                    TV_Channel_Data = null;
                    TV_Channel_Data = CreateRecordInfo(TableWithChannels, TV_Channel_Data);

                    #endregion
                    break;
                case mainForm.TypeEditor.Edit:
                    #region Передача параметров канала 

                    TV_Channel_Data_Incoming = CreateRecordInfo(TableWithChannels, TV_Channel_Data_Incoming);

                    #endregion
                    break;
            }

            #region Результат диалога 

            this.DialogResult = DialogResult.OK;

            #endregion
        }

        #endregion

        #region Событие: Выход из редактора канала 
      
        private void cancelButton_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Событие: Изменение параметра синхронизации канала

        private void СheckBoxINOFFSINHChannel_CheckedChanged(object sender, EventArgs e)
        {
            label_ID.Enabled = (sender as CheckBox).Checked;
            TV_Channel_Synchronize_ID_LinkTextBox.Enabled = (sender as CheckBox).Checked;
        }

        #endregion

        #region Событие: Добавление ссылки на видеоанонс телеканала 

        private void addTV_ChannelButton_Click(object sender, EventArgs e)
        {
            string strPath = PathVideoFile();

            if (!string.IsNullOrEmpty(strPath))
            {
                DataRow TMP = TableWithVideoPreview.NewRow();
                TMP["UnicalCounter"] = TableWithVideoPreview.Rows.Count;
                TMP["LinkPreviewFile"] = strPath.ToString();
                TableWithVideoPreview.Rows.Add(TMP);
                UpdateListOfVideoPriview();

                dataGridPreviewList.Rows[dataGridPreviewList.Rows.Count-1].Selected = true;
            }
        }

        #endregion

        #region Событие: Удаление ссылки на видеоанонс телеканала 

        private void removeTV_ChannelButton_Click(object sender, EventArgs e)
        {
            if (dataGridPreviewList.RowCount > 0)
            {
                TableWithVideoPreview.Rows[dataGridPreviewList.SelectedRows[0].Index].Delete();
                TableWithVideoPreview.AcceptChanges();
                UpdateListOfVideoPriview();
                if (TableWithVideoPreview.Rows.Count>0)
                {
                    dataGridPreviewList.Rows[0].Selected = true;
                }
                else { removeTV_ChannelButton.Enabled = false;}
            }
        }

        #endregion

        #region Метод: Корректировка адресса, с которого запускается приложение

        string RightPathExeFile()
        {
            string str = Convert.ToString(Application.StartupPath[0]);
            return str.ToUpper() + Application.StartupPath.Substring(1, Application.StartupPath.Length - 1);
        }

        #endregion

        #region Событие: 

        private void dataGridPreviewList_SelectionChanged(object sender, EventArgs e)
        {
            if (TableWithVideoPreview.Rows.Count>0) { removeTV_ChannelButton.Enabled=true; } 
            else { removeTV_ChannelButton.Enabled = false;}
        }

        #endregion


        #region Событие:



        #endregion

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }
}
