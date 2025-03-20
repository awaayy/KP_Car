using GIBDD.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace GIBDD.ViewModel
{
    public class AuthorizationViewModel : ViewModels
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

        public AuthorizationViewModel()
        {
            try
            {
                App.context.DriverLicenses.Load();
                App.context.Users.Load();
                foreach (var user in App.context.Users)
                {
                    Users.Add(user);
                }
            }
            catch
            {
                Users = new();
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

        public bool CheckLogin()
        {
            if (string.IsNullOrEmpty(Login))
            {
                LoginErrMsg = "Поле «Логин» обязательно для заполнения!";
                return false;
            }

            if (Login.Contains(' '))
            {
                LoginErrMsg = "Логин не должен содержать пробелы!";
                return false;
            }

            if (!Users.Any(u => u.Login == Login))
            {
                LoginErrMsg = "Пользователя не существует!";
                return false;
            }

            LoginErrMsg = "";
            return true;
        }

        public bool CheckPassword()
        {
            if (!CheckLogin())
                return false;

            if (string.IsNullOrEmpty(Password))
            {
                PasswordErrMsg = "Поле «Пароль» обязательно для заполнения!";
                return false;
            }

            var user = Users.FirstOrDefault(u => u.Login == Login && u.Password == Password);
            if (user == null)
            {
                PasswordErrMsg = "Пароль неверный!";
                return false;
            }

            Authorized = user;
            PasswordErrMsg = "";
            return true;
        }
    }
}