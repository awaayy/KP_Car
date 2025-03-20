using GIBDD.Model;
using GIBDD.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GIBDD.ViewModel
{
    public class RegistrationViewModel : ViewModels
    {
        private ObservableCollection<User> users = new();
        public ObservableCollection<User> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private User authorized;
        public User Authorized
        {
            get => authorized;
            set
            {
                authorized = value;
                OnPropertyChanged(nameof(Authorized));
            }
        }

        public RegistrationViewModel()
        {
            App.context.DriverLicenses.Load();
            App.context.Users.Load();
            foreach (var user in App.context.Users)
            {
                Users.Add(user);
            }
        }

        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string driverLicense;
        public string DriverLicense
        {
            get => driverLicense;
            set
            {
                driverLicense = value;
                OnPropertyChanged(nameof(DriverLicense));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string passwordRepeat;
        public string PasswordRepeat
        {
            get => passwordRepeat;
            set
            {
                passwordRepeat = value;
                OnPropertyChanged(nameof(PasswordRepeat));
            }
        }

        private string loginErrMsg;
        public string LoginErrMsg
        {
            get => loginErrMsg;
            set
            {
                loginErrMsg = value;
                OnPropertyChanged(nameof(LoginErrMsg));
            }
        }

        private string passwordErrMsg;
        public string PasswordErrMsg
        {
            get => passwordErrMsg;
            set
            {
                passwordErrMsg = value;
                OnPropertyChanged(nameof(PasswordErrMsg));
            }
        }

        private string driverLicenseErrMsg;
        public string DriverLicenseErrMsg
        {
            get => driverLicenseErrMsg;
            set
            {
                driverLicenseErrMsg = value;
                OnPropertyChanged(nameof(DriverLicenseErrMsg));
            }
        }

        public bool CheckLogin()
        {
            if (string.IsNullOrWhiteSpace(Login))
            {
                LoginErrMsg = "Поле «Логин» обязательно для заполнения!";
                return false;
            }

            if (Login.Contains(' '))
            {
                LoginErrMsg = "Логин не должен содержать пробелы!";
                return false;
            }

            if (Users.Any(u => u.Login == Login))
            {
                LoginErrMsg = "Пользователь с таким логином существует!";
                return false;
            }

            LoginErrMsg = "";
            return true;
        }

        public bool CheckPassword()
        {
            if (string.IsNullOrEmpty(Password))
            {
                PasswordErrMsg = "Поле «Пароль» обязательно для заполнения!";
                return false;
            }

            if (Password.Contains(' '))
            {
                PasswordErrMsg = "Пароль не должен содержать пробелы!";
                return false;
            }

            if (string.IsNullOrEmpty(PasswordRepeat))
            {
                PasswordErrMsg = "Повторите пароль!";
                return false;
            }

            if (Password != PasswordRepeat)
            {
                PasswordErrMsg = "Пароли должны совпадать!";
                return false;
            }

            PasswordErrMsg = "";
            return true;
        }

        public bool CheckDriverLicense()
        {
            if (string.IsNullOrEmpty(DriverLicense))
            {
                DriverLicenseErrMsg = "Поле водительское удостоверение не должно быть пустым";
                return false;
            }

            if (!DriverLicense.All(char.IsDigit))
            {
                DriverLicenseErrMsg = "Поле водительское удостоверение должно содержать только цифры";
                return false;
            }

            if (App.context.DriverLicenses.Any(dl => dl.Number == DriverLicense && dl.User != null))
            {
                DriverLicenseErrMsg = "Введенное водительское удостоверение уже существует!";
                return false;
            }

            DriverLicenseErrMsg = "";
            return true;
        }
    }
}