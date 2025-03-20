using GIBDD.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GIBDD.ViewModel
{
    public class CarPageViewModel : ViewModels
    {
        private VINNumber car;
        public VINNumber Car
        {
            get => car;
            set
            {
                car = value;
                OnPropertyChanged(nameof(Car));
                OnPropertyChanged(nameof(PublicInfo));
                OnPropertyChanged(nameof(ConfidentialInfo));
                OnPropertyChanged(nameof(Visibility));
            }
        }

        private User currentUser;
        public User CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public CarPageViewModel()
        {
            App.context.EngineNumbers.Load();
            App.context.VINNumbers.Load();
            App.context.Drivers.Load();
            App.context.DriverLicenses.Load();
        }

        public string RegistrationNumber
        {
            get => Car.RegistrationNumber;
        }

        public string VINNumber
        {
            get => Car.Number;
        }

        private bool isConfidential;
        public bool IsConfidential
        {
            get => isConfidential;
            set
            {
                isConfidential = value;
                OnPropertyChanged(nameof(IsConfidential));
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public Visibility Visibility
        {
            get
            {
                if (CurrentUser != null && Car != null && Car.Driver != null && CurrentUser.DriverLicense != null)
                {
                    if (CurrentUser.DriverLicense.DriverId == Car.Driver.DriverId)
                    {
                        return Visibility.Visible;
                    }
                }
                return Visibility.Collapsed;
            }
        }

        public string PublicInfo
        {
            get => $"{Car.CarType} {Car.Brand} {Car.Model}, цвет {Car.Color}, {Car.YearOfIssue}-го года выпуска" +
                $"\nДвигатель: объем {Car.EngineNumber.Capacity} л., мощность {Car.EngineNumber.Power} л.с.";
        }

        public string ConfidentialInfo
        {
            get => $"Владелец {Car.Driver.SurName} {Car.Driver.FirstName} {Car.Driver.Patronymic} {Car.Driver.BirthDay.Date.Day}.{Car.Driver.BirthDay.Date.Month}.{Car.Driver.BirthDay.Date.Year} г.р." +
                $"\nКатегория авто: {Car.Category}\nНомер двигателя: {Car.EngineNumber.NumberEngine}, крутящий момент: {Car.EngineNumber.Torque}";
        }
    }
}