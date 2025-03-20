using GIBDD.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBDD.ViewModel
{
	public class UserCabinetViewModel : ViewModels
	{
		private ObservableCollection<VINNumber> userCars = new();
		public ObservableCollection<VINNumber> UserCars
		{
			get => userCars;
			set
			{
				userCars = value;
				OnPropertyChanged(nameof(UserCars));
			}
		}

		private User currentUser;
		public User CurrentUser
		{
			get => currentUser;
			set
			{
				currentUser = value;
				OnPropertyChanged(nameof(CurrentUser));
			}
		}

		private string FullName
		{
			get => CurrentUser.DriverLicense.Driver.FirstName + " " + CurrentUser.DriverLicense.Driver.SurName;
		}

		private string Login
		{
			get => CurrentUser.Login;
		}

		public UserCabinetViewModel(User user)
		{
			CurrentUser = user;
			App.context.Users.Load();
			App.context.VINNumbers.Load();
			App.context.DriverLicenses.Load();
			App.context.Drivers.Load();
			foreach(var car in App.context.VINNumbers)
			{
				if (car.Driver.DriverLicense == CurrentUser.DriverLicense) UserCars.Add(car);
			}
		}
	}
}
