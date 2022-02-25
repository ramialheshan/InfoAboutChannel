using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InfoAboutChannel
{
    public partial class mainForm : Form
    {
        //Блок инициализации и описания 

        #region Описание переменных 
        
        #region Перечисления 

        /// <summary>
        /// Определяет действие
        /// </summary>
        public enum TypesOperation
        {
            /// <summary>
            /// Чтение изменений
            /// </summary>
            Read,
        }
        /// <summary>
        /// Перечисление для опреления типа редактирования канала 
        /// </summary>
        public enum TypeEditor
        {
            /// <summary>
            /// Добавление нового канала 
            /// </summary>
            Add,
            /// <summary>
            /// Изменение информации о канала
            /// </summary>
            Edit
        }

        #endregion

        #region Свойства 

        #region Формы 

        ShowOnlineTVForm formOnline;
        SettingForm formSetting;

        #endregion

        /// <summary>
        /// Подключение к базе данных телепередач 
        /// </summary>
        OleDbConnection TV_Base_Connect;
        /// <summary>
        /// Инструмент для получения информации с базы данных 
        /// </summary>
        OleDbDataAdapter adapter;


        bool _NideUpdate;
        /// <summary>
        /// Показывает нужно ли обновление 
        /// </summary>
        public bool NideUpdate
        {
            get { return _NideUpdate; }
            set 
            {
                switch (value)
                {
                    case true:
                        #region Пробуем выполнить обновление списка программ 

                        if (formOnline!= null)
                        {
                            formOnline.DialogResult = System.Windows.Forms.DialogResult.Abort;
                            formOnline.Dispose(); GC.Collect(); GC.WaitForPendingFinalizers();
                        }
                        if (formSetting!=null)
                        {
                            if (formSetting.UpdateListOfProgrammButton.Text == "Запуск обновления") { formSetting.UpdateListOfProgrammButton.PerformClick(); } _NideUpdate = false;
                        }
                        else
                        {
                            TimeSpan timer = new TimeSpan(0, 0, Convert.ToInt32(timerForUpdateProgramm.Interval / 1000));
                            formSetting = new SettingForm(SettingForm.TypesStart.Avto, _dSet, TV_Base_Connect, timer);
                            switch (formSetting.ShowDialog())
                            {
                                case DialogResult.Cancel:
                                case DialogResult.OK: ReadTimeUpdateFromDateSet(); timerForUpdateProgramm.Start(); break;
                            }

                            formSetting.Dispose(); GC.Collect(); GC.WaitForPendingFinalizers(); _NideUpdate = false; formSetting = null;
                        }
                        
                        #endregion
                        break;
                }
            }
        }

        /// <summary>
        /// Следующая дата обновления
        /// </summary>
        DateTime nextUpdateDate;
        /// <summary>
        /// Времени до следующего обновления
        /// </summary>
        TimeSpan timeForNextUpdate;
        /// <summary>
        /// Таймер до активации обновления 
        /// </summary>
        int counterOfTimerAction = 0;

        /// <summary>
        /// Время до активации таймера
        /// </summary>
        int TimeOfTimerAction = 0;

        #region КАНАЛЫ ТЕЛЕПЕРЕДАЧ 
        
        /// <summary>
        /// Текущий выбранный номер
        /// </summary>
        int CurrentNumber;
        
        /// <summary>
        /// Ссылка выбранного канала на онлайн трансляцию
        /// </summary>
        string linkForUpdate;
        
        #endregion

        #region ПРОГРАММЫ ТЕЛЕПЕРЕДАЧ 

        /// <summary>
        /// Таблица содержит перечень телепередач определенного телеканала 
        /// </summary>
        DataTable ProgrammsTable;
        /// <summary>
        /// Текущая выбранная программа 
        /// </summary>
        int CurrentProgramm;
        /// <summary>
        /// Таймер, который используеться для поиска соответствий
        /// </summary>
        System.Timers.Timer _timerForTextControl;
        /// <summary>
        /// таблица используеться для обмена информации при заполнении списка программ
        /// </summary>
        DataTable TempForProgrammCreate;
        /// <summary>
        /// Информация про программу 
        /// </summary>
        DataRow CurrentInfoForProgramm;

        #endregion

        #endregion

        #endregion

        #region Конструктор формы 

        public mainForm()
        {
            InitializeComponent();

            #region Создание инструментов 

            TV_Base_Connect = new OleDbConnection();
            adapter = new OleDbDataAdapter();
            adapter.SelectCommand = new OleDbCommand("", TV_Base_Connect);
            adapter.InsertCommand = new OleDbCommand("", TV_Base_Connect);
            adapter.DeleteCommand = new OleDbCommand("", TV_Base_Connect);
            adapter.UpdateCommand = new OleDbCommand("", TV_Base_Connect);

            ProgrammsTable = new DataTable();

            #endregion

            #region Настройка формы 

            //TypeGenreComboBox.SelectedIndex = 0;

            #region Информация о телеканале 
            
            //Закрываем список действий 
            ShowInfoAboutChannelBox.Image = UP_DOWN_LIST.Images["Up.png"];
            ShowChannelProgramButton.Show();
            Channel_Info_GroupBox.Size = new Size(Channel_Info_GroupBox.Size.Width, 101);
            ShowInfoAboutChannelBox.Tag = "Up.png";

            #endregion

            #region Информация о телепередаче 

            //Закрываем список действий 
            ShowInfoAboutChannelBox.Image = UP_DOWN_LIST.Images["Up.png"];
            GroupBoxWithLogoProgramm.Show();
            Description_TV_Group_Box.Size = new Size(Description_TV_Group_Box.Size.Width, 147);
            ShowInfoAboutProgramm.Tag = "Up.png";

            _timerForTextControl = new System.Timers.Timer(1000);
            _timerForTextControl.Elapsed += new System.Timers.ElapsedEventHandler(_timerForTextControl_Elapsed);

            #endregion

            #region Информация о фильтрации 

            //Закрываем список действий 
            ShowInfoAboutFilter.Image = UP_DOWN_LIST.Images["Up.png"];
            FilterProgramButton.Show();
            GroupBoxFilterProgramms.Size = new Size(GroupBoxFilterProgramms.Size.Width, 113);
            ShowInfoAboutFilter.Tag = "Up.png";

            #endregion

            #endregion

            #region Загрузка списка информации о телепередачах

            #region Подключение к базе данных

            TV_Base_Connect.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "/" + "TV_Database.mdb;" + "Jet OLEDB:Database Password=administrator";

            try
            {
                TV_Base_Connect.Open();
            }
            catch (InvalidOperationException ex_1)
            {
                MessageBox.Show("Ошибка: " + ex_1.Message+"\n"+"Приложение будет закрыто", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
            }
            catch (OleDbException ex_2)
            {
                MessageBox.Show("Ошибка: " + ex_2.Message + "\n" + "Приложение будет закрыто", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
            }

            #endregion

            #region Загрузка данных с базы данных 

            adapter.SelectCommand.CommandText = "SELECT * FROM TV_Channels_Table";
            adapter.Fill(_dSet.Tables["TV_Channels_Table"]);

            adapter.SelectCommand.CommandText = "SELECT * FROM TV_Programms_Table";
            adapter.Fill(_dSet.Tables["TV_Programms_Table"]);

            adapter.SelectCommand.CommandText = "SELECT * FROM SettingsApp";
            adapter.Fill(_dSet.Tables["SettingsApp"]);

            #endregion

            #region Чтение параметров времени обновление 

            ReadTimeUpdateFromDateSet();

            #endregion

            LoadListOfChannel(true);

            #endregion
        }

    

        #endregion

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Методы

        #region Метод: Корректировка адресса, с которого запускается приложение 

        public string RightPathFile()
        {
            string str = Convert.ToString(Application.StartupPath[0]);
            return str.ToUpper() + Application.StartupPath.Substring(1, Application.StartupPath.Length - 1);
        }

        #endregion

        #region Метод: Загрузка списка телеканалов 

        private void LoadListOfChannel(bool Clear)
        {
            #region Очистка опций 
 
            switch (Clear)
            {
                case true: 
                    ListOfChannelsLogo.Images.Clear();
                    ListOfChannelsLogo.Images.Add("UnknownLogo", InfoAboutChannel.Properties.Resources.UnknownLogo);
                    ListOfChannelsLogo.Images.Add("ADD_32", InfoAboutChannel.Properties.Resources.ADD_32);
                    break;
            }
            ListOfChannelsListView.SelectedItems.Clear();
            ListOfChannelsListView.Items.Clear();

            #endregion

            if (_dSet.Tables["TV_Channels_Table"].Rows.Count>0)
            {
                for (int i = 0; i < _dSet.Tables["TV_Channels_Table"].Rows.Count; i++)
                {
                    ListViewItem listItem = new ListViewItem();
                    listItem.Tag = _dSet.Tables["TV_Channels_Table"].Rows[i]["UnicalCounterChannel"].ToString();
                    listItem.Text = _dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_Name"].ToString();

                    switch (Clear)
                    {
                        case true:
                            try { ListOfChannelsLogo.Images.Add(System.IO.Path.GetFileName(RightPathFile() + "\\" + _dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_LOGO"].ToString()), Image.FromFile(RightPathFile() + "\\" + _dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_LOGO"].ToString())); }
                            catch (Exception) { }
                            break;
                    }

                    #region Определение изображения для канала 

                    try
                    {
                        string path = System.IO.Path.GetFileName(RightPathFile() + "\\" + _dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_LOGO"].ToString());
                        int TryIndex = ListOfChannelsLogo.Images.IndexOfKey(path);
                        if (TryIndex >= 0) { listItem.ImageIndex = TryIndex; }
                        else
                        { listItem.ImageIndex = ListOfChannelsLogo.Images.IndexOfKey("UnknownLogo"); }
                    }
                    catch (Exception) { listItem.ImageIndex = ListOfChannelsLogo.Images.IndexOfKey("UnknownLogo"); }

                    #endregion

                    ListOfChannelsListView.Items.Add(listItem);
                }
            }

            #region Добавляем элемент "Добавить" 

            ListViewItem listPlus = new ListViewItem();
            listPlus.Name = "Add";
            listPlus.Tag = -1;
            listPlus.Text = "Добавить";
            listPlus.ImageIndex = ListOfChannelsLogo.Images.IndexOfKey("ADD_32");
            ListOfChannelsListView.Items.Add(listPlus);

            #endregion
        }

        #endregion

        #region Метод: Операции со списком каналов 

        private void Data_TV_Channel_Operation(TypesOperation type, ListViewItem Item)
        {
            if (_dSet.Tables["TV_Channels_Table"].Rows.Count > 0)
            {
                for (int i = 0; i < _dSet.Tables["TV_Channels_Table"].Rows.Count; i++)
                {
                    if (_dSet.Tables["TV_Channels_Table"].Rows[i]["UnicalCounterChannel"].ToString() == Item.Tag.ToString())
                    {
                        switch (type)
                        {
                            case TypesOperation.Read:

                                #region Чтение свойств канала 

                                ShowChannelProgramButton.Enabled = true;
                                ShowOnlineFormButton.Enabled = true;

                                nameChannelTextBox.ReadOnly = false;
                                nameChannelTextBox.Text = _dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_Name"].ToString();
                                nameChannelTextBox.ReadOnly = true;

                                CategoryChannelTextBox.ReadOnly = false;
                                CategoryChannelTextBox.Text = _dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_Genre"].ToString();
                                CategoryChannelTextBox.ReadOnly = true;

                                SiteOfChannelTextBox.ReadOnly = false;
                                SiteOfChannelTextBox.Text = _dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_Site"].ToString();
                                SiteOfChannelTextBox.ReadOnly = true;

                                StateOfChannelTextBox.ReadOnly = false;
                                StateOfChannelTextBox.Text = _dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_Rating"].ToString();
                                StateOfChannelTextBox.ReadOnly = true;

                                LanguageOfChannelTextBox.ReadOnly = false;
                                LanguageOfChannelTextBox.Text = _dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_Language"].ToString();
                                LanguageOfChannelTextBox.ReadOnly = true;

                                AdressChannelTextBox.ReadOnly = false;
                                AdressChannelTextBox.Text = _dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_Adress"].ToString();
                                AdressChannelTextBox.ReadOnly = true;

                                CountryOfChannelTextBox.ReadOnly = false;
                                CountryOfChannelTextBox.Text = _dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_Country"].ToString();
                                CountryOfChannelTextBox.ReadOnly = true;

                                PhoneTextBox.ReadOnly = false;
                                PhoneTextBox.Text = _dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_Phone"].ToString();
                                PhoneTextBox.ReadOnly = true;

                                linkForUpdate = _dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_Site_Online"].ToString();

                                #region Определение изображения для канала 

                                try
                                {
                                    string path = System.IO.Path.GetFileName(_dSet.Tables["TV_Channels_Table"].Rows[i]["TV_Channel_LOGO"].ToString());
                                    int TryIndex = ListOfChannelsLogo.Images.IndexOfKey(path);
                                    if (TryIndex >= 0) { Item.ImageIndex = TryIndex; }
                                    else
                                    { Item.ImageIndex = ListOfChannelsLogo.Images.IndexOfKey("UnknownLogo"); }
                                }
                                catch (Exception) { Item.ImageIndex = ListOfChannelsLogo.Images.IndexOfKey("UnknownLogo"); }

                                #endregion

                                #endregion

                                break;
                        }
                    }
                }
            }
        }

        #endregion

        #region Метод: Добавление нового телеканала 

        private void addTV_Channel()
        {
            Editor_TV_Channel formEdit = new Editor_TV_Channel(TypeEditor.Add, null, _dSet.Tables["TV_Channels_Table"]);
            formEdit.Owner = this;
            switch (formEdit.ShowDialog())
            {
                case DialogResult.OK:

                    #region Добавление новой строки в таблицу каналов 

                    _dSet.Tables["TV_Channels_Table"].Rows.Add(formEdit.TV_Channel_Data);

                    #region Добавить канал на форму в список каналов 

                    ListViewItem listItem = new ListViewItem();
                    listItem.Tag = formEdit.TV_Channel_Data["UnicalCounterChannel"].ToString();
                    listItem.Text = formEdit.TV_Channel_Data["TV_Channel_Name"].ToString();

                    try { ListOfChannelsLogo.Images.Add(System.IO.Path.GetFileName(RightPathFile() +"\\"+ formEdit.TV_Channel_Data["TV_Channel_LOGO"].ToString()), Image.FromFile(RightPathFile() + "\\" + formEdit.TV_Channel_Data["TV_Channel_LOGO"].ToString())); }
                    catch (Exception) { }

                    #region Определение изображения для канала 

                    try
                    {
                        string path = System.IO.Path.GetFileName(RightPathFile()+"\\"+formEdit.TV_Channel_Data["TV_Channel_LOGO"].ToString());
                        int TryIndex = ListOfChannelsLogo.Images.IndexOfKey(path);
                        if (TryIndex >= 0) { listItem.ImageIndex = TryIndex; }
                        else
                        { listItem.ImageIndex = ListOfChannelsLogo.Images.IndexOfKey("UnknownLogo"); }
                    }
                    catch (Exception) { listItem.ImageIndex = ListOfChannelsLogo.Images.IndexOfKey("UnknownLogo"); }

                    #endregion

                    ListOfChannelsListView.Items.Add(listItem);

                    #endregion

                    #endregion

                    break;
                case DialogResult.Cancel:
                    if (ListOfChannelsListView.Items.Count > 0) { ListOfChannelsListView.Items["Add"].Selected = false; }
                    break;
            }

            formEdit.Dispose();
            GC.Collect();
        }

        #endregion

        #region Метод: Обновление таблицы с талеканалами 

        public void Update_dSet(DataTable Table_For_Update)
        {
            #region Обновить в базе данных информацию по передачам 

            adapter.SelectCommand.CommandText = "SELECT * FROM " + Table_For_Update.TableName;
            OleDbCommandBuilder bilderObjectsCommands = new OleDbCommandBuilder(adapter);
            adapter.DeleteCommand = bilderObjectsCommands.GetDeleteCommand();
            adapter.UpdateCommand = bilderObjectsCommands.GetUpdateCommand();
            adapter.InsertCommand = bilderObjectsCommands.GetInsertCommand();
            try { adapter.Update(Table_For_Update); }
            catch (Exception) { }
            Table_For_Update.AcceptChanges();

            #endregion
        }

        #endregion

        #region Метод: Обновление списка телепередач текущего канала 

        void UpdateListOfProgramms(int NumberSelectedItem)
        {
            #region Заполнения списка сигналов выбранного объекта 

            ProgrammsTable.Rows.Clear();
            ProgrammsTable = _dSet.Tables["TV_Programms_Table"].Copy();

            DataView dView = new DataView(ProgrammsTable);
            dView.RowFilter = "(Programm_Parent_TV_Channel=" + CurrentNumber +") AND (" + "Programm_DateOfAir >" + getDateTimeForFind(DateTime.Now, "00:00")+")";
            dView.Sort = "UnicalCounterProgramm ASC";

            ProgrammsGrid.AutoGenerateColumns = false;
            ProgrammsGrid.DataSource = dView;

            ProgrammsGrid.Columns["UnicalCounterProgramm"].DataPropertyName = "UnicalCounterProgramm";
            ProgrammsGrid.Columns["Programm_Parent_TV_Channel"].DataPropertyName = "Programm_Parent_TV_Channel";
            ProgrammsGrid.Columns["Programm_Name"].DataPropertyName = "Programm_Name";
            ProgrammsGrid.Columns["Programm_Genre"].DataPropertyName = "Programm_Genre";
            ProgrammsGrid.Columns["Programm_DateOfAir"].DataPropertyName = "Programm_DateOfAir";
            ProgrammsGrid.Columns["Programm_DateOfAir"].DefaultCellStyle.Format = "D";

            ProgrammsGrid.Columns["Programm_TimeOfAir"].DataPropertyName = "Programm_TimeOfAir";
            ProgrammsGrid.Columns["Programm_Director"].DataPropertyName = "Programm_Director";
            ProgrammsGrid.Columns["Programm_Logo"].DataPropertyName = "Programm_Logo";
            ProgrammsGrid.Columns["Programm_Description"].DataPropertyName = "Programm_Description";

            ProgrammsGrid.RefreshEdit();
            ProgrammsGrid.ClearSelection();

            if (ProgrammsGrid.Rows.Count > 0 && NumberSelectedItem < 0) { ProgrammsGrid.Rows[ProgrammsGrid.Rows.Count-1].Selected = true; return; }
            if (ProgrammsGrid.Rows.Count > 0 && NumberSelectedItem > 0) 
            {
                for (int i = 0; i < ProgrammsGrid.Rows.Count; i++)
                {
                    if (ProgrammsGrid.Rows[i].Cells["UnicalCounterProgramm"].Value.ToString() == NumberSelectedItem.ToString())
                    {
                        ProgrammsGrid.Rows[i].Selected = true; return;
                    }
                }
                ProgrammsGrid.Rows[0].Selected = true; return;
            }

            #endregion
        }

        #endregion

        #region Метод: Фильтрации списка телепередач 

        string getDateTimeForFind(DateTime Value, string Time)
        {
            String TEMP = "#" + Value.ToString("MM/dd/yyyy") + " " + Time+":00" + "#";
            return TEMP;
        }

        void FiltredListOfProgramms()
        {
            #region Заполнения списка сигналов выбранного объекта

            ProgrammsGrid.DataSource = null;
            ProgrammsTable.Rows.Clear();
            ProgrammsTable = _dSet.Tables["TV_Programms_Table"].Copy();
            
            DataView dView = new DataView(ProgrammsTable);
            if (Programm_ComboBox.Text.Trim()==string.Empty || Programm_ComboBox.Text.Trim()=="Начните вводить название программы")
            {
                dView.RowFilter = "(Programm_Parent_TV_Channel=" + CurrentNumber + ") AND (" + "Programm_DateOfAir >=" + getDateTimeForFind(DateInFilter.Value, StartTimeIntervalValueTextBox.Text.Trim()) + " AND Programm_DateOfAir <=" + getDateTimeForFind(DateEndFilter.Value, EndTimeIntervalValueTextBox.Text.Trim()) + ")";
            }
            else
            {
                dView.RowFilter = "(Programm_Name='" + Programm_ComboBox.Text.Trim() + "')" + ") AND (" + "(Programm_Parent_TV_Channel=" + CurrentNumber + ") AND (" + "Programm_DateOfAir >=" + getDateTimeForFind(DateInFilter.Value, StartTimeIntervalValueTextBox.Text.Trim()) + " AND Programm_DateOfAir <=" + getDateTimeForFind(DateEndFilter.Value, EndTimeIntervalValueTextBox.Text.Trim()) + ")";
            }
            
            dView.Sort = "UnicalCounterProgramm ASC";

            if (dView.Count > 0)
            {
                ProgrammsTable = dView.ToTable();

                ProgrammsGrid.AutoGenerateColumns = false;
                ProgrammsGrid.DataSource = ProgrammsTable;

                ProgrammsGrid.Columns["UnicalCounterProgramm"].DataPropertyName = "UnicalCounterProgramm";
                ProgrammsGrid.Columns["Programm_Parent_TV_Channel"].DataPropertyName = "Programm_Parent_TV_Channel";
                ProgrammsGrid.Columns["Programm_Name"].DataPropertyName = "Programm_Name";
                ProgrammsGrid.Columns["Programm_Genre"].DataPropertyName = "Programm_Genre";
                ProgrammsGrid.Columns["Programm_DateOfAir"].DataPropertyName = "Programm_DateOfAir";
                ProgrammsGrid.Columns["Programm_DateOfAir"].DefaultCellStyle.Format = "D";

                ProgrammsGrid.Columns["Programm_TimeOfAir"].DataPropertyName = "Programm_TimeOfAir";
                ProgrammsGrid.Columns["Programm_Director"].DataPropertyName = "Programm_Director";
                ProgrammsGrid.Columns["Programm_Logo"].DataPropertyName = "Programm_Logo";
                ProgrammsGrid.Columns["Programm_Description"].DataPropertyName = "Programm_Description";

                ProgrammsGrid.RefreshEdit();
                ProgrammsGrid.ClearSelection();

                if (ProgrammsGrid.Rows.Count > 0 && CurrentProgramm < 0) { ProgrammsGrid.Rows[ProgrammsGrid.Rows.Count - 1].Selected = true; return; }
                if (ProgrammsGrid.Rows.Count > 0 && CurrentProgramm > 0)
                {
                    for (int i = 0; i < ProgrammsGrid.Rows.Count; i++)
                    {
                        if (ProgrammsGrid.Rows[i].Cells["UnicalCounterProgramm"].Value.ToString() == CurrentProgramm.ToString())
                        {
                            ProgrammsGrid.Rows[i].Selected = true; return;
                        }
                    }
                    ProgrammsGrid.Rows[0].Selected = true; return;
                }
            }
            else
            {
                Description_TV_Group_Box.Enabled = false;
                RemoveProgramButton.Enabled = false;
                EditProgramButton.Enabled = false;
            }

            #endregion
        }

        #endregion

        #region Метод: Загрузка программ в список 

        private void LoadListOfProgrammInComboBoxProgramms(string nameFingProgramm)
        {
            Programm_ComboBox.Items.Clear();

            #region Добавляем всех 

            if (nameFingProgramm == string.Empty)
            {
                if (_dSet.Tables["TV_Programms_Table"].Rows.Count > 0)
                {
                    for (int i = 0; i < _dSet.Tables["TV_Programms_Table"].Rows.Count; i++)
                    {
                        Programm_ComboBox.Items.Add(_dSet.Tables["TV_Programms_Table"].Rows[i]["Programm_Name"].ToString());
                    }
                }
                return;
            }

            #endregion

            DataView dView = new DataView(_dSet.Tables["TV_Programms_Table"].Copy());
            try { dView.RowFilter = "Programm_Name=" + nameFingProgramm; }
            catch (Exception) { }
            DataTable Temp = _dSet.Tables["TV_Programms_Table"].Copy();
            DataTable Rezult = _dSet.Tables["TV_Programms_Table"].Copy();

            if (dView.Count == 0)
            {
                try { dView.RowFilter = "Programm_Name=" + nameFingProgramm.ToLower(); }
                catch (Exception) { }
            }

            if (dView.Count == 1)
            {
                #region Нашли четкое соответствие 

                Rezult = dView.ToTable();
                for (int i = 0; i < Rezult.Rows.Count; i++)
                {
                    Programm_ComboBox.Items.Add(Rezult.Rows[i]["Programm_Name"].ToString());
                }

                if (Programm_ComboBox.DroppedDown) { Programm_ComboBox.DroppedDown = false; }
                Programm_ComboBox.DroppedDown = true;

                return;

                #endregion
            }
            else
            {
                #region Поиск совпадений 

                if (Temp.Rows.Count > 0)
                {
                    for (int z = 0; z < Temp.Rows.Count; )
                    {
                        int IndexENTER = Temp.Rows[z]["Programm_Name"].ToString().IndexOf(nameFingProgramm);
                        if (IndexENTER != 0)
                        {
                            if (char.IsLower(nameFingProgramm, 0))
                            {
                                IndexENTER = Temp.Rows[z]["Programm_Name"].ToString().IndexOf(nameFingProgramm.Substring(0, 1).ToUpper() + nameFingProgramm.Substring(1, nameFingProgramm.Length-1));
                            }
                            else
                            {
                                IndexENTER = Temp.Rows[z]["Programm_Name"].ToString().IndexOf(nameFingProgramm.Substring(0, 1).ToLower() + nameFingProgramm.Substring(1, nameFingProgramm.Length - 1));
                            }
                            if (IndexENTER!=0) { Temp.Rows.RemoveAt(z); z = 0; } else { z++; }
                        }
                        else { z++; }
                    }
                }

                #endregion

                if (Temp.Rows.Count > 0)
                {
                    Rezult = Temp.Copy();
                }
            }

            #region Вывод списка программ в COMBOBOX 

            if (Rezult.Rows.Count > 0)
            {
                for (int i = 0; i < Rezult.Rows.Count; i++)
                {
                    Programm_ComboBox.Items.Add(Rezult.Rows[i]["Programm_Name"].ToString());
                }

                if (Programm_ComboBox.DroppedDown) { Programm_ComboBox.DroppedDown = false; }
                Programm_ComboBox.DroppedDown = true;
            }

            #endregion
        }

        #endregion

        #region Метод: Клонирует поле с информацией 

        private void CloneDataRow(DataTable dtOld, int rowNumber)
        {
            CurrentInfoForProgramm = dtOld.NewRow();
            CurrentInfoForProgramm.ItemArray = dtOld.Rows[rowNumber].ItemArray;
        }

        #endregion

        #region Метод: Чтение параметров времени до следующего обновления 

        private void ReadTimeUpdateFromDateSet()
        {
            if (_dSet.Tables["SettingsApp"].Rows.Count > 0)
            {
                for (int i = 0; i < _dSet.Tables["SettingsApp"].Rows.Count; i++)
                {
                    nextUpdateDate = (DateTime)_dSet.Tables["SettingsApp"].Rows[i]["DateTimeNextUpdate"];
                    timeForNextUpdate = nextUpdateDate.Subtract(DateTime.Now);
                    if (timeForNextUpdate.TotalSeconds <= 0)
                    {
                        timeForNextUpdate = new TimeSpan(int.Parse(_dSet.Tables["SettingsApp"].Rows[i]["HourForUpdateProgrammsList"].ToString()), int.Parse(_dSet.Tables["SettingsApp"].Rows[i]["MinuteForUpdateProgrammsList"].ToString()), 0);
                        _dSet.Tables["SettingsApp"].Rows[i]["DateTimeNextUpdate"] = DateTime.Now.AddMilliseconds(timeForNextUpdate.TotalMilliseconds); Update_dSet(_dSet.Tables["SettingsApp"]);
                        Update_dSet(_dSet.Tables["SettingsApp"]);
                    }
                    TimeOfTimerAction = Convert.ToInt32(timeForNextUpdate.TotalSeconds * 1000);
                    counterOfTimerAction = 0;
                    timerForUpdateProgramm.Start();

                    break;
                }
            }         
        }

        #endregion


        #region Метод:

        #endregion


        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //События

        #region Событие: Показ информации о телеканале 

        private void ShowInfoAboutScriptBox_Click(object sender, EventArgs e)
        {
            switch (ShowInfoAboutChannelBox.Tag.ToString())
            {
                case "Up.png":
                    //Показываем список действий 
                    ShowInfoAboutChannelBox.Image = UP_DOWN_LIST.Images[1];
                    ShowChannelProgramButton.Hide();
                    Channel_Info_GroupBox.Size = new Size(Channel_Info_GroupBox.Size.Width, 18);
                    ShowInfoAboutChannelBox.Tag = "Down.png";
                    break;
                case "Down.png":
                    //Закрываем список действий 
                    ShowInfoAboutChannelBox.Image = UP_DOWN_LIST.Images[0];
                    ShowChannelProgramButton.Show();
                    Channel_Info_GroupBox.Size = new Size(Channel_Info_GroupBox.Size.Width, 101);
                    ShowInfoAboutChannelBox.Tag = "Up.png";
                    break;
            }
        }

        #endregion

        #region Событие: Удаление телеканала 

        private void removeTV_Channel(object sender, EventArgs e)
        {
            #region Удаляем всех программ телеканала 

            DataView dView = new DataView(_dSet.Tables["TV_Programms_Table"]);
            dView.RowFilter = "Programm_Parent_TV_Channel = " + ListOfChannelsListView.SelectedItems[0].Tag.ToString();
            if (dView.Count>0)
            {
                #region Удаление программ из таблицы 

                DataTable TableWithItem = dView.ToTable();

                for (int z = 0; z < _dSet.Tables["TV_Programms_Table"].Rows.Count; z++)
                {
                    if (_dSet.Tables["TV_Programms_Table"].Rows[z].RowState != DataRowState.Deleted)
                    {
                        for (int i = 0; i < TableWithItem.Rows.Count; i++)
                        {
                            if (_dSet.Tables["TV_Programms_Table"].Rows[z]["UnicalCounterProgramm"].ToString() == TableWithItem.Rows[i]["UnicalCounterProgramm"].ToString())
                            {
                                _dSet.Tables["TV_Programms_Table"].Rows[z].Delete(); break;
                            }
                        }
                    }
                }

                #region Обновить в базе данных информацию по передачам 

                Update_dSet(_dSet.Tables["TV_Programms_Table"]);

                #endregion

                #endregion
            }

            #endregion

            #region Удаление выбранного телеканала 

            DataView dView_Channel = new DataView(_dSet.Tables["TV_Channels_Table"]);
            dView_Channel.RowFilter = "UnicalCounterChannel = " + ListOfChannelsListView.SelectedItems[0].Tag.ToString();

            if (dView_Channel.Count > 0)
            {
                bool find = false;
                DataTable TableWithItem = dView_Channel.ToTable();

                for (int z = 0; z < _dSet.Tables["TV_Channels_Table"].Rows.Count; z++)
                {
                    if (find) { break; }
                    for (int i = 0; i < TableWithItem.Rows.Count; i++)
                    {
                        if (_dSet.Tables["TV_Channels_Table"].Rows[z]["UnicalCounterChannel"].ToString() == TableWithItem.Rows[i]["UnicalCounterChannel"].ToString())
                        {
                            _dSet.Tables["TV_Channels_Table"].Rows[z].Delete(); find = true; break;
                        }
                    }
                }
            }

            Update_dSet(_dSet.Tables["TV_Channels_Table"]);

            #endregion

            #region Удаляем элемент в списке 

            ListOfChannelsListView.BeginUpdate();
            ListOfChannelsListView.Items.Remove(ListOfChannelsListView.SelectedItems[0]);
            ListOfChannelsListView.EndUpdate();

            #endregion
        }

        #endregion

        #region Событие: Редактирование телеканала 

        private void editTV_Channel(object sender, EventArgs e)
        {
            #region Поиск текущего поля 

            if (_dSet.Tables["TV_Channels_Table"].Rows.Count > 0)
            {
                for (int i = 0; i < _dSet.Tables["TV_Channels_Table"].Rows.Count; i++)
                {
                    if (_dSet.Tables["TV_Channels_Table"].Rows[i]["UnicalCounterChannel"].ToString() == ListOfChannelsListView.SelectedItems[0].Tag.ToString())
                    {
                        Editor_TV_Channel formEdit = new Editor_TV_Channel(TypeEditor.Edit, _dSet.Tables["TV_Channels_Table"].Rows[i], _dSet.Tables["TV_Channels_Table"]);
                        formEdit.Owner = this;
                        switch (formEdit.ShowDialog())
                        {
                            case DialogResult.OK:

                                #region Добавление новой строки в таблицу каналов 

                                Update_dSet(_dSet.Tables["TV_Channels_Table"]);
                                LoadListOfChannel(false);

                                #endregion

                                #region Настройка формы 

                                ListOfChannelsListView.SelectedItems.Clear();
                                try
                                {
                                    ListOfChannelsListView.Items[CurrentNumber].Selected = true;
                                }
                                catch (Exception)
                                {
                                    if (ListOfChannelsListView.Items.Count>0)
                                    {
                                        ListOfChannelsListView.Items[0].Selected = true;
                                    }
                                }

                                #endregion

                                break;
                        }
                    }
                }
            }
	
            #endregion
        }

        #endregion

        #region Событие: Добавление канала в список каналов 

        private void addTV_ChannelButton_Click(object sender, EventArgs e)
        {
            addTV_Channel();
        }

        #endregion

        #region Событие: Выбор телеканала из списка каналов 

        private void ListOfChannelsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                if (e.Item.Tag.ToString() != CurrentNumber.ToString())
                {
                    #region Определяем канал и выводим его свойства на просмотр 

                    if (e.Item.Text == "Добавить")
                    {
                        addTV_Channel();

                        Update_dSet(_dSet.Tables["TV_Channels_Table"]);
                        LoadListOfChannel(false);

                        #region Настройка формы 

                        try { CurrentNumber = int.Parse(e.Item.Tag.ToString()); } catch (Exception) { }
                        
                        //ListOfChannelsListView.SelectedItems.Clear();

                        #endregion

                    }
                    else
                    {
                        try { CurrentNumber = int.Parse(e.Item.Tag.ToString()); } catch (Exception) { }
                        Data_TV_Channel_Operation(TypesOperation.Read, e.Item);
                         
                        #region Открываем список 

                        if (ShowInfoAboutChannelBox.Tag == "Down.png")
                        {
                            ShowInfoAboutChannelBox.Image = UP_DOWN_LIST.Images[0];
                            ShowChannelProgramButton.Show();
                            Channel_Info_GroupBox.Size = new Size(Channel_Info_GroupBox.Size.Width, 101);
                            ShowInfoAboutChannelBox.Tag = "Up.png";
                        }

                        #endregion

                        #region Активация инструментов редактирования 

                        removeTV_ChannelButton.Enabled = true;
                        editTV_ChannelButton.Enabled = true;

                        #endregion
                    }

                    #endregion
                }
                else
                {
                    if (e.Item.Text == "Добавить") { e.Item.Selected = false; }
                }
            }
            else
            {
                #region Очистка всех полей 

                ShowChannelProgramButton.Enabled = false;
                ShowOnlineFormButton.Enabled = false;

                nameChannelTextBox.ReadOnly = false;
                nameChannelTextBox.Text = string.Empty;
                nameChannelTextBox.ReadOnly = true;

                CategoryChannelTextBox.ReadOnly = false;
                CategoryChannelTextBox.Text = string.Empty;
                CategoryChannelTextBox.ReadOnly = true;

                SiteOfChannelTextBox.ReadOnly = false;
                SiteOfChannelTextBox.Text = string.Empty;
                SiteOfChannelTextBox.ReadOnly = true;

                StateOfChannelTextBox.ReadOnly = false;
                StateOfChannelTextBox.Text = string.Empty;
                StateOfChannelTextBox.ReadOnly = true;

                LanguageOfChannelTextBox.ReadOnly = false;
                LanguageOfChannelTextBox.Text = string.Empty;
                LanguageOfChannelTextBox.ReadOnly = true;

                AdressChannelTextBox.ReadOnly = false;
                AdressChannelTextBox.Text = string.Empty;
                AdressChannelTextBox.ReadOnly = true;

                CountryOfChannelTextBox.ReadOnly = false;
                CountryOfChannelTextBox.Text = string.Empty;
                CountryOfChannelTextBox.ReadOnly = true;

                PhoneTextBox.ReadOnly = false;
                PhoneTextBox.Text = string.Empty;
                PhoneTextBox.ReadOnly = true;

                #endregion

                #region Деактивация инструментов редактирования 

                CurrentNumber = -2;
                removeTV_ChannelButton.Enabled = false;
                editTV_ChannelButton.Enabled = false;

                #endregion
            }
        }

        #endregion

        #region Событие: При закрытии формы 

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            #region Закрыть соединение с базой данных

            while (TV_Base_Connect.State != ConnectionState.Open) { System.Threading.Thread.Sleep(100); Application.DoEvents(); }
            try { TV_Base_Connect.Close(); }
            catch (Exception) { }

            #endregion
        }

        #endregion

        #region Событие: Просмотр программы телепередач 

        private void ShowChannelProgramButton_Click(object sender, EventArgs e)
        {
            #region Меню для выбора типа ананонсов

            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Программа телепередач");
            cm.MenuItems[cm.MenuItems.Count - 1].Click += new EventHandler(ShowChannelProgramItem_Click);
            cm.MenuItems.Add("Видео анонсы");
            cm.MenuItems[cm.MenuItems.Count - 1].Click += new EventHandler(ShowVideoListProgramItem_Click);
            cm.Show(ShowChannelProgramButton, new Point(0, ShowChannelProgramButton.Height));

            #endregion
        }

        #endregion

        #region Событие: Обработка события вызова просмотра списка телепередач 
        
        void ShowChannelProgramItem_Click(object sender, EventArgs e)
        {
            #region Поиск программ

            _mainTabControl.SelectedTab = _mainTabControl.TabPages["ProgrammsTabPage"];
            UpdateListOfProgramms(-1);

            #endregion
        }

        #endregion

        #region Событие: Обработка события вызова списка видео анансов 

        void ShowVideoListProgramItem_Click(object sender, EventArgs e)
        {
            if (_dSet.Tables["TV_Channels_Table"].Rows.Count > 0)
            {
                for (int i = 0; i < _dSet.Tables["TV_Channels_Table"].Rows.Count; i++)
                {
                    if (_dSet.Tables["TV_Channels_Table"].Rows[i]["UnicalCounterChannel"].ToString() == ListOfChannelsListView.SelectedItems[0].Tag.ToString())
                    {
                        FormShowVideoSite formOnline = new FormShowVideoSite(_dSet.Tables["TV_Channels_Table"].Rows[i]);
                        formOnline.Owner = this;
                        formOnline.ShowDialog();
                        formOnline.Dispose(); GC.Collect(); GC.WaitForPendingFinalizers();
                    }
                }
            }
        }

        #endregion
        
        #region Событие:

        #endregion


        //Список программ 

        #region Событие: Показ информации о телепередаче 

        private void ShowInfoAboutProgramm_Click(object sender, EventArgs e)
        {
            switch (ShowInfoAboutProgramm.Tag.ToString())
            {
                case "Up.png":
                    //Показываем список действий 
                    ShowInfoAboutProgramm.Image = UP_DOWN_LIST.Images["Down.png"];
                    GroupBoxWithLogoProgramm.Hide();
                    programmParamsBox.Hide();
                    Description_TV_Group_Box.Size = new Size(Description_TV_Group_Box.Size.Width, 18);
                    ShowInfoAboutProgramm.Tag = "Down.png";
                    break;
                case "Down.png":
                    //Закрываем список действий 
                    ShowInfoAboutProgramm.Image = UP_DOWN_LIST.Images["Up.png"];
                    GroupBoxWithLogoProgramm.Show();
                    programmParamsBox.Show();
                    Description_TV_Group_Box.Size = new Size(Description_TV_Group_Box.Size.Width, 147);
                    ShowInfoAboutProgramm.Tag = "Up.png";
                    break;
            }
        }

        #endregion
         
        #region Событие: Показ информации о фильтрации 

        private void ShowInfoAboutFilter_Click(object sender, EventArgs e)
        {
            switch (ShowInfoAboutFilter.Tag.ToString())
            {
                case "Up.png":
                    //Показываем список действий 
                    ShowInfoAboutFilter.Image = UP_DOWN_LIST.Images["Down.png"];
                    FilterProgramButton.Hide();
                    GroupBoxFilterProgramms.Size = new Size(GroupBoxFilterProgramms.Size.Width, 18);
                    ShowInfoAboutFilter.Location = new Point(ShowInfoAboutFilter.Location.X, GroupBoxFilterProgramms.Location.Y);
                    ShowInfoAboutFilter.Tag = "Down.png";
                    break;
                case "Down.png":
                    //Закрываем список действий 
                    ShowInfoAboutFilter.Image = UP_DOWN_LIST.Images["Up.png"];
                    FilterProgramButton.Show();
                    GroupBoxFilterProgramms.Size = new Size(GroupBoxFilterProgramms.Size.Width, 112);
                    ShowInfoAboutFilter.Location = new Point(ShowInfoAboutFilter.Location.X, GroupBoxFilterProgramms.Location.Y);
                    ShowInfoAboutFilter.Tag = "Up.png";
                    break;
            }
        }

        #endregion

        #region Событие: Возврат к списку телеканалов 

        private void ComeBackButton_Click(object sender, EventArgs e)
        {
            _mainTabControl.SelectedTab = _mainTabControl.TabPages["ChannelsTabPage"];
        }

        #endregion

        #region Событие: Выбор программы из списка 

        private void ProgrammsGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                #region Очистка опций

                NameOfChannelLabel.Text = string.Empty;
                GenreOfChannelLabel.Text = string.Empty;
                DirectorOfChannelLabel.Text = string.Empty;
                richTextBoxChannel.Text = string.Empty;
                pictureBoxLogoOfChannel.Image = InfoAboutChannel.Properties.Resources.WhatTheProgramm;

                #endregion

                if (ProgrammsGrid.Rows.Count > 0)
                {
                    Description_TV_Group_Box.Enabled = true;
                    RemoveProgramButton.Enabled = true;
                    EditProgramButton.Enabled = true;

                    #region Заполнение опций 

                    try
                    {
                        CurrentProgramm = int.Parse(ProgrammsGrid.SelectedRows[0].Cells["UnicalCounterProgramm"].Value.ToString());
                    }
                    catch (Exception) { }

                    NameOfChannelLabel.Text = ProgrammsGrid.SelectedRows[0].Cells["Programm_Name"].Value.ToString();
                    if (string.IsNullOrEmpty(NameOfChannelLabel.Text)) { panelNameProgramm.Visible = false; } else { panelNameProgramm.Visible = true; }

                    GenreOfChannelLabel.Text = ProgrammsGrid.SelectedRows[0].Cells["Programm_Genre"].Value.ToString();
                    if (string.IsNullOrEmpty(GenreOfChannelLabel.Text)) { panelGenreProgramm.Visible = false; } else { panelGenreProgramm.Visible = true; }

                    DirectorOfChannelLabel.Text = ProgrammsGrid.SelectedRows[0].Cells["Programm_Director"].Value.ToString();
                    if (string.IsNullOrEmpty(DirectorOfChannelLabel.Text)) { panelDirector.Visible = false; } else { panelDirector.Visible = true; }
                    
                    richTextBoxChannel.Text = ProgrammsGrid.SelectedRows[0].Cells["Programm_Description"].Value.ToString();
                    if (string.IsNullOrEmpty(richTextBoxChannel.Text)) { panelDescriptionProgramm.Visible = false; } else { panelDescriptionProgramm.Visible = true; }

                    #region Загрузка изображения 

                    try
                    {
                        //Загрузка с абсолютного адреса 
                        pictureBoxLogoOfChannel.Image = Image.FromFile(RightPathFile() + "\\" + ProgrammsGrid.SelectedRows[0].Cells["Programm_Logo"].Value.ToString());
                    }
                    catch (Exception) { }
                    

                    #endregion

                    #endregion
                }
                else
                {
                    Description_TV_Group_Box.Enabled = false;
                    RemoveProgramButton.Enabled = false;
                    EditProgramButton.Enabled = false;
                }
            }
            catch (Exception) { }
        }

        #endregion

        #region Событие: Выравнивание текста в поле описания 

        private void richTextBoxChannel_TextChanged(object sender, EventArgs e)
        {
            if (richTextBoxChannel.Text!=string.Empty)
            {
                richTextBoxChannel.SelectAll();
                richTextBoxChannel.SelectionAlignment = HorizontalAlignment.Left;
            }
        }

        #endregion

        #region Событие: Добавление программы в список 

        private void AddProgramButton_Click(object sender, EventArgs e)
        {
            EditorProgram formEdit = new EditorProgram(TypeEditor.Add, null, _dSet.Tables["TV_Programms_Table"], CurrentNumber);
            formEdit.Owner = this;
            switch (formEdit.ShowDialog())
            {
                case DialogResult.OK:

                    #region Добавление новой строки в таблицу программ 

                    _dSet.Tables["TV_Programms_Table"].Rows.Add(formEdit.TV_Programm_Data);

                    #region Обновить в базе данных информацию по передачам 

                    Update_dSet(_dSet.Tables["TV_Programms_Table"]);

                    #endregion

                    UpdateListOfProgramms(-1);

                    #endregion

                    break;
            }

            formEdit.Dispose();
            GC.Collect();
        }

        #endregion

        #region Событие: Удаление программы из список 

        private void RemoveProgramButton_Click(object sender, EventArgs e)
        {
            for (int z = 0; z < _dSet.Tables["TV_Programms_Table"].Rows.Count; z++)
            {
                if (_dSet.Tables["TV_Programms_Table"].Rows[z].RowState != DataRowState.Deleted)
                {
                    if (_dSet.Tables["TV_Programms_Table"].Rows[z]["UnicalCounterProgramm"].ToString() == CurrentProgramm.ToString())
                    {
                        _dSet.Tables["TV_Programms_Table"].Rows[z].Delete(); break;
                    }
                }
            }

            #region Обновить в базе данных информацию по передачам 

            Update_dSet(_dSet.Tables["TV_Programms_Table"]);

            #endregion

            UpdateListOfProgramms(-1);
        }

        #endregion

        #region Событие: Редактирование программы 

        private void ProgrammsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditProgramButton.PerformClick();
        }

        private void editTV_Programm(object sender, EventArgs e)
        {
            #region Поиск текущего поля

            if (_dSet.Tables["TV_Programms_Table"].Rows.Count > 0)
            {
                for (int i = 0; i < _dSet.Tables["TV_Programms_Table"].Rows.Count; i++)
                {
                    if (_dSet.Tables["TV_Programms_Table"].Rows[i]["UnicalCounterProgramm"].ToString() == CurrentProgramm.ToString())
                    {
                        EditorProgram formEdit = new EditorProgram(TypeEditor.Edit, _dSet.Tables["TV_Programms_Table"].Rows[i], _dSet.Tables["TV_Programms_Table"],CurrentProgramm);
                        formEdit.Owner = this;
                        switch (formEdit.ShowDialog())
                        {
                            case DialogResult.OK:

                                #region Добавление новой строки в таблицу каналов

                                Update_dSet(_dSet.Tables["TV_Programms_Table"]);

                                #region Обновить в базе данных информацию по передачам 

                                Update_dSet(_dSet.Tables["TV_Programms_Table"]);

                                #endregion

                                UpdateListOfProgramms(-1);

                                #endregion

                                break;
                        }
                    }
                }
            }

            #endregion
        }

        #endregion

        #region Событие: Курсор мыши над полем с описанием 

        private void richTextBoxChannel_MouseEnter(object sender, EventArgs e)
        {
            ShowInfoProgramm.ToolTipTitle = "Полное описание телепередачи";
            ShowInfoProgramm.SetToolTip(richTextBoxChannel, richTextBoxChannel.Text);
            ShowInfoProgramm.ToolTipIcon = ToolTipIcon.Info;

            ShowInfoProgramm.Active = true;
        }

        #endregion

        #region Событие: Выход курсора за границы блока описания телеканала 

        private void richTextBoxChannel_MouseLeave(object sender, EventArgs e)
        {
            ShowInfoProgramm.Active = false;
        }

        #endregion

        #region Событие: Запуск фильтрации 

        private void FilterProgramButton_Click(object sender, EventArgs e)
        {
            FiltredListOfProgramms();
        }

        #endregion

        //Поиск программы в списке 

        #region Событие: Выбор программы в списке 

        private void ProgrammComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (Programm_ComboBox.Text == "Начните вводить название программы")
            {
                Programm_ComboBox.Text = string.Empty;
                Programm_ComboBox.ForeColor = SystemColors.ControlText;
            }
        }

        #endregion

        #region Событие: Изменение текста при поиске программы 

        private void Programm_ComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Left:
                case Keys.Right:
                case Keys.Down:
                case Keys.Enter:
                case Keys.Escape: return; break;
            }

            if (_timerForTextControl.Enabled) { _timerForTextControl.Stop(); }
            _timerForTextControl.Start();
        }

        #endregion

        #region Событие: Активация таймера для поиска программы 

        void _timerForTextControl_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timerForTextControl.Stop();
            this.Invoke(new EventHandler(delegate { LoadListOfProgrammInComboBoxProgramms(Programm_ComboBox.Text); }));
        }

        #endregion

        #region Событие: Выбор программы из списка 

        private void Programm_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Поиск в таблице опции 

            TempForProgrammCreate = _dSet.Tables["TV_Programms_Table"].Copy();

            if (TempForProgrammCreate.Rows.Count > 0)
            {
                for (int i = 0; i < TempForProgrammCreate.Rows.Count; i++)
                {
                    if (TempForProgrammCreate.Rows[i]["Programm_Name"].ToString() == Programm_ComboBox.Text.Trim())
                    {
                        CloneDataRow(TempForProgrammCreate, i);

                        #region Перенос свойств 

                        #region Жанр 
                         
                        //for (int z = 0; z < TypeGenreComboBox.Items.Count; z++)
                        //{
                        //    if (TempForProgrammCreate.Rows[i]["Programm_Genre"].ToString() == TypeGenreComboBox.Items[z].ToString())
                        //    {
                        //        TypeGenreComboBox.SelectedIndex = z; break;
                        //    }
                        //}

                        #endregion

                        #endregion

                        break;
                    }
                }
            }

            #endregion
        }

        #endregion

        #region Событие: Вход выход из элемента поиска программы 

        private void Programm_ComboBox_Enter(object sender, EventArgs e)
        {
            if (Programm_ComboBox.Text == "Начните вводить название программы")
            {
                Programm_ComboBox.Text = string.Empty;
                Programm_ComboBox.ForeColor = SystemColors.ControlText;
            }
        }
        private void Programm_ComboBox_Leave(object sender, EventArgs e)
        {
            if (Programm_ComboBox.Text == string.Empty)
            {
                Programm_ComboBox.ForeColor = SystemColors.GrayText;
                Programm_ComboBox.Text = "Начните вводить название программы";
                if (_timerForTextControl.Enabled) { _timerForTextControl.Stop(); }
                Programm_ComboBox.Items.Clear();
            }
            else
            {
                #region Поиск в таблице опции

                TempForProgrammCreate = _dSet.Tables["TV_Programms_Table"].Copy();

                if (TempForProgrammCreate.Rows.Count > 0)
                {
                    for (int i = 0; i < TempForProgrammCreate.Rows.Count; i++)
                    {
                        if (TempForProgrammCreate.Rows[i]["Programm_Name"].ToString() == Programm_ComboBox.Text.Trim())
                        {
                            CloneDataRow(TempForProgrammCreate, i);
                             
                            #region Перенос свойств 

                            #region Жанр 

                            //for (int z = 0; z < TypeGenreComboBox.Items.Count; z++)
                            //{
                            //    if (TempForProgrammCreate.Rows[i]["Programm_Genre"].ToString() == TypeGenreComboBox.Items[z].ToString())
                            //    {
                            //        TypeGenreComboBox.SelectedIndex = z; break;
                            //    }
                            //}

                            #endregion

                            #endregion

                            break;
                        }
                    }
                }

                #endregion
            }
        }

        #endregion

        #region Событие: Выбор жанра другой программы 

        private void TypeGenreComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Programm_ComboBox.ForeColor = SystemColors.GrayText;
            Programm_ComboBox.Text = "Начните вводить название программы";
            if (_timerForTextControl.Enabled) { _timerForTextControl.Stop(); }
            Programm_ComboBox.Items.Clear();
        }

        #endregion

        //Меню трея


        #region События: Нажатие кнопки "Выход из программы" 

        private void toolStripExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Событие: Показать спрятать главное окно 

        private void toolStripShowMainForm_Click(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Minimized: this.WindowState = FormWindowState.Normal; toolStripShowMainForm.Text = "Скрыть главное окно"; break;
                case FormWindowState.Normal: this.WindowState = FormWindowState.Minimized; toolStripShowMainForm.Text = "Показать главное окно"; break;
            }
        }

        #endregion

        #region Событие: Вызов окна настройки программы 

        private void toolStripSettingForm_Click(object sender, EventArgs e)
        {
            timerForUpdateProgramm.Stop();
            TimeSpan timer = new TimeSpan(0, 0, Convert.ToInt32((TimeOfTimerAction-counterOfTimerAction)/ 1000));
            formSetting = new SettingForm(SettingForm.TypesStart.Standart, _dSet, TV_Base_Connect, timer);
            switch (formSetting.ShowDialog())
            {
                case DialogResult.Cancel: timerForUpdateProgramm.Start();break;

                case DialogResult.OK:

                    if (formSetting.TimeUpdateWasChanged)
                    {
                        ReadTimeUpdateFromDateSet();
                    }
                    else { timerForUpdateProgramm.Start(); }

                    break;
            }

            formSetting.Dispose(); GC.Collect(); GC.WaitForPendingFinalizers(); formSetting = null; 

            #region Приминение изменений 


            #endregion
        }

        #endregion

        #region Событие: Показ формы онлайн просмотра канала 

        private void ShowOnlineFormButton_Click(object sender, EventArgs e)
        {
            formOnline = new ShowOnlineTVForm(linkForUpdate);
            formOnline.ShowDialog(); formOnline.Dispose(); GC.Collect(); GC.WaitForPendingFinalizers();
        }

        #endregion

        #region Событие: Попытка обновления списка программ 

        private void timerForUpdateProgramm_Tick(object sender, EventArgs e)
        {
            counterOfTimerAction += 1000;

            #region Проверяем можем ли запустить обновление программ

            if (counterOfTimerAction>=TimeOfTimerAction)
            {
                counterOfTimerAction = 0;
                timerForUpdateProgramm.Stop();
                NideUpdate = true;    
            }

            #endregion
        }

        #endregion

        #region Событие:


        #endregion

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }
}
