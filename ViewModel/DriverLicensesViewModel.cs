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
	public class DriverLicensesViewModel : ViewModels
	{
		private ObservableCollection<DriverLicense> driverLicenses = new();
		public ObservableCollection<DriverLicense> DriverLicenses
		{
			get => driverLicenses;
			set
			{
				driverLicenses = value;
				OnPropertyChanged(nameof(DriverLicenses));
			}
		}

		private ObservableCollection<VehicleCategory> categories;
		public ObservableCollection<VehicleCategory> Categories
		{
			get => categories;
			set
			{
				categories = value;
				OnPropertyChanged(nameof(Categories));
			}
		}

		private string _licenseNumber;
		private string _issuedBy;
		private DateOnly _dateOfIssue;

		public string LicenseNumber
		{
			get => _licenseNumber;
			set
			{
				_licenseNumber = value;
				OnPropertyChanged(nameof(LicenseNumber));
			}
		}

		public string IssuedBy
		{
			get => _issuedBy;
			set
			{
				_issuedBy = value;
				OnPropertyChanged(nameof(IssuedBy));
			}
		}

		public DateOnly DateOfIssue
		{
			get => _dateOfIssue;
			set
			{
				_dateOfIssue = value;
				OnPropertyChanged(nameof(DateOfIssue));
			}
		}

		private Driver driver;
		public Driver Driver
		{
			get => driver;
			set
			{
				driver = value;
				OnPropertyChanged(nameof(Driver));
			}
		}

		public DriverLicensesViewModel(Driver driver)
		{
			Driver = driver;
			App.context.Drivers.Load();
			App.context.Users.Load();
			App.context.VINNumbers.Load();

			foreach (var dl in App.context.DriverLicenses)
			{
				DriverLicenses.Add(dl);
			}

			Categories = new()
			{
				new VehicleCategory { Name = "A" },
				new VehicleCategory { Name = "B" },
				new VehicleCategory { Name = "C" },
				new VehicleCategory { Name = "D" },
				new VehicleCategory { Name = "E" }
			};
		}

		private RelayCommand add;
		public RelayCommand Add
		{
			get
			{
				return add ?? (add = new(obj =>
				{
					AddDriverLicense addDriverLicense = new()
					{
						DataContext = this
					};
					if (addDriverLicense.ShowDialog() == true)
					{
						string selectedCategoriesStr = string.Join(", ", Categories
							.Where(c => c.IsSelected)
							.Select(c => c.Name));

						DriverLicense driverLicense = new()
						{
							Driver = Driver,
							Number = LicenseNumber,
							VehicleCategories = selectedCategoriesStr,
							IssuedBy = IssuedBy,
							DateOfIssue = DateOfIssue
						};
						App.context.Add(driverLicense);
						DriverLicenses.Add(driverLicense);
					}
				},
				obj => true));
			}
		}
	}
}
