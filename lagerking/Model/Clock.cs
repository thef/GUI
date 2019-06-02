using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Windows.Data;
using lagerking.View;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using lagerking.Model;

namespace lagerking.Model
{
    class Clock : INotifyPropertyChanged
    {
        public Clock()
        {
           Opdater();
        }
        
        string dato;
        string tid;

        public void Opdater()
        {
            Dato = DateTime.Now.ToLongDateString();
            Tid = DateTime.Now.ToLongTimeString();
        }

        public string Dato
        {
            get { return dato; }
            private set
            {
                if (dato != value)
                {
                    dato = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public string Tid
        {
            get { return tid; }
            private set
            {
                if (tid != value)
                {
                    tid = value;
                    NotifyPropertyChanged();

                }
            }
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
