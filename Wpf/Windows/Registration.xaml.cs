using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Wpf
{
    public partial class Registration : Window
    {
        #region Variables
        private DataBase dataBase = new DataBase();
        private MainWindow mainWindow;
        #endregion

        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Registration(object sender, RoutedEventArgs e)
        {
            switch (dataBase.RegisterNewUser(LoginTextBox.Text, PasswordTextBox.Text,
                PasswordСonfirmationTextBox.Text, MailTextBox.Text))
            {
                case RegistrationResults.LoginOrPasswordLessThanFive:
                    MessageBox.Show("Логин или Пароль меньше 5 символов!");
                    break;
                case RegistrationResults.PasswordOrPasswordСonfirmationNotEqual:
                    MessageBox.Show("Пароли не совпадают!");
                    break;
                case RegistrationResults.IncorrectMail:
                    MessageBox.Show("Вы ввели неверную почту!");
                    break;
                case RegistrationResults.UserAlreadyExists:
                    MessageBox.Show("Такой пользователь уже существует!");
                    break;
                case RegistrationResults.RegistrationSuccessful:
                    MessageBox.Show("Регистрация прошла успешно!");
                    break;
                default:
                    break;
            }
        }
        private void Button_Back(object sender, RoutedEventArgs e)
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
