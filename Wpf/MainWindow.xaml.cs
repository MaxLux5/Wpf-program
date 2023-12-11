using System;
using System.Data.SqlClient;
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

        private string loginText = "Логин";
        private string passwordText = "Пароль";

        private const string loginTextBoxName = "LoginTextBox";
        private const string passwordTextBoxName = "PasswordTextBox";
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChangeTextBox(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            switch (textBox.Name)
            {
                case loginTextBoxName:
                    if (textBox.Text == loginText) textBox.Text = string.Empty;
                    else if(textBox.Text == string.Empty) textBox.Text = loginText;
                    break;
                case passwordTextBoxName:
                    if (textBox.Text == passwordText) textBox.Text = string.Empty;
                    else if (textBox.Text == string.Empty) textBox.Text = passwordText;
                    break;
                default:
                    throw new Exception("Шляпу ты написал, чудила!");
            }
            
        }
        private void Button_Authorization(object sender, RoutedEventArgs e)
        {
            if (dataBase.CheckAuthorization(LoginTextBox.Text, PasswordTextBox.Text))
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
    }
}
