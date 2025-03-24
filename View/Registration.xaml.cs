using GIBDD.Model;
using GIBDD.ViewModel;
using System;
using System.Windows;

namespace GIBDD.View
{
    public partial class Registration : Window
    {
        public RegistrationViewModel RegistrationViewModel { get; set; } = new();
        public User User { get => RegistrationViewModel.Authorized; }

        public Registration()
        {
            try
            {
                InitializeComponent();
                DataContext = RegistrationViewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации Registration: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistrationViewModel.Password = txtPassword.Password;
                RegistrationViewModel.PasswordRepeat = txtConfirmPassword.Password;

                if (RegistrationViewModel.CheckLogin() && RegistrationViewModel.CheckPassword() && RegistrationViewModel.CheckDriverLicense())
                {
                    MessageBox.Show("Регистрация прошла успешно!", "Теперь выполните вход.", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Ошибка регистрации! Проверьте введённые данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
