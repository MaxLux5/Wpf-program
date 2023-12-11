using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Wpf
{
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
            MessageBox.Show(dataBase.RegisterNewUser(LoginTextBox.Text, PasswordTextBox.Text, 
                PasswordСonfirmationTextBox.Text, MailTextBox.Text));
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
