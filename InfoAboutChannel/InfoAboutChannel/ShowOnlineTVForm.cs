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
    public partial class ShowOnlineTVForm : Form
    {
        #region Переменные

        //Ссылка на веб сайт онлайн трансляции 
        string URLOnlineString;

        #endregion
        
        #region Конструктор 

        public ShowOnlineTVForm(string URL_Online)
        {
            InitializeComponent();
            URLOnlineString = URL_Online.ToString();
        }

        #endregion

        #region События
        
        #region Событие: Изменение состояния окна 

        private void ChangeFormStateButton_Click(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized: this.WindowState = FormWindowState.Normal; break;
                case FormWindowState.Normal: this.WindowState = FormWindowState.Maximized; break;
            }
        }

        #endregion

        #endregion


        #region Методы

        #region Метод: Обработка адреса и переход по ссылке 

        private void Navigate(String address)
        {
            if (String.IsNullOrEmpty(address)) return;
            if (address.Equals("about:blank")) return;
            if (!address.StartsWith("http://") &&
                !address.StartsWith("https://"))
            {
                address = "http://" + address;
            }
            try
            {
                webBrowser1.Navigate(new Uri(address));
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }

        #endregion

        private void ShowOnlineTVForm_Shown(object sender, EventArgs e)
        {
            #region Показ ссылки на форме

            AdressOfOnlinePage.ReadOnly = false;
            string URLCOntrol = URLOnlineString.ToString();
            if (!URLOnlineString.StartsWith("http://") &&
                !URLOnlineString.StartsWith("https://"))
            {
                URLCOntrol = "http://" + URLOnlineString.ToString();
            }
            AdressOfOnlinePage.Text = URLCOntrol.Trim();
            AdressOfOnlinePage.ReadOnly = true;

            #endregion


            Navigate(URLOnlineString);
        }

        #endregion

        #region Метод: 

        private void GoMainPage_Click(object sender, EventArgs e)
        {
            Navigate(URLOnlineString);
        }

        #endregion

        #region Метод:

        #endregion
    }
}
