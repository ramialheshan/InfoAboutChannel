using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace InfoAboutChannel
{
    public class UpdaterOfChannels
    {
        #region Переменные

        #region Перечисления

        /// <summary>
        /// Определяет время дня для выбора телепередач
        /// </summary>
        enum TypesTimeOfDay
        {
            /// <summary>
            /// Весь день
            /// </summary>
            tmall,
            /// <summary>
            /// Сейчас
            /// </summary>
            tmnow,
            /// <summary>
            /// Утро 5-12
            /// </summary>
            tmmrn,
            /// <summary>
            /// День 11-18
            /// </summary>
            tmday,
            /// <summary>
            /// Вечер 17-23
            /// </summary>
            tmevn,
            /// <summary>
            /// Ночь 23-06
            /// </summary>
            tmngt
        }

        ///// <summary>
        ///// Идентификатор канала
        ///// </summary>
        //enum IDOfChannel
        //{
        //    /// <summary>
        //    /// 1+1
        //    /// </summary>
        //    id13,
        //    /// <summary>
        //    /// 2+2
        //    /// </summary>
        //    id262,
        //    /// <summary>
        //    /// 5 канал
        //    /// </summary>
        //    id316,
        //    /// <summary>
        //    /// ICTV
        //    /// </summary>
        //    id7,
        //    /// <summary>
        //    /// Интер
        //    /// </summary>
        //    id16,
        //    /// <summary>
        //    /// Новый канал
        //    /// </summary>
        //    id141
        //}

        #endregion

        #region Свойства 

        /// <summary>
        /// Подключение к базе данных телепередач 
        /// </summary>
        OleDbConnection TV_Base_Connect;
        /// <summary>
        /// Инструмент для получения информации с базы данных 
        /// </summary>
        OleDbDataAdapter adapter;
        /// <summary>
        /// Подключеие к базе 
        /// </summary>
        DataSet dSet;
        /// <summary>
        /// Информация про программу для добавления 
        /// </summary>
        DataRow CurrentInfoAboutProgramm;

        /// <summary>
        /// Состояние процесса обновления списка программ
        /// </summary>
        public bool WorkingOfUpdate;

        /// <summary>
        /// Содержит информацию по списку обновления каналов 
        /// </summary>
        public DataTable tableWithChannelInfoUpdate;
        /// <summary>
        /// показывает поток обновления остановлен или нет 
        /// </summary>
        public bool CurrentStateOfUpdate;

        #endregion

        #endregion

        #region Конструктор 

        public UpdaterOfChannels(DataSet dSet_WithData, OleDbConnection DataConnection)
        {
            #region Создаем инструменты для работы 

            #region Работа с данными 

            TV_Base_Connect = DataConnection;
            adapter = new OleDbDataAdapter();
            dSet = dSet_WithData;
            adapter.SelectCommand = new OleDbCommand("", TV_Base_Connect);
            adapter.InsertCommand = new OleDbCommand("", TV_Base_Connect);
            adapter.DeleteCommand = new OleDbCommand("", TV_Base_Connect);
            adapter.UpdateCommand = new OleDbCommand("", TV_Base_Connect);

            #endregion

            #region Создаем таблицу обновлений 

            tableWithChannelInfoUpdate = new DataTable();

            tableWithChannelInfoUpdate.Columns.Add("UnicalCounterProgramm");
            tableWithChannelInfoUpdate.Columns["UnicalCounterProgramm"].DataType = System.Type.GetType("System.Int32");
            tableWithChannelInfoUpdate.Columns.Add("Channel_Name");
            tableWithChannelInfoUpdate.Columns["Channel_Name"].DataType = System.Type.GetType("System.String");
            tableWithChannelInfoUpdate.Columns.Add("DateOfUpdateChannel");
            tableWithChannelInfoUpdate.Columns["DateOfUpdateChannel"].DataType = System.Type.GetType("System.DateTime");
            tableWithChannelInfoUpdate.Columns.Add("StateOfUpdate");
            tableWithChannelInfoUpdate.Columns["StateOfUpdate"].DataType = System.Type.GetType("System.String");

            DataView dView = new DataView(dSet.Tables["TV_Channels_Table"]);
            dView.RowFilter = "TV_Channel_Synchronize=" + bool.TrueString;

            #region Заполняем список каналов для обновления 

            if (dView.Count > 0)
            {
                foreach (DataRowView row in dView) 
                {
                    DataRow rowTEMP = tableWithChannelInfoUpdate.NewRow();
                    rowTEMP["UnicalCounterProgramm"] = int.Parse(row["UnicalCounterChannel"].ToString());
                    rowTEMP["Channel_Name"] = row["TV_Channel_Name"];
                    rowTEMP["StateOfUpdate"] = "Ожидание";
                    tableWithChannelInfoUpdate.Rows.Add(rowTEMP);
                }
            }

            #endregion

            #endregion

            #endregion
        }

        #endregion

        #region События 

        #endregion

        #region Методы

        #region Метод: Запуск обновления списка передач по всем каналам 

        public void START()
        {
            CurrentStateOfUpdate = true;

            #region Обновление списка программ по всем каналам 

            //перебираем все записи по каналам
            foreach (DataRow ChannelRow in dSet.Tables["TV_Channels_Table"].Rows)
            {
                if (bool.Parse(ChannelRow["TV_Channel_Synchronize"].ToString()))
                {
                    #region Чистка списка программ по данному каналу 

                    DateTime Endtime = DateTime.Now;
                    int counter = 0;
                    switch (DateTime.Now.DayOfWeek)
                    {
                        case DayOfWeek.Monday: Endtime = Endtime.AddDays(14); counter = 14; break;
                        case DayOfWeek.Tuesday: Endtime = Endtime.AddDays(13); counter = 13; break;
                        case DayOfWeek.Wednesday: Endtime = Endtime.AddDays(12); counter = 12; break;
                        case DayOfWeek.Thursday: Endtime = Endtime.AddDays(11); counter = 11; break;
                        case DayOfWeek.Friday: Endtime = Endtime.AddDays(10); counter = 10; break;
                        case DayOfWeek.Saturday: Endtime = Endtime.AddDays(9); counter = 9; break;
                        case DayOfWeek.Sunday: Endtime = Endtime.AddDays(8); counter = 8; break;
                    }

                    ClearProgrammsAboutChannel(ChannelRow["UnicalCounterChannel"].ToString(), DateTime.Now, Endtime);

                    #endregion

                    #region Выполняем обновление списка программ 

                    DateTime dateOfUpdate = DateTime.Now;
                    for (int s = 0; s < counter; s++)
                    {
                        if (WorkingOfUpdate)
                        {
                            dateOfUpdate = DateTime.Now.AddDays(s);
                            UpdateTableOfUpdating(int.Parse(ChannelRow["UnicalCounterChannel"].ToString()), dateOfUpdate, "Выполняеться");
                            LoadProgramListFromWebPage(dateOfUpdate, ChannelRow, TypesTimeOfDay.tmall);
                        }
                        else { break; }
                    }
                    UpdateTableOfUpdating(int.Parse(ChannelRow["UnicalCounterChannel"].ToString()), dateOfUpdate, "Завершено");

                    #endregion

                    if (!WorkingOfUpdate)  { break; }
                }
            }

            #endregion

            CurrentStateOfUpdate = false;
        }

        #endregion

        #region Запрос HTML страницы 

        /// <summary>
        /// Метод: Возращает строку с HTML страницей 
        /// </summary>
        /// <param name="pathSTR">URI ссылка на страницу</param>
        /// <returns>Возращает строку с HTML страницей</returns>
        string GetHTMLOfPage(string pathSTR)
        {
            string HTML_Data = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pathSTR);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (response.CharacterSet == null) readStream = new StreamReader(receiveStream);
                    else readStream = new StreamReader(receiveStream, Encoding.GetEncoding(1251));

                    HTML_Data = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                    return HTML_Data;
                }
                else { return null; }
            }
            catch (Exception e) { return null; }
        }

        #endregion

        #region Метод: Загрузка списка программ с веб страницы 

        void LoadProgramListFromWebPage(DateTime day, DataRow Channel, TypesTimeOfDay time)
        {
            #region Формирование URL

            DateTime currentdate = day;
            string pathSTR = string.Empty;
            string STR = "http://tvgid.ua/tv-program/ch/";
            string STRDate = day.ToString("ddMMyyyy");

            pathSTR = STR + STRDate + "/" + Channel["TV_Channel_Synchronize_ID_Link"].ToString() + "/" + time.ToString() + "/";

            #endregion

            #region Времменные переменные

            string HTML_Data = string.Empty;
            HtmlAgilityPack.HtmlNodeCollection ItemProgrammCollection = null; //список программ
            HtmlAgilityPack.HtmlNodeCollection timeOfItemProgrammCollection = null; //список ссылок по программам
            TimeSpan OriginalTime = new TimeSpan(0, 0, 0); 

            #endregion

            #region Загрузка списка телепередач из HTML страницы

            string ItemInnerHTMLpathSTR = GetHTMLOfPage(pathSTR);

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(ItemInnerHTMLpathSTR);

            if (htmlDoc.DocumentNode != null)
            {
                timeOfItemProgrammCollection = htmlDoc.DocumentNode.SelectNodes("//td[@class='time']");
                ItemProgrammCollection = htmlDoc.DocumentNode.SelectNodes("//td[@class='item']");
            }

            #region Загрузка информации по телепередачам

            if (ItemProgrammCollection != null && timeOfItemProgrammCollection != null)
            {
                if (ItemProgrammCollection.Count > 0)
                {
                    for (int i = 0; i < ItemProgrammCollection.Count; i++)
                    {
                        if (!WorkingOfUpdate) { break; }
                        var Doc = new HtmlAgilityPack.HtmlDocument();
                        Doc.LoadHtml(ItemProgrammCollection[i].InnerHtml);

                        #region Загрузка информации про передачу

                        var control = Doc.DocumentNode.SelectSingleNode("//a");
                        CurrentInfoAboutProgramm = dSet.Tables["TV_Programms_Table"].NewRow();
                        if (control.Attributes["href"] != null)
                        {
                            if (!string.IsNullOrEmpty(Doc.DocumentNode.SelectSingleNode("//a").Attributes["href"].Value))
                            {
                                string ItemInnerHTML = GetHTMLOfPage("http://tvgid.ua" + Doc.DocumentNode.SelectSingleNode("//a").Attributes["href"].Value);
                                if (!string.IsNullOrEmpty(ItemInnerHTML))
                                {
                                    #region Проверка даты и времени программы 

                                    var stringArray = timeOfItemProgrammCollection[i].InnerHtml.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                    TimeSpan tempControl = new TimeSpan(int.Parse(stringArray[0]), int.Parse(stringArray[1]), 0);

                                    switch (TimeSpan.Compare(tempControl,OriginalTime))
                                    {
                                        case 1:  break;
                                        case -1: currentdate = currentdate.AddDays(1); break;
                                    }
                                    OriginalTime = new TimeSpan(tempControl.Hours, tempControl.Minutes, tempControl.Seconds);

                                    currentdate = new DateTime(currentdate.Year, currentdate.Month, currentdate.Day, tempControl.Hours, tempControl.Minutes, tempControl.Seconds);

                                    #endregion

                                    #region Подготовка содержимого для чтения 

                                    HtmlAgilityPack.HtmlDocument htmlDocItem = new HtmlAgilityPack.HtmlDocument();
                                    htmlDocItem.LoadHtml(ItemInnerHTML);

                                    var itemMAIN = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']");
                                    htmlDocItem.LoadHtml(itemMAIN.InnerHtml);
                                    var itemIMG = htmlDocItem.DocumentNode.SelectSingleNode("//img");
                                    
                                    #endregion
                                    
                                    #region Чтение и разбор содержимого 

                                    CurrentInfoAboutProgramm["Programm_Parent_TV_Channel"] = Channel["UnicalCounterChannel"]; 
                                    CurrentInfoAboutProgramm["Programm_Name"] = control.InnerHtml.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&ndash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim().ToString();
                                    CurrentInfoAboutProgramm["Programm_Genre"] = "";

                                    CurrentInfoAboutProgramm["Programm_DateOfAir"] = currentdate;
                                    CurrentInfoAboutProgramm["Programm_TimeOfAir"] = timeOfItemProgrammCollection[i].InnerHtml;
                                    CurrentInfoAboutProgramm["Programm_Director"] = "";

                                    #region определение и загрузка файла изображения передачи 

                                    try
                                    {
                                        Uri uri = new Uri("http://tvgid.ua" + itemIMG.Attributes["src"].Value);
                                        string filename = string.Empty;

                                        try
                                        {
                                            filename = System.IO.Path.GetFileName(uri.LocalPath);
                                            DownloadRemoteImageFile("http://tvgid.ua" + itemIMG.Attributes["src"].Value, RightPathFile() + "\\" + "LOGO_OF_CHANNEL" + "\\" + filename);
                                            CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                        }
                                        catch (Exception) { CurrentInfoAboutProgramm["Programm_Logo"] = ""; }

                                        CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    }
                                    catch (Exception)
                                    {
                                        CurrentInfoAboutProgramm["Programm_Logo"] = itemMAIN.InnerHtml.Trim().ToString();
                                    }

                                    #endregion

                                    #region Определяем описание программы 

                                    try
                                    {
                                        string itemFindDescription = itemMAIN.InnerHtml.Replace(itemIMG.OuterHtml, "").Replace("\r\n", "").Replace("\t", "").Replace("<br><br>", "\r\n").Replace("<br>", "\r\n").Replace("<strong>", "").Replace("</strong>", "").Replace("<p>", "").Replace("</p>", "").Replace("<b>", "").Replace("</b>", "");
                                        CurrentInfoAboutProgramm["Programm_Description"] = itemFindDescription.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim();
                                    }
                                    catch (Exception)
                                    {
                                        CurrentInfoAboutProgramm["Programm_Description"] = itemMAIN.InnerHtml.Trim().ToString();
                                    }

                                    #endregion

                                    CurrentInfoAboutProgramm["Programm_HowCreate"] = "Web";

                                    dSet.Tables["TV_Programms_Table"].Rows.Add(CurrentInfoAboutProgramm);

                                    continue;
                                    
                                    #endregion

                                    #region МУСОР 


                                    

                                    //#region Основные запросы

                                    //var itemFind = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p/strong/img");
                                    //var test = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']");

                                    //var itemFindTEST = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p");
                                    

                                    //var itemFindAll = htmlDocItem.DocumentNode.SelectSingleNode("//img");

                                    //#region Варианты содержимого




                                    //#endregion

                                    //#endregion

                                    //#region Рабочий вариант

                                    //#region Вариант содержимого №1

                                    ////Выборка информации V1 
                                    //var itemFindV1 = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p/strong/img");
                                    //var itemFindV1_2 = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p/strong[2]");

                                    //var itemFindV1_b = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b/img");
                                    //var itemFindV1_2_b = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b[2]");

                                    //if (itemFindV1 != null && itemFindV1_2 != null)
                                    //{
                                    //    #region Полная информация про передачу

                                    //    CurrentInfoAboutProgramm["Programm_Parent_TV_Channel"] = Channel["UnicalCounterChannel"];
                                    //    CurrentInfoAboutProgramm["Programm_Name"] = control.InnerHtml.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim().ToString();

                                    //    #region Определяем жанр программы

                                    //    try
                                    //    {
                                    //        string[] itemFindGenre = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p/strong").InnerHtml.Replace(itemFindV1.OuterHtml, "").Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                    //        CurrentInfoAboutProgramm["Programm_Genre"] = itemFindGenre[0].Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim();
                                    //    }
                                    //    catch (Exception) { CurrentInfoAboutProgramm["Programm_Genre"] = itemFindAll.InnerHtml.Trim().ToString(); }

                                    //    #endregion

                                    //    CurrentInfoAboutProgramm["Programm_DateOfAir"] = currentdate;
                                    //    CurrentInfoAboutProgramm["Programm_TimeOfAir"] = timeOfItemProgrammCollection[i].InnerHtml;

                                    //    #region Определяем режиссера программы

                                    //    try
                                    //    {
                                    //        string[] itemFindDirector = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p").InnerHtml.Replace(htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p/strong[1]").OuterHtml, "").Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                    //        CurrentInfoAboutProgramm["Programm_Director"] = itemFindDirector[0].Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim();
                                    //    }
                                    //    catch (Exception) { CurrentInfoAboutProgramm["Programm_Director"] = itemFindAll.InnerHtml.Trim().ToString(); }

                                    //    #endregion

                                    //    #region Определение и загрузка файла изображения передачи

                                    //    try
                                    //    {
                                    //        Uri uri = new Uri("http://tvgid.ua" + itemFindV1.Attributes["src"].Value);
                                    //        string filename = string.Empty;

                                    //        try
                                    //        {
                                    //            filename = System.IO.Path.GetFileName(uri.LocalPath);
                                    //            DownloadRemoteImageFile("http://tvgid.ua" + itemFindV1.Attributes["src"].Value, RightPathFile() + "\\" + "LOGO_OF_CHANNEL" + "\\" + filename);
                                    //            CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //        }
                                    //        catch (Exception) { CurrentInfoAboutProgramm["Programm_Logo"] = ""; }

                                    //        CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //    }
                                    //    catch (Exception) { CurrentInfoAboutProgramm["Programm_Logo"] = itemFindAll.InnerHtml.Trim().ToString(); }

                                    //    #endregion

                                    //    #region Определяем описание программы

                                    //    try
                                    //    {
                                    //        string itemFindDescriptionTEMP = string.Empty;
                                    //        for (int z = 0; z < 5; z++)
                                    //        {
                                    //            //Проверяем если другие текстовые параграфы в документе
                                    //            var item_P = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p[" + (z + 2) + "]");
                                    //            if (item_P != null)
                                    //            {
                                    //                itemFindDescriptionTEMP += item_P.InnerHtml;
                                    //            }
                                    //            else { break; }
                                    //        }

                                    //        if (!string.IsNullOrEmpty(itemFindDescriptionTEMP))
                                    //        {
                                    //            CurrentInfoAboutProgramm["Programm_Description"] = itemFindDescriptionTEMP.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim();
                                    //        }
                                    //        else
                                    //        {
                                    //            string[] itemFindDescription = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p").InnerHtml.Replace(htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p/strong[1]").OuterHtml, "").ToString().Replace("&nbsp;", "").Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                    //            try
                                    //            {
                                    //                CurrentInfoAboutProgramm["Programm_Description"] = itemFindDescription[3].Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim();
                                    //            }
                                    //            catch (Exception)
                                    //            {
                                    //                try { CurrentInfoAboutProgramm["Programm_Description"] = itemFindDescription[3].Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim(); }
                                    //                catch (Exception) { }
                                    //            }
                                    //        }

                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        CurrentInfoAboutProgramm["Programm_Description"] = itemFindAll.InnerHtml.Trim().ToString();
                                    //    }

                                    //    #endregion

                                    //    CurrentInfoAboutProgramm["Programm_HowCreate"] = "Web";

                                    //    dSet.Tables["TV_Programms_Table"].Rows.Add(CurrentInfoAboutProgramm);

                                    //    #endregion

                                    //    continue;
                                    //}

                                    //if (itemFindV1_b != null && itemFindV1_2_b != null)
                                    //{
                                    //    #region Полная информация про передачу

                                    //    CurrentInfoAboutProgramm["Programm_Parent_TV_Channel"] = Channel["UnicalCounterChannel"];
                                    //    CurrentInfoAboutProgramm["Programm_Name"] = control.InnerHtml.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim().ToString();

                                    //    #region Определяем жанр программы

                                    //    try
                                    //    {
                                    //        string[] itemFindGenre = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b").InnerHtml.Replace(itemFindV1_b.OuterHtml, "").Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                    //        CurrentInfoAboutProgramm["Programm_Genre"] = itemFindGenre[0].Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim();
                                    //    }
                                    //    catch (Exception) { CurrentInfoAboutProgramm["Programm_Genre"] = itemFindAll.InnerHtml.Trim().ToString(); }

                                    //    #endregion

                                    //    CurrentInfoAboutProgramm["Programm_DateOfAir"] = currentdate;
                                    //    CurrentInfoAboutProgramm["Programm_TimeOfAir"] = timeOfItemProgrammCollection[i].InnerHtml;

                                    //    #region Определяем режиссера программы

                                    //    try
                                    //    {
                                    //        string[] itemFindDirector = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']").InnerHtml.Replace(htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b").OuterHtml, "").Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                    //        CurrentInfoAboutProgramm["Programm_Director"] = itemFindDirector[0].Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Replace("\r\n", "").Replace("\t", "").Trim();
                                    //    }
                                    //    catch (Exception) { CurrentInfoAboutProgramm["Programm_Director"] = itemFindAll.InnerHtml.Trim().ToString(); }

                                    //    #endregion

                                    //    #region Определение и загрузка файла изображения передачи

                                    //    try
                                    //    {
                                    //        Uri uri = new Uri("http://tvgid.ua" + itemFindV1_b.Attributes["src"].Value);
                                    //        string filename = string.Empty;

                                    //        try
                                    //        {
                                    //            filename = System.IO.Path.GetFileName(uri.LocalPath);
                                    //            DownloadRemoteImageFile("http://tvgid.ua" + itemFindV1_b.Attributes["src"].Value, RightPathFile() + "\\" + "LOGO_OF_CHANNEL" + "\\" + filename);
                                    //            CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //        }
                                    //        catch (Exception) { CurrentInfoAboutProgramm["Programm_Logo"] = ""; }

                                    //        CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //    }
                                    //    catch (Exception) { CurrentInfoAboutProgramm["Programm_Logo"] = itemFindAll.InnerHtml.Trim().ToString(); }

                                    //    #endregion

                                    //    #region Определяем описание программы

                                    //    try
                                    //    {
                                    //        var test1 = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']").InnerHtml;
                                    //        var test2 = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b").OuterHtml;
                                    //        var test3 = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b[2]").OuterHtml;
                                    //        string[] itemFindDescription = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']").InnerHtml.Replace(htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b").OuterHtml, "").Replace(htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b[2]").OuterHtml, "").ToString().Replace("&nbsp;", "").Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                    //        CurrentInfoAboutProgramm["Programm_Description"] = itemFindDescription[2].Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim();
                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        CurrentInfoAboutProgramm["Programm_Description"] = itemFindAll.InnerHtml.Trim().ToString();
                                    //    }

                                    //    #endregion

                                    //    CurrentInfoAboutProgramm["Programm_HowCreate"] = "Web";

                                    //    dSet.Tables["TV_Programms_Table"].Rows.Add(CurrentInfoAboutProgramm);

                                    //    #endregion

                                    //    continue;
                                    //}

                                    //#endregion

                                    //#region Вариант содержимого №2

                                    //if (itemFindV1 != null && itemFindV1_2 == null)
                                    //{
                                    //    #region Полная информация про передачу

                                    //    CurrentInfoAboutProgramm["Programm_Parent_TV_Channel"] = Channel["UnicalCounterChannel"];
                                    //    CurrentInfoAboutProgramm["Programm_Name"] = control.InnerHtml.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim().ToString();

                                    //    #region Определяем жанр программы 

                                    //    try
                                    //    {
                                    //        CurrentInfoAboutProgramm["Programm_Genre"] = "";

                                    //        //string[] itemFindGenre = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p/strong").InnerHtml.Replace(itemFindV1.OuterHtml, "").Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                    //        //CurrentInfoAboutProgramm["Programm_Genre"] = itemFindGenre[0].Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim();
                                    //    }
                                    //    catch (Exception) { CurrentInfoAboutProgramm["Programm_Genre"] = itemFindAll.InnerHtml.Trim().ToString(); }

                                    //    #endregion

                                    //    CurrentInfoAboutProgramm["Programm_DateOfAir"] = currentdate;
                                    //    CurrentInfoAboutProgramm["Programm_TimeOfAir"] = timeOfItemProgrammCollection[i].InnerHtml;

                                    //    #region Определяем режиссера программы

                                    //    try
                                    //    {
                                    //        string[] itemFindDirector = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p").InnerHtml.Replace(htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p/strong[1]").OuterHtml, "").Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                    //        CurrentInfoAboutProgramm["Programm_Director"] = itemFindDirector[0].Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim();
                                    //    }
                                    //    catch (Exception) { CurrentInfoAboutProgramm["Programm_Director"] = itemFindAll.InnerHtml.Trim().ToString(); }

                                    //    #endregion

                                    //    #region определение и загрузка файла изображения передачи

                                    //    try
                                    //    {
                                    //        Uri uri = new Uri("http://tvgid.ua" + itemFindV1.Attributes["src"].Value);
                                    //        string filename = string.Empty;

                                    //        try
                                    //        {
                                    //            filename = System.IO.Path.GetFileName(uri.LocalPath);
                                    //            DownloadRemoteImageFile("http://tvgid.ua" + itemFindV1.Attributes["src"].Value, RightPathFile() + "\\" + "LOGO_OF_CHANNEL" + "\\" + filename);
                                    //            CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //        }
                                    //        catch (Exception) { CurrentInfoAboutProgramm["Programm_Logo"] = ""; }

                                    //        CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        CurrentInfoAboutProgramm["Programm_Logo"] = itemFindAll.InnerHtml.Trim().ToString();
                                    //    }

                                    //    #endregion

                                    //    #region Определяем описание программы

                                    //    //Проверяем если другие текстовые параграфы в документе 

                                    //    try
                                    //    {
                                    //        string itemFindDescriptionTEMP = string.Empty;
                                    //        for (int z = 0; z < 5; z++)
                                    //        {
                                    //            var item_P = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p[" + (z + 2) + "]");
                                    //            if (item_P != null)
                                    //            {
                                    //                itemFindDescriptionTEMP += item_P.InnerHtml;
                                    //            }
                                    //            else { break; }
                                    //        }

                                    //        if (!string.IsNullOrEmpty(itemFindDescriptionTEMP))
                                    //        {
                                    //            CurrentInfoAboutProgramm["Programm_Description"] = itemFindDescriptionTEMP.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("\r\n", "").Trim();
                                    //        }
                                    //        else
                                    //        {
                                    //            string[] itemFindDescription = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p").InnerHtml.Replace(htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p/strong[1]").OuterHtml, "").ToString().Replace("&nbsp;", "").Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                    //            CurrentInfoAboutProgramm["Programm_Description"] = itemFindDescription[3].Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("\r\n", "").Trim();
                                    //        }

                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        CurrentInfoAboutProgramm["Programm_Description"] = itemFindAll.InnerHtml.Trim().ToString();
                                    //    }

                                    //    #endregion

                                    //    CurrentInfoAboutProgramm["Programm_HowCreate"] = "Web";

                                    //    dSet.Tables["TV_Programms_Table"].Rows.Add(CurrentInfoAboutProgramm);

                                    //    #endregion

                                    //    continue;
                                    //}

                                    //if (itemFindV1_b != null && itemFindV1_2_b == null)
                                    //{
                                    //    #region Полная информация про передачу

                                    //    CurrentInfoAboutProgramm["Programm_Parent_TV_Channel"] = Channel["UnicalCounterChannel"];
                                    //    CurrentInfoAboutProgramm["Programm_Name"] = control.InnerHtml.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim().ToString();

                                    //    #region Определяем жанр программы

                                    //    try
                                    //    {
                                    //        string[] itemFindGenre = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b").InnerHtml.Replace(itemFindV1_b.OuterHtml, "").Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                    //        CurrentInfoAboutProgramm["Programm_Genre"] = itemFindGenre[0].Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim();
                                    //    }
                                    //    catch (Exception) { CurrentInfoAboutProgramm["Programm_Genre"] = itemFindAll.InnerHtml.Trim().ToString(); }

                                    //    #endregion

                                    //    CurrentInfoAboutProgramm["Programm_DateOfAir"] = currentdate;
                                    //    CurrentInfoAboutProgramm["Programm_TimeOfAir"] = timeOfItemProgrammCollection[i].InnerHtml;

                                    //    #region Определяем режиссера программы

                                    //    try
                                    //    {
                                    //        string[] itemFindDirector = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']").InnerHtml.Replace(htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b").OuterHtml, "").Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                    //        CurrentInfoAboutProgramm["Programm_Director"] = itemFindDirector[0].Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Replace("\r\n", "").Replace("\t", "").Trim();
                                    //    }
                                    //    catch (Exception) { CurrentInfoAboutProgramm["Programm_Director"] = itemFindAll.InnerHtml.Trim().ToString(); }

                                    //    #endregion

                                    //    #region определение и загрузка файла изображения передачи

                                    //    try
                                    //    {
                                    //        Uri uri = new Uri("http://tvgid.ua" + itemFindV1_b.Attributes["src"].Value);
                                    //        string filename = string.Empty;

                                    //        try
                                    //        {
                                    //            filename = System.IO.Path.GetFileName(uri.LocalPath);
                                    //            DownloadRemoteImageFile("http://tvgid.ua" + itemFindV1_b.Attributes["src"].Value, RightPathFile() + "\\" + "LOGO_OF_CHANNEL" + "\\" + filename);
                                    //            CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //        }
                                    //        catch (Exception) { CurrentInfoAboutProgramm["Programm_Logo"] = ""; }

                                    //        CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        CurrentInfoAboutProgramm["Programm_Logo"] = itemFindAll.InnerHtml.Trim().ToString();
                                    //    }

                                    //    #endregion

                                    //    #region Определяем описание программы

                                    //    try
                                    //    {
                                    //        string itemFindDescriptionTEMP = string.Empty;
                                    //        for (int z = 0; z < 5; z++)
                                    //        {
                                    //            var item_P = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b[" + (z + 2) + "]");
                                    //            if (item_P != null)
                                    //            {
                                    //                itemFindDescriptionTEMP += item_P.InnerHtml;
                                    //            }
                                    //            else { break; }
                                    //        }

                                    //        if (!string.IsNullOrEmpty(itemFindDescriptionTEMP))
                                    //        {
                                    //            CurrentInfoAboutProgramm["Programm_Description"] = itemFindDescriptionTEMP.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("\r\n", "").Trim();
                                    //        }
                                    //        else
                                    //        {
                                    //            string[] itemFindDescription = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b").InnerHtml.Replace(htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b/img").OuterHtml, "").ToString().Replace("&nbsp;", "").Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                    //            CurrentInfoAboutProgramm["Programm_Description"] = itemFindDescription[3].Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("\r\n", "").Trim();
                                    //        }

                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        CurrentInfoAboutProgramm["Programm_Description"] = itemFindAll.InnerHtml.Trim().ToString();
                                    //    }

                                    //    #endregion

                                    //    CurrentInfoAboutProgramm["Programm_HowCreate"] = "Web";

                                    //    dSet.Tables["TV_Programms_Table"].Rows.Add(CurrentInfoAboutProgramm);

                                    //    #endregion

                                    //    continue;
                                    //}


                                    //#endregion

                                    //#endregion

                                    //#region Вариант содержимого №3

                                    ////Выборка информации V3
                                    //var itemFindV3 = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p/img");
                                    //var itemFindV3_b = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b/img");

                                    //if (itemFindV3 != null)
                                    //{
                                    //    CurrentInfoAboutProgramm["Programm_Parent_TV_Channel"] = Channel["UnicalCounterChannel"];
                                    //    CurrentInfoAboutProgramm["Programm_Name"] = control.InnerHtml.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim().ToString();
                                    //    CurrentInfoAboutProgramm["Programm_Genre"] = "";

                                    //    CurrentInfoAboutProgramm["Programm_DateOfAir"] = currentdate;
                                    //    CurrentInfoAboutProgramm["Programm_TimeOfAir"] = timeOfItemProgrammCollection[i].InnerHtml;
                                    //    CurrentInfoAboutProgramm["Programm_Director"] = "";

                                    //    #region определение и загрузка файла изображения передачи

                                    //    try
                                    //    {
                                    //        Uri uri = new Uri("http://tvgid.ua" + itemFindV3.Attributes["src"].Value);
                                    //        string filename = string.Empty;

                                    //        try
                                    //        {
                                    //            filename = System.IO.Path.GetFileName(uri.LocalPath);
                                    //            DownloadRemoteImageFile("http://tvgid.ua" + itemFindV3.Attributes["src"].Value, RightPathFile() + "\\" + "LOGO_OF_CHANNEL" + "\\" + filename);
                                    //            CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //        }
                                    //        catch (Exception) { CurrentInfoAboutProgramm["Programm_Logo"] = ""; }

                                    //        CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        CurrentInfoAboutProgramm["Programm_Logo"] = itemFindAll.InnerHtml.Trim().ToString();
                                    //    }

                                    //    #endregion

                                    //    #region Определяем описание программы

                                    //    try
                                    //    {
                                    //        string itemFindDescription = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p").InnerHtml.Replace(htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p/img").OuterHtml, "").Replace("<br>", "").Replace("<strong>", "").Replace("</strong>", "");
                                    //        CurrentInfoAboutProgramm["Programm_Description"] = itemFindDescription.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("\r\n", "").Replace("&nbsp;", " ").Trim();
                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        CurrentInfoAboutProgramm["Programm_Description"] = itemFindAll.InnerHtml.Trim().ToString();
                                    //    }

                                    //    #endregion

                                    //    CurrentInfoAboutProgramm["Programm_HowCreate"] = "Web";

                                    //    dSet.Tables["TV_Programms_Table"].Rows.Add(CurrentInfoAboutProgramm);

                                    //    continue;
                                    //}

                                    //if (itemFindV3_b != null)
                                    //{
                                    //    CurrentInfoAboutProgramm["Programm_Parent_TV_Channel"] = Channel["UnicalCounterChannel"];
                                    //    CurrentInfoAboutProgramm["Programm_Name"] = control.InnerHtml.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim().ToString();
                                    //    CurrentInfoAboutProgramm["Programm_Genre"] = "";

                                    //    CurrentInfoAboutProgramm["Programm_DateOfAir"] = currentdate;
                                    //    CurrentInfoAboutProgramm["Programm_TimeOfAir"] = timeOfItemProgrammCollection[i].InnerHtml;
                                    //    CurrentInfoAboutProgramm["Programm_Director"] = "";

                                    //    #region определение и загрузка файла изображения передачи

                                    //    try
                                    //    {
                                    //        Uri uri = new Uri("http://tvgid.ua" + itemFindV3_b.Attributes["src"].Value);
                                    //        string filename = string.Empty;

                                    //        try
                                    //        {
                                    //            filename = System.IO.Path.GetFileName(uri.LocalPath);
                                    //            DownloadRemoteImageFile("http://tvgid.ua" + itemFindV3_b.Attributes["src"].Value, RightPathFile() + "\\" + "LOGO_OF_CHANNEL" + "\\" + filename);
                                    //            CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //        }
                                    //        catch (Exception) { CurrentInfoAboutProgramm["Programm_Logo"] = ""; }

                                    //        CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        CurrentInfoAboutProgramm["Programm_Logo"] = itemFindAll.InnerHtml.Trim().ToString();
                                    //    }

                                    //    #endregion

                                    //    #region Определяем описание программы

                                    //    try
                                    //    {
                                    //        string itemFindDescription = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b").InnerHtml.Replace(htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b/img").OuterHtml, "").Replace("<br>", "").Replace("<strong>", "").Replace("</strong>", "");
                                    //        CurrentInfoAboutProgramm["Programm_Description"] = itemFindDescription.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("\r\n", "").Trim();
                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        CurrentInfoAboutProgramm["Programm_Description"] = itemFindAll.InnerHtml.Trim().ToString();
                                    //    }

                                    //    #endregion

                                    //    CurrentInfoAboutProgramm["Programm_HowCreate"] = "Web";

                                    //    dSet.Tables["TV_Programms_Table"].Rows.Add(CurrentInfoAboutProgramm);

                                    //    continue;
                                    //}


                                    //#endregion

                                    //#region Вариант содержимого №4

                                    ////Выборка информации V3
                                    //var itemFindV4 = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/img");

                                    //if (itemFindV4 != null)
                                    //{
                                    //    CurrentInfoAboutProgramm["Programm_Parent_TV_Channel"] = Channel["UnicalCounterChannel"];
                                    //    CurrentInfoAboutProgramm["Programm_Name"] = control.InnerHtml.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim().ToString();
                                    //    CurrentInfoAboutProgramm["Programm_Genre"] = "";

                                    //    CurrentInfoAboutProgramm["Programm_DateOfAir"] = currentdate;
                                    //    CurrentInfoAboutProgramm["Programm_TimeOfAir"] = timeOfItemProgrammCollection[i].InnerHtml;
                                    //    CurrentInfoAboutProgramm["Programm_Director"] = "";

                                    //    #region Определение и загрузка файла изображения передачи

                                    //    try
                                    //    {
                                    //        Uri uri = new Uri("http://tvgid.ua" + itemFindV4.Attributes["src"].Value);
                                    //        string filename = string.Empty;

                                    //        try
                                    //        {
                                    //            filename = System.IO.Path.GetFileName(uri.LocalPath);
                                    //            DownloadRemoteImageFile("http://tvgid.ua" + itemFindV4.Attributes["src"].Value, RightPathFile() + "\\" + "LOGO_OF_CHANNEL" + "\\" + filename);
                                    //            CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //        }
                                    //        catch (Exception) { CurrentInfoAboutProgramm["Programm_Logo"] = ""; }

                                    //        CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename;
                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        CurrentInfoAboutProgramm["Programm_Logo"] = itemFindAll.InnerHtml.Trim().ToString();
                                    //    }

                                    //    #endregion

                                    //    #region Определяем описание программы

                                    //    try
                                    //    {
                                    //        string itemFindDescription = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']").InnerHtml.Replace(htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/img").OuterHtml, "").Replace("<br>", "").Replace("<strong>", "").Replace("</strong>", "");
                                    //        CurrentInfoAboutProgramm["Programm_Description"] = itemFindDescription.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("\r\n", "").Replace("&nbsp;", " ").Trim();
                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        CurrentInfoAboutProgramm["Programm_Description"] = itemFindAll.InnerHtml.Trim().ToString();
                                    //    }

                                    //    #endregion

                                    //    CurrentInfoAboutProgramm["Programm_HowCreate"] = "Web";

                                    //    dSet.Tables["TV_Programms_Table"].Rows.Add(CurrentInfoAboutProgramm);

                                    //    continue;
                                    //}

                                    //#endregion

                                    //if (itemFind == null)
                                    //{
                                    //    #region Полная информация про передачу

                                    //    CurrentInfoAboutProgramm["Programm_Parent_TV_Channel"] = Channel["UnicalCounterChannel"];
                                    //    CurrentInfoAboutProgramm["Programm_Name"] = control.InnerHtml.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim().ToString();
                                    //    CurrentInfoAboutProgramm["Programm_Genre"] = Channel["UnicalCounterChannel"];
                                    //    CurrentInfoAboutProgramm["Programm_DateOfAir"] = currentdate;
                                    //    CurrentInfoAboutProgramm["Programm_TimeOfAir"] = timeOfItemProgrammCollection[i].InnerHtml;
                                    //    CurrentInfoAboutProgramm["Programm_Director"] = "Не определен";

                                    //    #region Загрузка файла изображения передачи

                                    //    var itemFind2 = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p/img");
                                    //    if (itemFind2 == null) { itemFind2 = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b/img"); }
                                    //    if (itemFind2 == null) { itemFind2 = htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p/b/img"); }

                                    //    Uri uri2 = new Uri("http://tvgid.ua" + itemFind2.Attributes["src"].Value);
                                    //    string filename2 = string.Empty;

                                    //    try
                                    //    {
                                    //        filename2 = System.IO.Path.GetFileName(uri2.LocalPath);
                                    //        DownloadRemoteImageFile("http://tvgid.ua" + itemFind.Attributes["src"].Value, RightPathFile() + "\\" + "LOGO_OF_CHANNEL" + "\\" + filename2);
                                    //        CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename2;
                                    //    }
                                    //    catch (Exception) { CurrentInfoAboutProgramm["Programm_Logo"] = ""; }
                                    //    CurrentInfoAboutProgramm["Programm_Logo"] = "LOGO_OF_CHANNEL\\" + filename2;

                                    //    #endregion

                                    //    #region Определяем описание программы

                                    //    string str = string.Empty;

                                    //    try
                                    //    {
                                    //        str = (string)htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/p").InnerHtml.Replace(itemFind2.OuterHtml, "").Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("\r\n", "").Replace("<br>", "").Trim().ToString();
                                    //    }
                                    //    catch (Exception)
                                    //    {
                                    //        try
                                    //        {
                                    //            str = (string)htmlDocItem.DocumentNode.SelectSingleNode("//div[@id='ncnt']/b").InnerHtml.Replace(itemFind2.OuterHtml, "").Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("\r\n", "").Replace("<br>", "").Trim().ToString();
                                    //        }
                                    //        catch (Exception) { }
                                    //    }

                                    //    CurrentInfoAboutProgramm["Programm_Description"] = str;

                                    //    #endregion

                                    //    CurrentInfoAboutProgramm["Programm_HowCreate"] = "Web";

                                    //    dSet.Tables["TV_Programms_Table"].Rows.Add(CurrentInfoAboutProgramm);

                                    //    #endregion
                                    //}

                                    #endregion

                                }
                                else
                                {
                                    #region Нет информации про программу

                                    CurrentInfoAboutProgramm["Programm_Parent_TV_Channel"] = Channel["UnicalCounterChannel"];
                                    CurrentInfoAboutProgramm["Programm_Name"] = control.InnerHtml.ToString();
                                    CurrentInfoAboutProgramm["Programm_Genre"] = string.Empty;
                                    CurrentInfoAboutProgramm["Programm_DateOfAir"] = currentdate;
                                    CurrentInfoAboutProgramm["Programm_TimeOfAir"] = timeOfItemProgrammCollection[i].InnerHtml;
                                    CurrentInfoAboutProgramm["Programm_Director"] = string.Empty;
                                    CurrentInfoAboutProgramm["Programm_Logo"] = string.Empty;
                                    CurrentInfoAboutProgramm["Programm_Description"] = string.Empty;
                                    CurrentInfoAboutProgramm["Programm_HowCreate"] = "Web";

                                    dSet.Tables["TV_Programms_Table"].Rows.Add(CurrentInfoAboutProgramm);

                                    #endregion
                                }

                            }
                        }
                        else
                        {
                            #region Если нет информации про программу

                            CurrentInfoAboutProgramm["Programm_Parent_TV_Channel"] = Channel["UnicalCounterChannel"];
                            CurrentInfoAboutProgramm["Programm_Name"] = control.InnerHtml.Replace("&hellip;", "...").Replace("&mdash;", "-").Replace("&quot;", "\"").Replace("&nbsp;", " ").Trim().ToString();
                            CurrentInfoAboutProgramm["Programm_Genre"] = string.Empty;
                            CurrentInfoAboutProgramm["Programm_DateOfAir"] = currentdate;
                            CurrentInfoAboutProgramm["Programm_TimeOfAir"] = timeOfItemProgrammCollection[i].InnerHtml;
                            CurrentInfoAboutProgramm["Programm_Director"] = string.Empty;
                            CurrentInfoAboutProgramm["Programm_Logo"] = string.Empty;
                            CurrentInfoAboutProgramm["Programm_Description"] = string.Empty;
                            CurrentInfoAboutProgramm["Programm_HowCreate"] = "Web";

                            dSet.Tables["TV_Programms_Table"].Rows.Add(CurrentInfoAboutProgramm);

                            #endregion
                        }

                        #endregion
                    }

                    Update_dSet(dSet.Tables["TV_Programms_Table"]);
                }
            }

            #endregion

            #endregion
        }


        #region Метод: Корректировка адресса, с которого запускается приложение

        public string RightPathFile()
        {
            string str = Convert.ToString(Application.StartupPath[0]);
            return str.ToUpper() + Application.StartupPath.Substring(1, Application.StartupPath.Length - 1);
        }

        #endregion

        #region Метод: Обновление таблицы с талеканалами 

        private void Update_dSet(DataTable Table_For_Update)
        {
            #region Обновить в базе данных информацию по передачам

            adapter.SelectCommand.CommandText = "SELECT * FROM " + Table_For_Update.TableName;
            OleDbCommandBuilder bilderObjectsCommands = new OleDbCommandBuilder(adapter);
            adapter.DeleteCommand = bilderObjectsCommands.GetDeleteCommand();
            adapter.UpdateCommand = bilderObjectsCommands.GetUpdateCommand();
            adapter.InsertCommand = bilderObjectsCommands.GetInsertCommand();
            adapter.Update(Table_For_Update);
            Table_For_Update.AcceptChanges();

            #endregion
        }

        #endregion

        #region Метод: Очистка периода от телепередач 

        public void ClearProgrammsAboutChannel(string Programm_Parent_TV_Channel, DateTime startInterval, DateTime EndInterval)
        {
            DataView dView = new DataView(dSet.Tables["TV_Programms_Table"]);
            dView.RowFilter = "(Programm_Parent_TV_Channel=" + Programm_Parent_TV_Channel + ") AND (" + "Programm_DateOfAir >=" + getDateTimeForFind(startInterval, "00:00") + " AND " + "Programm_DateOfAir <=" + getDateTimeForFind(EndInterval, "23:59") + ")" + " AND " + "(Programm_HowCreate ='" + "Web" + "')";

            #region Чистим список программ

            if (dView.Count > 0)
            {
                foreach (DataRowView row in dView) { row.Delete(); }
                Update_dSet(dSet.Tables["TV_Programms_Table"]);
                dSet.AcceptChanges();
            }

            #endregion
        }

        #endregion

        #region Метод: Преобразователь даты 

        string getDateTimeForFind(DateTime Value, string Time)
        {
            String TEMP = "#" + Value.Month.ToString() + "/" + Value.Day.ToString() + "/" + Value.Year.ToString() + " " + Time + ":00" + "#";
            return TEMP;
        }

        #endregion

        #region Метод: Загружает изображение с удаленного ресурса 

        private static bool DownloadRemoteImageFile(string uri, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception)
            {
                return false;
            }

            // Check that the remote file was found. The ContentType
            // check is performed since a request for a non-existent
            // image file might be redirected to a 404-page, which would
            // yield the StatusCode "OK", even though the image was not
            // found.
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {

                // if the remote file was found, download it
                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = File.OpenWrite(fileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }
                return true;
            }
            else return false;
        }

        #endregion

        #region Метод:

        #endregion

        #endregion

        #region Метод: Корректировка таблицы обновления списка каналов 

        /// <summary>
        /// Метод: Корректировка таблицы обновления списка каналов
        /// </summary>
        public void UpdateTableOfUpdating(int CurrentChannel, DateTime DayOfUpdate, string Status)
        {
            if (tableWithChannelInfoUpdate.Rows.Count>0)
            {
                for (int i = 0; i < tableWithChannelInfoUpdate.Rows.Count; i++)
                {
                    if (tableWithChannelInfoUpdate.Rows[i]["UnicalCounterProgramm"].ToString()==CurrentChannel.ToString())
                    {
                        tableWithChannelInfoUpdate.Rows[i]["DateOfUpdateChannel"] = DayOfUpdate;
                        tableWithChannelInfoUpdate.Rows[i]["StateOfUpdate"] = Status;
                    }               
                }    
            }
        }

        
        #endregion

        #region

        #endregion

        #endregion
    }
}

        
