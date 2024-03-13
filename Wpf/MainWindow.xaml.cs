using System;
using System.Windows;
using System.Windows.Controls;

namespace Wpf
{
    public partial class MainWindow : Window
    {
        #region Variables
        private DataBase dataBase = new DataBase();
        private Registration registrationWindow;
        private Menu menuWindow;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            PasswordBoxML.Tag = "";
        }
        
        private void Button_Authorization(object sender, RoutedEventArgs e)
        {
            if (dataBase.CheckRegistration(LoginTextBox.Text, PasswordTextBox.Text))
            {
                MessageBox.Show("Вы успешно вошли!");

                menuWindow = new Menu();
                menuWindow.Show();
                Close();
            }
            else MessageBox.Show("Вы ввели неверные данные!");
        }
        private void Button_Registration(object sender, RoutedEventArgs e)
        {
            registrationWindow = new Registration();
            registrationWindow.Show();
            Close();
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox.IsChecked == true)
            {
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordBoxML.Visibility = Visibility.Hidden;
            }
            else
            {
                PasswordBoxML.Visibility = Visibility.Visible;
                PasswordTextBox.Visibility = Visibility.Hidden;
            }
        }
        private void PasswordBoxML_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Text = PasswordBoxML.Password;
        }
        private void PasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBoxML.Password = PasswordTextBox.Text;
        }
        private void PasswordBoxML_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBoxML.Tag = PasswordBoxML.Password;
        }
    }
}
