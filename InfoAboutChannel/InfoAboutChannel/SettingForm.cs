#region Классы 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

#endregion

namespace InfoAboutChannel
{
    public partial class SettingForm : Form
    {
        #region Переменные 

        #region Перечисления 

        /// <summary>
        /// Определяет тип старта окна 
        /// </summary>
        public enum TypesStart
        {
            /// <summary>
            /// Запуск по истечению таймера
            /// </summary>
            Avto,
            /// <summary>
            /// Запуск пользователем 
            /// </summary>
            Standart
        }



        #endregion

        public DataSet dsetWithData;
        public OleDbConnection connectionData;
        /// <summary>
        /// Флажек показывает было обновление или нет
        /// </summary>
        public bool TimeUpdateWasChanged;
        public Thread ThreadForUpdateChannels;
        UpdaterOfChannels updateChannelsMODULE;
        /// <summary>
        /// Инструмент для получения информации с базы данных 
        /// </summary>
        OleDbDataAdapter adapter;

        TypesStart startPosition;
        
        /// <summary>
        /// Время до следующего обновления 
        /// </summary>
        TimeSpan timeForUpdate;

        #endregion

        #region Конструктор 

        public SettingForm(TypesStart type,DataSet DataSetFrom, OleDbConnection connection,TimeSpan timeForUpdateMain )
        {
            InitializeComponent();
            dsetWithData = DataSetFrom;
            connectionData = connection;
            timeForUpdate = timeForUpdateMain;
            startPosition = type;

            adapter = new OleDbDataAdapter();
            adapter.SelectCommand = new OleDbCommand("", connectionData);
            adapter.InsertCommand = new OleDbCommand("", connectionData);
            adapter.DeleteCommand = new OleDbCommand("", connectionData);
            adapter.UpdateCommand = new OleDbCommand("", connectionData);
        }

        #endregion

        #region События 

        #region Событие: При показе формы 

        private void SettingForm_Shown(object sender, EventArgs e)
        {
            #region Вывод времени на форму 

            HourBoxForUpdate.ReadOnly = false;
            HourBoxForUpdate.Text = timeForUpdate.Hours.ToString();
            HourBoxForUpdate.ReadOnly = true;

            MinuteBoxForUpdate.ReadOnly = false;
            MinuteBoxForUpdate.Text = timeForUpdate.Minutes.ToString();
            MinuteBoxForUpdate.ReadOnly = true;

            #endregion

            if (dsetWithData.Tables["SettingsApp"].Rows.Count>0)
            {
                for (int i = 0; i < dsetWithData.Tables["SettingsApp"].Rows.Count; i++)
                {
                    comboBoxOriginalTime.Text = dsetWithData.Tables["SettingsApp"].Rows[i]["HourForUpdateProgrammsList"].ToString();
                    comboBoxOriginalTimeMinute.Text = dsetWithData.Tables["SettingsApp"].Rows[i]["MinuteForUpdateProgrammsList"].ToString();
                    TimeUpdateWasChanged = false;
                }
            }

            switch (startPosition)
            {
                case TypesStart.Avto: UpdateListOfProgrammButton.PerformClick(); break;
            }
        }

        #endregion

        #region Событие: Обновление списка программ по всем телеканалам 

        private void UpdateListOfProgrammButton_Click(object sender, EventArgs e)
        {
            switch (UpdateListOfProgrammButton.Text)
            {
                case "Запуск обновления":
                    //panelSettingTimerUpdate.Enabled = false;
                    
                    HourBoxForUpdate.ReadOnly =false;
                    HourBoxForUpdate.Text = string.Empty;
                    HourBoxForUpdate.ReadOnly =true;

                    MinuteBoxForUpdate.ReadOnly = false;
                    MinuteBoxForUpdate.Text = string.Empty;
                    MinuteBoxForUpdate.ReadOnly = true;
                    okButton.Enabled = false;
                    cancelButton.Enabled = false;

                    UpdateListOfProgrammButton.Text = "Остановить обновление";
                    updateChannelsMODULE = null; updateChannelsMODULE = new UpdaterOfChannels(dsetWithData, connectionData);
                    updateChannelsMODULE.WorkingOfUpdate = true;

                    #region Загрузка списка каналов для обновления и настройка 

                    ProgrammsGridForUpdate.AutoGenerateColumns = false;
                    ProgrammsGridForUpdate.DataSource = updateChannelsMODULE.tableWithChannelInfoUpdate;
                    updateChannelsMODULE.tableWithChannelInfoUpdate.RowChanged += new DataRowChangeEventHandler(tableWithChannelInfoUpdate_RowChanged);                

                    ProgrammsGridForUpdate.Columns["UnicalCounterProgramm"].DataPropertyName = "UnicalCounterChannel";
                    ProgrammsGridForUpdate.Columns["Channel_Name"].DataPropertyName = "Channel_Name";
                    ProgrammsGridForUpdate.Columns["DateOfUpdateChannel"].DataPropertyName = "DateOfUpdateChannel";
                    ProgrammsGridForUpdate.Columns["DateOfUpdateChannel"].DefaultCellStyle.Format = "D";
                    ProgrammsGridForUpdate.Columns["StateOfUpdate"].DataPropertyName = "StateOfUpdate";

                    ProgrammsGridForUpdate.RefreshEdit();
                    ProgrammsGridForUpdate.ClearSelection();

                    if (ProgrammsGridForUpdate.RowCount>0) { ProgrammsGridForUpdate.Rows[0].Selected = true; }
                    
                    #endregion

                    if (ThreadForUpdateChannels!=null)
                    {
                        try { ThreadForUpdateChannels.Abort(); } catch (Exception) { ThreadForUpdateChannels = null; } 
                    }
                    ThreadForUpdateChannels = new Thread(ThreadingMethodUpdate);
                    ThreadForUpdateChannels.IsBackground = true;
                    ThreadForUpdateChannels.Start();
                    break;
                case "Остановить обновление":
                    UpdateListOfProgrammButton.Enabled = false;
                    updateChannelsMODULE.WorkingOfUpdate = false;
                    this.Cursor = Cursors.WaitCursor;
                    //ThreadForUpdateChannels.Abort();
                    //panelSettingTimerUpdate.Enabled = true;
                    UpdateListOfProgrammButton.Text = "Запуск обновления";
                    this.Cursor = Cursors.Default;
                    break;
            }
        }

        protected void ThreadingMethodUpdate() { updateChannelsMODULE.START(); }

        #endregion

        #region Событие: обработчик реагирует на изменение информации в таблице обновления 
        
        void tableWithChannelInfoUpdate_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (updateChannelsMODULE.tableWithChannelInfoUpdate.Rows.Count > 0)
            {
                for (int i = 0; i < updateChannelsMODULE.tableWithChannelInfoUpdate.Rows.Count; i++)
                {
                    if (updateChannelsMODULE.tableWithChannelInfoUpdate.Rows[i]["StateOfUpdate"].ToString() == "Выполняеться" || updateChannelsMODULE.tableWithChannelInfoUpdate.Rows[i]["StateOfUpdate"].ToString() == "Ожидание")
                    {
                        return;
                    }
                    else
                    {
                        if (i + 1 == updateChannelsMODULE.tableWithChannelInfoUpdate.Rows.Count)
                        {
                            this.Invoke(new EventHandler(
                                                            delegate
                                                            {
                                                                if (UpdateListOfProgrammButton.Enabled)
                                                                {
                                                                    UpdateListOfProgrammButton.PerformClick();
                                                                }
                                                                else
                                                                {
                                                                    UpdateListOfProgrammButton.Enabled = true;
                                                                    okButton.Enabled = true;
                                                                    cancelButton.Enabled = true;
                                                                    if (ThreadForUpdateChannels != null)
                                                                    {
                                                                        try { ThreadForUpdateChannels.Abort(); }
                                                                        catch (Exception) { }
                                                                    }
                                                                }
                                                            }
                                                            )); return;
                        }
                    }
                }
            }
            else { return; }
            this.Invoke(new EventHandler(
                delegate 
                {
                    if (UpdateListOfProgrammButton.Enabled)
                    {
                        UpdateListOfProgrammButton.PerformClick();     
                    }
                    else
                    {
                        UpdateListOfProgrammButton.Enabled = true;
                        okButton.Enabled = true;
                        cancelButton.Enabled = true;
                        if (ThreadForUpdateChannels != null)
                        {
                            try { ThreadForUpdateChannels.Abort(); }
                            catch (Exception) { }
                        }
                    }
                } 
                ));
        }

        #endregion

        #region Событие: Фиксация изменений времени 

        private void comboBoxOriginalTime_TextChanged(object sender, EventArgs e)
        {
            TimeUpdateWasChanged = true;

        }
        
        #endregion

        #region Событие: Нажатие кнопки "ОК" 

        private void okButton_Click(object sender, EventArgs e)
        {
            #region Проверяем изменение настроек обновления

            if (TimeUpdateWasChanged)
            {
                dsetWithData.Tables["SettingsApp"].Rows[0]["HourForUpdateProgrammsList"] = int.Parse(comboBoxOriginalTime.Text.Trim());
                dsetWithData.Tables["SettingsApp"].Rows[0]["MinuteForUpdateProgrammsList"] = int.Parse(comboBoxOriginalTimeMinute.Text.Trim());
                TimeSpan timeForNextUpdate = new TimeSpan(int.Parse(dsetWithData.Tables["SettingsApp"].Rows[0]["HourForUpdateProgrammsList"].ToString()), int.Parse(dsetWithData.Tables["SettingsApp"].Rows[0]["MinuteForUpdateProgrammsList"].ToString()), 0);
                dsetWithData.Tables["SettingsApp"].Rows[0]["DateTimeNextUpdate"] = DateTime.Now.AddMilliseconds(timeForNextUpdate.TotalMilliseconds); Update_dSet(dsetWithData.Tables["SettingsApp"]);
                Update_dSet(dsetWithData.Tables["SettingsApp"]);
                Update_dSet(dsetWithData.Tables["SettingsApp"]);
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            #endregion
        }

        #endregion

        #region Событие: Нажатие кнопки "Отмена" 

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion

        #region Событие:

        #endregion

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

        #region Методы

        #endregion


    }
}

