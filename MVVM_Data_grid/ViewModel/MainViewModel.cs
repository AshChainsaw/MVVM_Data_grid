using MVVM_Data_grid.Model;
using MVVM_Data_grid.Utility;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Input;
using System.ComponentModel;



namespace MVVM_Data_grid.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Person> Persons { get; set; }

        public RelayCommand AddCommand => new RelayCommand(execute => AddPerson());
        public RelayCommand DeleteCommand => new RelayCommand(execute => RemovePerson(), canExecute => SelectedItem != null);
        public RelayCommand DownCommand => new RelayCommand(execute => DownPerson(), canExecute => SelectedItem != null);
        public RelayCommand UpCommand => new RelayCommand(execute => UpPerson(), canExecute => SelectedItem != null);
        public RelayCommand InsertCommand => new RelayCommand(execute => InsertPerson(), canExecute => SelectedItem != null);
        public RelayCommand ClearCommand => new RelayCommand(execute => ClearPerson(), canExecute => SelectedItem != null);
        public RelayCommand SerializeCommand => new RelayCommand(execute => SerializeList());
        public RelayCommand CloseCommand => new RelayCommand(execute => CloseApp());
        
        
      

        

        public MainViewModel()
        {
            Persons = new ObservableCollection<Person>();

            
        }

        private Person selectedItem;

        private int TabIndex;

        public Person SelectedItem
        {
            get
            { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        public WindowState WindowS { get; private set; }

        private void AddPerson()
        {
            Persons.Add(new Person
            {
                Name = "",
                Surname = "",
                Function = "",
                Logo = ""
            });
        }
        private void RemovePerson()
        {
            Persons.Remove(SelectedItem);
        }
        private void DownPerson()
        {
            int currentIndex = Persons.IndexOf(selectedItem);
            if (currentIndex != -1 && currentIndex < Persons.Count - 1)
                Persons.Move(currentIndex, currentIndex + 1);
        }
        private void UpPerson()
        {
            var selected = SelectedItem;
            if (selected == null)
            {
                Persons.Move(TabIndex, 0);
            }
            int currentindex = Persons.IndexOf(selectedItem);
            if (currentindex > 0)
            {
                Persons.Move(currentindex, currentindex - 1);
            }
        }
        private void InsertPerson()
        {
            //var selected = SelectedItem;
            int currentindex = Persons.IndexOf(selectedItem);
            Persons.Insert(currentindex + 1, new Person { Name = "", Surname = "", Function = "", Logo = "" });
        }

        private void ClearPerson()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Czy na pewno chcesz wyczyścić Play Liste? Po wyczyszczeniu utracisz wszystkie dane. Będzie to katastrofalny błąd Play Listy", "Czyszczenie Listy ", MessageBoxButton.YesNoCancel);
            if (MessageBoxResult.Yes == messageBoxResult)
            {
                Persons.Clear();
            }
        }
        public void SerializeList()
        {
            var data = Persons;
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            string realtivepath = "Serialize";
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, realtivepath);
            string filePath = Path.Combine(folderPath, "PersonList.json");
            File.WriteAllText(filePath, json);
        }
        public void DeserializeList()
        {
            string realtivepath = "Serialize";
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, realtivepath);
            string filePath = Path.Combine(folderPath, "PersonList.json");
            string json = File.ReadAllText(filePath);
            Persons = JsonConvert.DeserializeObject<ObservableCollection<Person>>(json);
        }

        public void CloseApp()
        {
            SerializeList();
            Application.Current.Shutdown();
        }

        
        

        
        
    }
}
