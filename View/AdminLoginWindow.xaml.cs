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
	/// Логика взаимодействия для AdminLoginWindow.xaml
	/// </summary>
	public partial class AdminLoginWindow : Window
	{
		public AdminLoginWindow()
		{
			InitializeComponent();
		}
		public string Login { get; private set; }
		public string Password { get; private set; }

		public bool IsSuccess { get; private set; } = false;

		private void BtnLogin_Click(object sender, RoutedEventArgs e)
		{
			Login = txtLogin.Text.Trim();
			Password = txtPassword.Password.Trim();

			if (Login == "admin" && Password == "password123")
			{
				IsSuccess = true;
				MessageBox.Show("Вы успешно вошли как администратор!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
				DialogResult = true;
			}
			else
			{
				MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}
	}
}
