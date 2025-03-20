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
	/// Логика взаимодействия для AddEngine.xaml
	/// </summary>
	public partial class AddEngine : Window
	{
		public AddEngine()
		{
			InitializeComponent();
		}

		private void OK_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
        }
    }
}
