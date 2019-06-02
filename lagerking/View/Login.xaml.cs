using System.Windows;
using System.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Data;
using lagerking.View;
using System.Runtime.CompilerServices;

namespace lagerking.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
  
        

 

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            string username = "";
            string passwd = "";


            if (username == "" && passwd == "")
            {
                mw.Show();
                mw.Close();

            }

            else
            {
                MessageBox.Show("Wrong Input", "Login Window");
                SystemSounds.Exclamation.Play();
            }
        }
    }



}
