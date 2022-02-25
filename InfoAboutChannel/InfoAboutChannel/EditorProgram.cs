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
    public partial class EditorProgram : Form
    {
        #region Описание переменных 

        /// <summary>
        /// Хранит информацию о телепередаче
        /// </summary>
        public DataRow TV_Programm_Data;
        /// <summary>
        /// Хранит информацию о телепередаче передаю для обработки 
        /// </summary>
        public DataRow TV_Programm_Incoming;
        /// <summary>
        /// Тип редактора 
        /// </summary>
        mainForm.TypeEditor typeForm;
        /// <summary>
        /// Таблица с телепередачами 
        /// </summary>
        DataTable TableWithProgramms;

        /// <summary>
        /// Уникальный номер канала родителя передачи 
        /// </summary>
        int UnicalNumberParentChannel;

        #endregion

        #region Конструктор формы 

        public EditorProgram(mainForm.TypeEditor typeEdit, DataRow IncomingProgramRow, DataTable SourceProgramm,int UnicalNumberOfChannel)
        {
            InitializeComponent();

            switch (typeEdit)
            {
                case mainForm.TypeEditor.Add:
                    this.Text = "Добавление телепередачи";
                    okButton.Text = "Создать";
                    typeForm = mainForm.TypeEditor.Add;
                    break;
                case mainForm.TypeEditor.Edit:
                    this.Text = "Редактивароние телепередачи";
                    okButton.Text = "Применить";
                    ReadSetParamsProgramm(IncomingProgramRow);
                    typeForm = mainForm.TypeEditor.Edit;
                    break;
            }

            TV_Programm_Incoming = IncomingProgramRow;
            TableWithProgramms = SourceProgramm;
            UnicalNumberParentChannel = UnicalNumberOfChannel;
        }

        #endregion

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Методы

        #region Метод: Чтение - Загрузка значений об телепередаче 

        protected void ReadSetParamsProgramm(DataRow CurrentInfo)
        {
            nameProgrammTextBox.Text = CurrentInfo["Programm_Name"].ToString();
            GerneComboBox.Text = CurrentInfo["Programm_Genre"].ToString();
            DirectorProgrammTextBox.Text = CurrentInfo["Programm_Director"].ToString();
            DateOfProgramm.Value = (DateTime)CurrentInfo["Programm_DateOfAir"];
            TimeOfProgrammTextBox.Text = CurrentInfo["Programm_TimeOfAir"].ToString();
            LogoProgrammTextBox.Text = CurrentInfo["Programm_Logo"].ToString();
            Programm_Description_RichTextBox.Text = CurrentInfo["Programm_Description"].ToString();
        }

        #endregion

        #region Метод: Корректировка адресса, с которого запускается приложение 

        protected string RightPathFile()
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

                    if (!System.IO.Directory.Exists(RightPathFile() + "\\" + "LOGO_OF_PROGRAMM")) { System.IO.Directory.CreateDirectory(RightPathFile() + "\\" + "LOGO_OF_PROGRAMM"); }
                    
                    #region Удаляем файл если он есть и копируем новый 

                    try
                    {
                        if (System.IO.File.Exists(RightPathFile() + "\\" + "LOGO_OF_PROGRAMM" +"\\"+ System.IO.Path.GetFileName(OpenFile.FileName)))
                        {
                            System.IO.File.Delete(RightPathFile() + "\\" + "LOGO_OF_PROGRAMM" +"\\"+ System.IO.Path.GetFileName(OpenFile.FileName));
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        System.IO.File.Copy(OpenFile.FileName, RightPathFile() + "\\" + "LOGO_OF_PROGRAMM" +"\\"+ System.IO.Path.GetFileName(OpenFile.FileName));
                    }
                    catch (Exception) { }

                    #endregion

                    string rezult = "LOGO_OF_PROGRAMM\\" + System.IO.Path.GetFileName(OpenFile.FileName);

                    OpenFile.Dispose(); GC.Collect();
                    return rezult;
                    break;
            }
            OpenFile.Dispose(); GC.Collect();
            return string.Empty;
        }

        #endregion

        #region Метод: Создаем запись о канале 

        protected DataRow CreateOrEditRecordInfo(DataTable StandartTable, DataRow RowForEdit)
        {
            if (RowForEdit == null)
            {
                DataRow infoRow = StandartTable.NewRow();

                infoRow["Programm_Parent_TV_Channel"] = UnicalNumberParentChannel;
                infoRow["Programm_Name"] = nameProgrammTextBox.Text.Trim();
                infoRow["Programm_Genre"] = GerneComboBox.Text.Trim();
                infoRow["Programm_Director"] = DirectorProgrammTextBox.Text.Trim();
                infoRow["Programm_DateOfAir"] = DateOfProgramm.Value;
                infoRow["Programm_TimeOfAir"] = TimeOfProgrammTextBox.Text.Trim();
                infoRow["Programm_Logo"] = LogoProgrammTextBox.Text.Trim();
                infoRow["Programm_Description"] = Programm_Description_RichTextBox.Text.Trim();

                return infoRow;
            }
            else
            {
                RowForEdit["Programm_Name"] = nameProgrammTextBox.Text.Trim();
                RowForEdit["Programm_Genre"] = GerneComboBox.Text.Trim();
                RowForEdit["Programm_Director"] = DirectorProgrammTextBox.Text.Trim();
                RowForEdit["Programm_DateOfAir"] = DateOfProgramm.Value;
                RowForEdit["Programm_TimeOfAir"] = TimeOfProgrammTextBox.Text.Trim();
                RowForEdit["Programm_Logo"] = LogoProgrammTextBox.Text.Trim();
                RowForEdit["Programm_Description"] = Programm_Description_RichTextBox.Text.Trim();

                return RowForEdit;
            }
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
                LogoProgrammTextBox.ReadOnly = false;
                LogoProgrammTextBox.Text = temp;
                LogoProgrammTextBox.ReadOnly = true;
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

                    TV_Programm_Data = null;
                    TV_Programm_Data = CreateOrEditRecordInfo(TableWithProgramms, TV_Programm_Data);

                    #endregion
                    break;
                case mainForm.TypeEditor.Edit:
                    #region Передача параметров канала 

                    TV_Programm_Incoming = CreateOrEditRecordInfo(TableWithProgramms, TV_Programm_Incoming);

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

        #region Событие:

        #endregion

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------



      
    }
}
