using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Threading;
using System.ComponentModel;
using lagerking.Model;

namespace lagerking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        private Clock clock = new Clock();
        

        public MainWindow()
        {
            InitializeComponent();
            sbiClock.DataContext = clock;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(tiktid);
            timer.Start();

        }

        void tiktid(object sender, EventArgs e)
        {
            clock.Opdater();
        }


        private void SortOrderCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)    
        {
            ComboBoxItem combobox = e.AddedItems[0] as ComboBoxItem;
            string sortere;
            if (combobox != null)
            {
                
                sortere = combobox.Tag.ToString();

                SortDescription sortereDesc = new SortDescription(sortere, ListSortDirection.Ascending);
                ICollectionView cv = CollectionViewSource.GetDefaultView(DataContext);
                if (cv != null)
                {
                    //Cleare menuen først
                    cv.SortDescriptions.Clear();
                    if (sortere != "None")
                        cv.SortDescriptions.Add(sortereDesc);
                }
                

            }
        }

        
    }
}
