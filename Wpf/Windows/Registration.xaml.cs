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

        private string loginText = "Логин";
        private string passwordText = "Пароль";
        private string passwordConfirmationText = "Подтверждение пароля";
        private string mailText = "Почта";

        private const string loginTextBoxName = "LoginTextBox";
        private const string passwordTextBoxName = "PasswordTextBox";
        private const string passwordConfirmationTextBoxName = "PasswordСonfirmationTextBox";
        private const string mailTextBoxName = "MailTextBox";
        #endregion

        public Registration()
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
                    else if(textBox.Text == string.Empty) textBox.Text = passwordText;
                    break;
                case passwordConfirmationTextBoxName:
                    if (textBox.Text == passwordConfirmationText) textBox.Text = string.Empty;
                    else if(textBox.Text == string.Empty) textBox.Text = passwordConfirmationText;
                    break;
                case mailTextBoxName:
                    if (textBox.Text == mailText) textBox.Text = string.Empty;
                    else if(textBox.Text == string.Empty) textBox.Text = mailText;
                    break;
                default:
                    throw new Exception("Очередная шляпа написана тобой!");
            }
        }

        private void Button_Registration(object sender, RoutedEventArgs e)
        {
            switch (dataBase.RegisterNewUser(LoginTextBox.Text, PasswordTextBox.Text,
                PasswordСonfirmationTextBox.Text, MailTextBox.Text))
            {
                case RegistrationResults.LoginOrPasswordLessThanFive:
                    MessageBox.Show("Логин или Пароль меньше 5 символов!");
                    break;
                case RegistrationResults.IncorrectLoginOrPasswordOrMail:
                    MessageBox.Show("Некорректные Данные");
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
