using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace myWpfApp
{
    class Commands : ObservableCollection<Location>, INotifyPropertyChanged
    {
        private Location _currentLocation;

        public Location CurrenteLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; NotifyPropertyChanged(); }
        }

        private string _navn;

        public string Navn
        {
            get { return _navn; }
            set { _navn = value; NotifyPropertyChanged(); }
        }

        private string _vej;

        public string Vej
        {
            get { return _vej; }
            set { _vej = value; NotifyPropertyChanged(); }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }

        private int _vejnummer;

        public int Vejnummer
        {
            get { return _vejnummer; }
            set { _vejnummer = value; NotifyPropertyChanged(); }
        }

        private int _Postnummber;

        public int Postnummber
        {
            get { return _Postnummber; }
            set { _Postnummber = value; NotifyPropertyChanged(); }
        }

        private string _By;

        public string By
        {
            get { return _By; }
            set { _By = value; NotifyPropertyChanged(); }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged(); }
        }

        private string _amount;

        public string Amount
        {
            get { return _amount; }
            set { _amount = value; NotifyPropertyChanged(); }
        }

        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; NotifyPropertyChanged(); }
        }

        ICommand _openFile;

        public ICommand OpenFile
        {
            get { return _openFile ?? (_openFile = new RelayCommand(OpenFileFunction)); }
        }

        public void OpenFileFunction()
        {
            if (FileName == null || FileName == "")
            {
                FileName = "UnNamed.txt";
            }
            StreamReader streamReader = new StreamReader(FileName);

            while (true)
            {
                
                var stillTextToRead = streamReader.ReadLine();
                if (stillTextToRead != null)
                {

                    stillTextToRead = stillTextToRead.Replace("Location Id: ", "");
                    int id = Int32.Parse(stillTextToRead);

                    var locationName = streamReader.ReadLine();
                    var name = locationName.Replace("Location Name: ", "");

                    var locationRoad = streamReader.ReadLine();
                    var road = locationRoad.Replace("Location Road: ", "");

                    var locationRoadnumber = streamReader.ReadLine();
                    locationRoadnumber = locationRoadnumber.Replace("Location Roadnumber: ", "");
                    int roadnumber = Int32.Parse(locationRoadnumber);

                    var locationZipcode = streamReader.ReadLine();
                    locationZipcode = locationRoadnumber.Replace("Location Zipcode: ", "");
                    int zipcode = Int32.Parse(locationZipcode);

                    var locationCity = streamReader.ReadLine();
                    var city = locationRoad.Replace("Location City: ", "");

                    Location newLocation = new Location();
                    newLocation.Id = id;
                    newLocation.Navn = name;
                    newLocation.Vej = road;
                    newLocation.Vejnummer = roadnumber;
                    newLocation.Postnummer = zipcode;
                    newLocation.By = city;
                    newLocation.Trees = new List<Tree>();

                    while (true)
                    {
                        //Read every Tree in Location
                        var locationTreeEnded = streamReader.ReadLine();
                        if (locationTreeEnded.Contains("-"))
                        {
                            break;
                        }
                        else
                        {
                            var type = locationTreeEnded.Replace("Tree Type: ", "");

                            var locationTreeAmount = streamReader.ReadLine();
                            locationTreeAmount = locationRoadnumber.Replace("Tree Amount: ", "");
                            int amount = Int32.Parse(locationTreeAmount);

                            Tree newTree = new Tree();

                            newTree.Type = Type;
                            newTree.Amount = Amount;

                            newLocation.Trees.Add(newTree);
                        }
                    }
                    Add(newLocation);
                    CurrenteLocation = newLocation;
                }
            }

        }

        ICommand _saveFile;

        public ICommand SaveFile
        {
            get { return _saveFile ?? (_saveFile = new RelayCommand(SaveFileFunction)); }
        }

        public void SaveFileFunction()
        {
            if (FileName == null || FileName == "")
            {
                FileName = "UnNamed.txt";
            }

            StreamWriter streamWriter = new StreamWriter(FileName);

            foreach (var location in Items)
            {
                streamWriter.WriteLine("Location Id: " + location.Id);
                streamWriter.WriteLine("Location Name: " + location.Navn);
                streamWriter.WriteLine("Location Road: " + location.Vej);
                streamWriter.WriteLine("Location Roadnumber: " + location.Vejnummer);
                streamWriter.WriteLine("Location Zipcode: " + location.Postnummer);
                streamWriter.WriteLine("Location City: " + location.By);

                foreach (var tree in location.Trees)
                {
                    streamWriter.WriteLine("Tree Type: " + tree.Type);
                    streamWriter.WriteLine("Tree Amount: " + tree.Amount);
                }

                streamWriter.WriteLine("--------------------------------------------------");
            }

            streamWriter.Close();

        }

        ICommand _addTree;

        public ICommand AddTree
        {
            get { return _addTree ?? (_addTree = new RelayCommand(AddTreeFunction)); }
        }

        public void AddTreeFunction()
        {
            var location = CurrenteLocation;
            Remove(CurrenteLocation);

            Tree newTree = new Tree();

            newTree.Type = Type;
            newTree.Amount = Amount;
            
            location.Trees.Add(newTree);
            Add(location);
            CurrenteLocation = location;
        }

        private ObservableCollection<Tree> _trees = new ObservableCollection<Tree>();

        public ObservableCollection<Tree> Trees
        {
            get { return _trees;  }
            set { _trees = value; NotifyPropertyChanged(); }
        }

        ICommand _outputTree;

        public ICommand OutputTree
        {
            get { return _outputTree ?? (_outputTree = new RelayCommand(OutputTreeFunction)); }
        }

        public void OutputTreeFunction()
        {
            Trees.Clear();
            foreach (var tree in CurrenteLocation.Trees)
            {
                Trees.Add(tree);
            }
        }


        ICommand _addLocation;

        public ICommand AddLocation
        {
            get { return _addLocation ?? (_addLocation = new RelayCommand(AddLocationFunction)); }
        }

        ICommand _searchLocation;

        public ICommand SearchLocation
        {
            get { return _searchLocation ?? (_searchLocation = new RelayCommand(SearchLocationFunction)); }
        }

        public void SearchLocationFunction()
        {
            foreach (var location in Items)
            {
                if (location.Navn == Navn)
                {
                    CurrenteLocation = location;
                }
            }
        }



        private int currentId = 0;

        public void AddLocationFunction()
        {
            Location newLocation = new Location();
            newLocation.Id = currentId;
            currentId++;
            newLocation.Navn = Navn;
            newLocation.Vej = Vej;
            newLocation.Vejnummer = Vejnummer;
            newLocation.Postnummer = Postnummber;
            newLocation.By = By;
            newLocation.Trees = new List<Tree>();
            Add(newLocation);
            CurrenteLocation = newLocation;
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
