using GIBDD.Model;
using GIBDD.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBDD.ViewModel
{
	public class DriversViewModel : ViewModels
	{
		private ObservableCollection<Driver> drivers = new();
		public ObservableCollection<Driver> Drivers
		{
			get => drivers;
			set
			{
				drivers = value;
				OnPropertyChanged(nameof(Drivers));
			}
		}

		public DriversViewModel()
		{
			App.context.DriverLicenses.Load();
			App.context.VINNumbers.Load();
			App.context.Users.Load();

			foreach (var driver in App.context.Drivers)
			{
				Drivers.Add(driver);
			}
		}

		private RelayCommand add;
		public RelayCommand Add
		{
			get
			{
				return add ?? (add = new(obj =>
				{
					Driver driver = new();
					AddDriver addDriver = new()
					{
						DataContext = driver
					};
					if(addDriver.ShowDialog() == true)
					{
						App.context.Add(driver);
						Drivers.Add(driver);
					}
				},
				obj => true));
			}
		}
	}
}
