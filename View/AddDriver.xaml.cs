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
	/// Логика взаимодействия для AddDriver.xaml
	/// </summary>
	public partial class AddDriver : Window
	{
		private DriverLicensesViewModel DriverLicensesViewModel { get; set; }
		public AddDriver()
		{
			InitializeComponent();
			DriverLicensesViewModel = new((Driver)DataContext);
			cbDriverLicense.ItemsSource = DriverLicensesViewModel.DriverLicenses;
			btnAddLicense.DataContext = DriverLicensesViewModel;
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
