﻿using System;
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
	/// Логика взаимодействия для AddDriverLicense.xaml
	/// </summary>
	public partial class AddDriverLicense : Window
	{
		public AddDriverLicense()
		{
			InitializeComponent();
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
