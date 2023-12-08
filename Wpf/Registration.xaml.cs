using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        DataBase dataBase = new DataBase();

        MainWindow mainWindow;





        public Registration()
        {
            InitializeComponent();
        }

        private void LoginGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Логин")
            {
                textBox.Text = string.Empty;
            }
        }
        private void LoginLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == string.Empty)
            {
                textBox.Text = "Логин";
            }
        }

        private void PasswordGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Пароль")
            {
                textBox.Text = string.Empty;
            }
        }
        private void PasswordLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == string.Empty)
            {
                textBox.Text = "Пароль";
            }
        }

        private void PasswordСonfirmationGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Подтверждение пароля")
            {
                textBox.Text = string.Empty;
            }
        }
        private void PasswordConfirmationLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == string.Empty)
            {
                textBox.Text = "Подтверждение пароля";
            }
        }

        private void MailGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Почта")
            {
                textBox.Text = string.Empty;
            }
        }
        private void MailLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == string.Empty)
            {
                textBox.Text = "Почта";
            }
        }



        private void Button_Registration(object sender, RoutedEventArgs e)
        {
            dataBase.OpenConnection();

            string queryString = "select login_user, password_user, mail_user from Register" +
                $" where login_user = '{LoginTextBox.Text}' and password_user = '{PasswordTextBox.Text}' and mail_user = '{MailTextBox.Text}'";

            if (PasswordTextBox.Text == PasswordСonfirmationTextBox.Text)
            {
                if (MailTextBox.Text.Contains("@gmail.com") || MailTextBox.Text.Contains("@yandex.ru") || MailTextBox.Text.Contains("@mail.ru"))
                {
                    if (PasswordTextBox.Text.Length >= 5 || LoginTextBox.Text.Length >= 5)
                    {
                        SqlCommand command = new SqlCommand(queryString, dataBase.GetConnection());
                        if (command.ExecuteScalar() == null)
                        {
                            queryString = "insert into Register(login_user, password_user, mail_user)" +
                                $" values ('{LoginTextBox.Text}', '{PasswordTextBox.Text}', '{MailTextBox.Text}')";

                            command = new SqlCommand(queryString, dataBase.GetConnection());
                            command.ExecuteNonQuery();

                            MessageBox.Show("Регистрация прошла успешно!");
                            OpenMainWindow();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароль или Логин меньше 5 символов недопустим!");
                    }
                }
                else
                {
                    MessageBox.Show("Вы ввели неверную почту!");
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают!");
            }

            dataBase.CloseConnection();
        }



        private void Button_Back(object sender, RoutedEventArgs e)
        {
            OpenMainWindow();
        }



        private void OpenMainWindow()
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
