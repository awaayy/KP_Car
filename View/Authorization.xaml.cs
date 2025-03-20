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
	/// Логика взаимодействия для Authorization.xaml
	/// </summary>
	public partial class Authorization : Window
	{
		AuthorizationViewModel AuthorizationViewModel { get; set; } = new();
		public User User { get => AuthorizationViewModel.Authorized; }
		public Authorization()
		{
			InitializeComponent();
			DataContext = AuthorizationViewModel;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			AuthorizationViewModel.Password = txtPassword.Password;
			if (AuthorizationViewModel.CheckLogin() && AuthorizationViewModel.CheckPassword()) DialogResult = true;
		}
	}
}
