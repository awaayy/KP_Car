using GIBDD.Model;
using GIBDD.View;
using GIBDD.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GIBDD
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static CarsViewModel CarsViewModel { get; set; } = new CarsViewModel();
		public MainWindow()
		{
			InitializeComponent();
			DataContext = CarsViewModel;
			
		}

        public ICommand OpenProtocolsCommand { get; }
        public ICommand OpenInsurancesCommand { get; }

        private void VINField_LostFocus(object sender, RoutedEventArgs e)
		{

        }

		private void GosNumber_TextInput(object sender, TextCompositionEventArgs e)
		{

        }
    }
}