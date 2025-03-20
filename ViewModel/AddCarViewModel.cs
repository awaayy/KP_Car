using GIBDD.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBDD.ViewModel
{
	public class AddCarViewModel : ViewModels
	{
		private VINNumber car;

		public VINNumber Car 
		{ 
			get => car;
			set 
			{ 
				car = value;
				OnPropertyChanged(nameof(Car));
			}
		}

		public string Number
		{
			get => Car.Number;
			set
			{
				Car.Number = value;
				OnPropertyChanged(nameof(Number));
			}
		}

		public string RegistrationNumber
		{
			get => Car.RegistrationNumber;
			set
			{
				Car.RegistrationNumber = value;
				OnPropertyChanged(nameof(RegistrationNumber));
			}
		}

		public string Brand
		{
			get => Car.Brand;
			set
			{
				Car.Brand = value;
				OnPropertyChanged(nameof(Brand));
			}
		}

		public string Model
		{
			get => Car.Model;
			set
			{
				Car.Model = value;
				OnPropertyChanged(nameof(Model));
			}
		}

		public string YearOfIssue
		{
			get => Car.YearOfIssue;
			set
			{
				Car.YearOfIssue = value;
				OnPropertyChanged(nameof(YearOfIssue));
			}
		}

		public string CarType
		{
			get => Car.CarType;
			set
			{
				Car.CarType = value;
				OnPropertyChanged(nameof(CarType));
			}
		}

		public string Category
		{
			get => Car.Category;
			set
			{
				Car.Category = value;
				OnPropertyChanged(nameof(Category));
			}
		}

		public Driver Driver
		{
			get => Car.Driver;
			set
			{
				Car.Driver = value;
				OnPropertyChanged(nameof(Driver));
			}
		}

		public EngineNumber EngineNumber
		{
			get => Car.EngineNumber;
			set
			{
				Car.EngineNumber = value;
				OnPropertyChanged(nameof(EngineNumber));
			}
		}

		public string Photography
		{
			get => Car.Photography;
			set
			{
				Car.Photography = value;
				OnPropertyChanged(nameof(Photography));
			}
		}

		public string ImageUrl
		{
			get => Car.ImageUrl;
			set
			{
				OnPropertyChanged(ImageUrl);
			}
		}

		public AddCarViewModel()
		{
			Car = new();
		}

		private RelayCommand changePhoto;
		public RelayCommand ChangePhoto
		{
			get
			{
				return changePhoto ?? (changePhoto = new(obj =>
				{
					string photographyPath = "";
					OpenFileDialog openFileDialog = new()
					{
						Filter = "Файлы изображений (*.png, *.jpg, *.jpeg)|*.png;*.jpg;*.jpeg"
					};
					if (openFileDialog.ShowDialog() == true)
					{
						photographyPath = System.IO.Path.GetFileName(openFileDialog.FileName);
						try
						{
							File.Copy(openFileDialog.FileName, App.PhotographyDirectory + $"\\{photographyPath}");
							Photography = photographyPath;
						}
						catch
						{
							Photography = photographyPath;
						}
						OnPropertyChanged(nameof(Photography));
						OnPropertyChanged(nameof(ImageUrl));
					}
				},
				obj => true));
			}
		}
	}
}
