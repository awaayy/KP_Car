using GIBDD.Model;
using GIBDD.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GIBDD.View
{
	/// <summary>
	/// Логика взаимодействия для Registration.xaml
	/// </summary>
	public partial class Registration : Window
	{

		public RegistrationViewModel RegistrationViewModel { get; set; } = new();
		public User User { get => RegistrationViewModel.Authorized; }
		public Registration()
		{
			InitializeComponent();
			DataContext = RegistrationViewModel;
		}

        private void Register_Click(object sender, RoutedEventArgs e)
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

    }
}
