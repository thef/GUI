using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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

namespace lagerking
{
    public class Commands : ObservableCollection<Product>, INotifyPropertyChanged
    {
        LagerkingDbContext _db = new LagerkingDbContext();
        public List<Product> _products { get; set; }
        Clock clock = new Clock();
        string filename = "";
       

        public Commands()
        {
            
            _products = _db.products.ToList();

            
            foreach (var product in _products)
            {
                Add(product);
            }
            clock.Opdater();
           
        }

        #region DBLagerking properties

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged();
                //OnPropertyChanged("Name");
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged();
                //OnPropertyChanged("Description");
            }
        }

        private Decimal _price;
        public Decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyPropertyChanged();
                //OnPropertyChanged("Price");
            }
        }

        private int _stock;
        public int Stock
        {
            get
            {
                return _stock;
            }
            set
            {
                _stock = value;
                NotifyPropertyChanged();
                //OnPropertyChanged("Stock");
            }
        }
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                NotifyPropertyChanged();
                //OnPropertyChanged("ID");
            }
        }

        private string _department;
        public string Department
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
                NotifyPropertyChanged();

            }
        }

        #endregion






        #region Commands

       

        ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(AddVare)); }
        }

        private void AddVare()
        {
            Product product = new Product();
            
            CurrentAfdelingIndex = 0;
            ProduktIndex newFunc = new ProduktIndex();

            product.Name = Name;
            product.Price = Price;
            product.Stock = Stock;
            product.Description = "Description missing";
            product.Department = Department;
            Add(product);
            _db.products.Add(product);
            // Show what the error is if SaveChanges(); fail.
            try
            {
                _db.SaveChanges();

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            //Add(newFunc);
            //CurrentVarer = newFunc;
            CurrentVarer = product;
            MediatorImpl.NotifyColleagues("Add", true);

        }


        ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(DeleteVare, DeleteVare_CanExecute)); }
        }

        private void DeleteVare()
        {
            CurrentVarer.Name = Name;
            CurrentVarer.Description = Description;
            CurrentVarer.Price = Price;
            CurrentVarer.Stock = Stock;
            //CurrentVarer.Id = Id;

            _db.products.Remove(CurrentVarer);

            try
            {
                _db.SaveChanges();

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

            Remove(CurrentVarer);
            MediatorImpl.NotifyColleagues("Del", true);
        }

        private bool DeleteVare_CanExecute()
        {
            if (Count > 0 && CurrentIndex >= 0)
                return true;
            else
                return false;
        }
        ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                return _nextCommand ?? (_nextCommand = new RelayCommand(
                    () => ++CurrentIndex,
                    () => CurrentIndex < (Count - 1)));
            }
        }

        ICommand _PreviusCommand;
        public ICommand PreviusCommand
        {
            get { return _PreviusCommand ?? (_PreviusCommand = new RelayCommand(PreviusCommandExecute, PreviusCommandCanExecute)); }
        }

        private void PreviusCommandExecute()
        {
            if (CurrentIndex > 0)
                --CurrentIndex;
        }

        private bool PreviusCommandCanExecute()
        {
            if (CurrentIndex > 0)
                return true;
            else
                return false;
        }

        ICommand _SaveAsCommand;
        public ICommand SaveAsCommand
        {
            get { return _SaveAsCommand ?? (_SaveAsCommand = new RelayCommand<string>(SaveAsCommand_Execute)); }
        }

        private void SaveAsCommand_Execute(string argFilename)
        {
            if (argFilename == "")
            {
                MessageBox.Show("Du skal skrive et fil navn før du gemmer!", "UGYLDIG!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                filename = argFilename;
                SaveFileCommand_Execute();
            }
        }

        ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get { return _SaveCommand ?? (_SaveCommand = new RelayCommand(SaveFileCommand_Execute, SaveFileCommand_CanExecute)); }
        }

        private void SaveFileCommand_Execute()
        {
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(Commands));
            TextWriter writer = new StreamWriter(filename);
            // Serialize all the agents.
            serializer.Serialize(writer, this);
            writer.Close();
        }

        private bool SaveFileCommand_CanExecute()
        {
            return (filename != "") && (Count > 0);
        }

        #endregion 
       
        
        
        #region Properties

        int currentIndex = -1;

        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                if (currentIndex != value)
                {
                    currentIndex = value;
                    NotifyPropertyChanged();
                }
            }
        }

        Product currentVarer = null;

        public Product CurrentVarer
        {
            get { return currentVarer; }
            set
            {
                if (currentVarer != value)
                {
                    currentVarer = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        ICommand _NewFileCommand;
        public ICommand NewFileCommand
        {
            get { return _NewFileCommand ?? (_NewFileCommand = new RelayCommand(NewFileCommand_Execute)); }
        }

        private void NewFileCommand_Execute()
        {
            MessageBoxResult res = MessageBox.Show("Er du sikker på at du vil starte en ny fil?", "OBS! Alt data bliver slettet",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                Clear();
                filename = "";
            }
        }

        ICommand _OpenFileCommand;
        public ICommand OpenFileCommand
        {
            get { return _OpenFileCommand ?? (_OpenFileCommand = new RelayCommand<string>(OpenFileCommand_Execute)); }
        }

        private void OpenFileCommand_Execute(string argFilename)
        {
            if (argFilename == "")
            {

                MessageBox.Show("Du skal skrive et filnavn på tekstboksen", "Kan ikke gemme filen",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                filename = argFilename;
                Commands tempFunc = new Commands();

                // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
                XmlSerializer serializer = new XmlSerializer(typeof(Commands));
                try
                {
                    TextReader reader = new StreamReader(filename);
                    // Deserialize all the agents.
                    tempFunc = (Commands)serializer.Deserialize(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Kan ikke gemme filen", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Vi indsætter varerindex ind igen 
                Clear();
                foreach (var varer in tempFunc)
                    Add(varer);
            }
        }

        ICommand _CloseAppCommand;
        public ICommand CloseAppCommand
        {
            get { return _CloseAppCommand ?? (_CloseAppCommand = new RelayCommand(CloseCommand_Execute)); }
        }

        private void CloseCommand_Execute()
        {
            Application.Current.MainWindow.Close();
        }


        public IReadOnlyCollection<string> FilterSpecialities
        {
            get
            {
                ObservableCollection<string> result = new ObservableCollection<string>();
                result.Add("All");
                foreach (var s in new Afdelinger())
                    result.Add(s);
                return result;
            }
        }

        int currentAfdelingIndex = 0;

        public int CurrentAfdelingIndex
        {
            get { return currentAfdelingIndex; }
            set
            {
                if (currentAfdelingIndex != value)
                {
                    ICollectionView cv = CollectionViewSource.GetDefaultView(this);
                    currentAfdelingIndex = value;
                    if (currentAfdelingIndex == 0)
                        cv.Filter = null;
                   
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public new event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
