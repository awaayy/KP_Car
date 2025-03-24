using GIBDD.Model;
using GIBDD.View;
using GIBDD.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace GIBDD
{
    public partial class MainWindow : Window
    {
        public static CarsViewModel CarsViewModel { get; set; } = new CarsViewModel();
        public User CurrentUser { get; set; }
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                DataContext = CarsViewModel;
                UpdateButtonVisibility();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка при инициализации MainWindow: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateButtonVisibility()
        {
            if (CurrentUser != null)
            {
                if (CurrentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    Add.Visibility = Visibility.Visible;
                    Delete.Visibility = Visibility.Visible;
                    RegistrationButton.Visibility = Visibility.Collapsed;
                    AdminMode.Visibility = Visibility.Collapsed; // Скрываем кнопку авторизации администратора
                    UserCabinet.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Add.Visibility = Visibility.Collapsed;
                    Delete.Visibility = Visibility.Collapsed;
                    RegistrationButton.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                Add.Visibility = Visibility.Collapsed;
                Delete.Visibility = Visibility.Collapsed;
                RegistrationButton.Visibility = Visibility.Visible;
                AdminMode.Visibility = Visibility.Visible; // Показываем кнопку авторизации администратора, если никто не авторизован
            }
        }

        private void AdminMode_Click(object sender, RoutedEventArgs e)
        {
            AdminLoginWindow adminLoginWindow = new AdminLoginWindow();
            if (adminLoginWindow.ShowDialog() == true)
            {
                CurrentUser = new User { Role = "Admin" };
                UpdateButtonVisibility();
            }
        }

        private void UserCabinet_Click(object sender, RoutedEventArgs e)
        {
            UserCabinet userCabinet = new UserCabinet();
            userCabinet.DataContext = new UserCabinetViewModel(CurrentUser);
            userCabinet.ShowDialog();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            if (registration.ShowDialog() == true)
            {
                System.Windows.MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Reset_Click(object sender, RoutedEventArgs e) { }
        private void GosNumber_TextInput(object sender, TextCompositionEventArgs e) { }
        private void VINField_TextInput(object sender, TextCompositionEventArgs e) { }
        private void VINField_TextChanged(object sender, TextChangedEventArgs e) { }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddCarViewModel vm = new();
            AddCar windowAddCar = new() { DataContext = vm };
            if (windowAddCar.ShowDialog() == true)
            {
                CarsViewModel.Add.Execute(null);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CarsViewModel.SelectedCar != null)
            {
                var result = System.Windows.MessageBox.Show("Вы уверены, что хотите удалить выбранный автомобиль?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    CarsViewModel.Delete.Execute(null);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите автомобиль для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddMenuItem_Click(object sender, RoutedEventArgs e) { }
        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e) { }
        private void ViewMenuItem_Click(object sender, RoutedEventArgs e) { }
    }
}