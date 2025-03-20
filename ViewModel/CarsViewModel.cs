using GIBDD.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using GIBDD.View;

namespace GIBDD.ViewModel
{
    public class CarsViewModel : ViewModels
    {
        public bool IsAdmin
        {
            get => App.IsAdmin;
            set
            {
                App.IsAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        private Visibility visibility;
        public Visibility Visibility
        {
            get
            {
                if (IsAdmin) return Visibility.Visible;
                else return Visibility.Collapsed;
            }
        }

        private bool FilterVehiclesPredicate(object item)
        {
            if (item is VINNumber vehicle)
            {
                // Функция для проверки наличия всех символов из запроса в строке
                bool ContainsAllCharacters(string source, string query)
                {
                    if (string.IsNullOrEmpty(query)) return true;

                    foreach (var character in query.ToLower())
                    {
                        if (!source.ToLower().Contains(character))
                            return false;
                    }
                    return true;
                }

                return (string.IsNullOrEmpty(SearchGosNumber) || ContainsAllCharacters(vehicle.RegistrationNumber, SearchGosNumber)) &&
                       (string.IsNullOrEmpty(SearchVIN) || ContainsAllCharacters(vehicle.Number, SearchVIN));
            }
            return false;
        }

        private void FilterVehicles()
        {
            CollectionView.Refresh();
        }

        private string _searchGosNumber;
        public string SearchGosNumber
        {
            get => _searchGosNumber;
            set
            {
                _searchGosNumber = value;
                FilterVehicles();
                OnPropertyChanged(nameof(SearchGosNumber));
            }
        }

        private string _searchVIN;
        public string SearchVIN
        {
            get => _searchVIN;
            set
            {
                _searchVIN = value;
                FilterVehicles();
                OnPropertyChanged(nameof(SearchVIN));
            }
        }

        public bool IsDriver
        {
            get => App.IsDriver;
            set
            {
                App.IsDriver = value;
                OnPropertyChanged(nameof(IsDriver));
            }
        }

        private User user;
        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private ObservableCollection<VINNumber> cars = new();
        public ObservableCollection<VINNumber> Cars
        {
            get => cars;
            set
            {
                cars = value;
                OnPropertyChanged(nameof(Cars));
            }
        }

        public CollectionViewSource ViewSource { get; set; } = new();
        public ICollectionView CollectionView => ViewSource.View;

        public CarsViewModel()
        {
            App.context.Drivers.Load();
            App.context.DriverLicenses.Load();
            App.context.EngineNumbers.Load();
            App.context.Users.Load();
            App.context.VINNumbers.Load();
            Cars = new ObservableCollection<VINNumber>(App.context.VINNumbers.ToList());
            ViewSource.Source = Cars;
            CollectionView.Filter = FilterVehiclesPredicate;
        }

        private VINNumber selectedCar;
        public VINNumber SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        private RelayCommand reset;
        public RelayCommand Reset
        {
            get
            {
                return reset ?? (reset = new(obj =>
                {
                    SearchGosNumber = string.Empty;
                    SearchVIN = string.Empty;

                    FilterVehicles();
                },
                obj => true));
            }
        }

        private RelayCommand add;
        public RelayCommand Add
        {
            get
            {
                return add ?? (add = new(obj =>
                {
                    AddCarViewModel vm = new();
                    AddCar windowAddCar = new()
                    {
                        DataContext = vm
                    };
                    if (windowAddCar.ShowDialog() == true)
                    {
                        if (App.context.VINNumbers.Any(car => car.EngineNumber == vm.EngineNumber))
                        {
                            var result = MessageBox.Show("Автомобиль с указанным двигателем уже существует. Если вы продолжите, это заменит существующий автомобиль. Продолжить?", "Предупреждение", MessageBoxButton.YesNo);

                            if (result == MessageBoxResult.No)
                            {
                                return;
                            }

                            VINNumber deletable;
                            deletable = App.context.VINNumbers.Where(car => vm.EngineNumber == car.EngineNumber).FirstOrDefault();
                            Cars.Remove(deletable);
                        }
                        App.context.VINNumbers.Add(vm.Car);
                        Cars.Add(vm.Car);
                        App.context.SaveChanges();

                        CollectionView.Refresh();
                    }
                },
                obj => IsAdmin));
            }
        }

        private RelayCommand delete;
        public RelayCommand Delete
        {
            get
            {
                return delete ?? (delete = new(obj =>
                {
                    App.context.Remove(SelectedCar);
                    Cars.Remove(SelectedCar);
                    App.context.SaveChanges();
                },
                obj => SelectedCar != null && IsAdmin));
            }
        }

        private RelayCommand watch;
        public RelayCommand Watch
        {
            get
            {
                return watch ?? (watch = new(obj =>
                {
                    CarPageViewModel carPageViewModel = new()
                    {
                        Car = App.context.VINNumbers.Where(a => SelectedCar == a).First(),
                        CurrentUser = User
                    };
                    CarPage carPage = new()
                    {
                        DataContext = carPageViewModel
                    };
                    carPage.Show();
                },
                obj => SelectedCar != null));
            }
        }

        private RelayCommand loginAdmin;
        public RelayCommand LoginAdmin
        {
            get
            {
                return loginAdmin ?? (loginAdmin = new(obj =>
                {
                    AdminLoginWindow adminLoginWindow = new();
                    if (adminLoginWindow.ShowDialog() == true)
                    {
                        IsAdmin = true;
                    }
                    OnPropertyChanged(nameof(IsAdmin));
                    OnPropertyChanged(nameof(Visibility));
                },
                obj => true));
            }
        }

        private RelayCommand authorization;
        public RelayCommand Authorization
        {
            get
            {
                return authorization ?? (authorization = new(obj =>
                {
                    if (IsDriver == true)
                    {
                        UserCabinet userCabinet = new();
                        userCabinet.DataContext = new UserCabinetViewModel(User);
                        userCabinet.Show();
                        return;
                    }
                    View.Authorization authorization = new();
                    if (authorization.ShowDialog() == true)
                    {
                        IsDriver = true;
                        User = authorization.User;
                        UserCabinet userCabinet = new();
                        userCabinet.DataContext = new UserCabinetViewModel(User);
                        userCabinet.Show();
                    }
                },
                obj => true));
            }
        }
    }
}