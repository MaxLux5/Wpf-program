using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataBase dataBase = new DataBase();

        Registration registrationWindow;





        public MainWindow()
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

        private void Button_Authorization(object sender, RoutedEventArgs e)
        {
            string queryString = "select login_user, password_user, mail_user from Register" +
                $" where login_user = '{LoginTextBox.Text}' and password_user = '{PasswordTextBox.Text}' and mail_user = '{MailTextBox.Text}'";

            dataBase.OpenConnection();

            SqlCommand cmd = new SqlCommand(queryString, dataBase.GetConnection());

            if (cmd.ExecuteScalar() != null)
            {
                MessageBox.Show("Вы успешно вошли!");
            }
            else
            {
                MessageBox.Show("Вы ввели неверные данные!");
            }

            dataBase.CloseConnection();
        }

        private void Button_Registration(object sender, RoutedEventArgs e)
        {
            registrationWindow = new Registration();
            registrationWindow.Show();
            Close();
        }
    }
}
