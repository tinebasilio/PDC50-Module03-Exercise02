using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module02Exercise01.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Module02Exercise01.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        // role of ViewModel
        private Employee _employee;
        private string _fullName; 

        public EmployeeViewModel()
        {
            // Initialize a sample employee model. Coordination with Model
            _employee = new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                Position = "Manager"
            };

            // UI Thread Management
            LoadEmployeeDataCommand = new Command(async () => await LoadEmployeeDataAsync());

            // Collections
            Employees = new ObservableCollection<Employee>
            {
                new Employee {FirstName="Peter", LastName="Song", Position="DevOps Engineer", Department="IT", isActive=true},
                new Employee {FirstName="Joe", LastName="Smith", Position="Technical Specialist", Department="IT", isActive=true},
                new Employee {FirstName="Sam", LastName="Yu", Position="UI/UX Designer", Department="IT", isActive=true},
                new Employee {FirstName="Ashley", LastName="Kim", Position="Software Engineer", Department="IT", isActive=true},
                new Employee {FirstName="Juan", LastName="Smith", Position="Information Security Analyst", Department="IT", isActive=false}
            };

        }

        // Setting collections in public
        public ObservableCollection<Employee> Employees { get; set; }

        // Two-way Data Binding and Conversion
        public string FullName
        {
            get => _fullName;
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }
        // UI Thread Management
        public ICommand LoadEmployeeDataCommand { get; }

        private async Task LoadEmployeeDataAsync()
        {
            await Task.Delay(1000); // I/O operation
            FullName = $"Manager: {_employee.FirstName} {_employee.LastName}"; // Data Conversion
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }

}
