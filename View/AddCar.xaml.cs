using GIBDD.Model;
using GIBDD.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
	/// Логика взаимодействия для AddCar.xaml
	/// </summary>
	public partial class AddCar : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private EnginesViewModel EnginesViewModel = new();
		private DriversViewModel DriversViewModel = new();
		public AddCar()
		{
			InitializeComponent();
            AddEngine.DataContext = EnginesViewModel;
            EngineBox.ItemsSource = EnginesViewModel.Engines;
			AddDriver.DataContext = DriversViewModel;
			DriversBox.ItemsSource = DriversViewModel.Drivers;
		}

        // Добавьте обработчик события на кнопку, которая будет открывать окно AddEngine
        private void AddEngine_Click(object sender, RoutedEventArgs e)
        {
            // Создаем и открываем окно AddEngine
            AddEngine addEngineWindow = new AddEngine();

            // Показываем окно как модальное
            bool? result = addEngineWindow.ShowDialog();

            // После закрытия окна можно обработать результат, если необходимо
            if (result == true)
            {
                // Обновляем список двигателей
                EnginesViewModel.RefreshEngines();
            }
        }



        private void Accept_Click(object sender, RoutedEventArgs e)
		{
			string ErrorMsg = string.Empty;
			if (ColorBox.Text == null)
			{
				ErrorMsg += "Укажите цвет\n";
			}
			if (BrandBox.Text == null)
			{
				ErrorMsg += "Укажите бренд\n";
			}
			if (ModelBox.Text == null)
			{
                ErrorMsg += "Укажите модель\n";
            }
			if (YearBox.Text == null || YearBox.Text.Length != 4)
			{
                ErrorMsg += "Корректно укажите год\n";
            }
			if (CarTypeBox.Text == null)
			{
                ErrorMsg += "Укажите тип автомобиля\n";
            }
			if (CategoryBox.Text == null)
			{
                ErrorMsg += "Укажите категорию автомобиля\n";
            }
			if (EngineBox.SelectedValue == null)
			{
				if (EngineBox.Items.Count == 0)
				{
					ErrorMsg += "Создайте новый двигатель и укажите его\n";
				}
				else ErrorMsg += "Укажите двигатель\n"; 
            }
			if (DriversBox.SelectedValue == null)
			{
                if (DriversBox.Items.Count == 0)
                {
                    ErrorMsg += "Внесите нового водителя и укажите его\n";
                }
                else ErrorMsg += "Укажите водителя\n";
            }
			if (PathBlock.Text == null)
			{
				ErrorMsg += "Выберите фотографию\n";
            }
			if (ErrorMsg != string.Empty)
			{
				MessageBox.Show(ErrorMsg, "Устраните ошибки");
				return;
			}
			DialogResult = true;
        }

		private void MaskedTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			// Допустимые буквы: А, В, Е, К, М, Н, О, Р, С, Т, У, Х (кириллица и латиница)
			string allowedLetters = "ABEKMHOPCTYXАВЕКМНОРСТУХ";

			// Проверяем, является ли введенный символ допустимой буквой или цифрой
			if (!Regex.IsMatch(e.Text, $"^[{allowedLetters.ToLower()}]$") && !char.IsDigit(e.Text, 0))
			{
				e.Handled = true;
			}
		}

		private void VinTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			string allowedChars = "ABCDEFGHJKLMNPRSTUVWXYZ0123456789";

			if (!allowedChars.Contains(e.Text.ToUpper()))
			{
				e.Handled = true; 
			}
		}

		private void VinTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string vin = vinTextBox.Text.ToUpper();

			if (vin.Length == 17)
			{
				if (validationResult == null) 
				{
					return;
				}

				// Общая проверка формата VIN-номера
				string vinPattern = @"^[A-HJ-NP-Z0-9]{17}$";
				if (!Regex.IsMatch(vin, vinPattern))
				{
					validationResult.Text = "Ошибка: Недопустимые символы.";
					return;
				}

				// Проверка WMI (первые 3 символа)
				string wmi = vin.Substring(0, 3);
				if (!Regex.IsMatch(wmi, @"^[A-HJ-NP-Z0-9]{3}$"))
				{
					validationResult.Text = "Ошибка: Некорректный WMI.";
					return;
				}

				// Проверка VDS (4-9 символы)
				string vds = vin.Substring(3, 6);
				if (!Regex.IsMatch(vds, @"^[A-HJ-NP-Z0-9]{6}$"))
				{
					validationResult.Text = "Ошибка: Некорректный VDS.";
					return;
				}

				// Проверка VIS (10-17 символы)
				string vis = vin.Substring(9, 8);
				if (!Regex.IsMatch(vis, @"^[A-HJ-NP-Z0-9]{8}$"))
				{
					validationResult.Text = "Ошибка: Некорректный VIS.";
					return;
				}

				// Проверка десятого символа (год выпуска)
				char yearCode = vis[0];
				if (!IsYearCodeValid(yearCode))
				{
					validationResult.Text = "Ошибка: Недопустимый код года.";
					return;
				}

				validationResult.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
				validationResult.Text = "VIN-номер корректен!";
			}
			else
			{
				validationResult.Text = "Введите полный VIN-номер (17 символов).";
			}
		}

		private bool IsYearCodeValid(char yearCode)
		{
			// Список допустимых кодов года (пример: A-Y, 0-9, исключая I, O, Q)
			string validYearCodes = "ABCDEFGHJKLMNPRSTUVWXYZ0123456789";
			return validYearCodes.Contains(yearCode);
		}

		private void PhotographyChange_Click(object sender, RoutedEventArgs e)
		{
			var car = this.DataContext as VINNumber;
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
					car.Photography = photographyPath;
				}
				catch
				{
					car.Photography = photographyPath;
				}
				OnPropertyChanged(nameof(car.Photography));
				OnPropertyChanged(nameof(car.ImageUrl));
			}
		}
	}
}
