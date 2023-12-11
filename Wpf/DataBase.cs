using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;

namespace Wpf
{
    internal class DataBase
    {
        SqlConnection connection = new SqlConnection(
            "Server=HOME-PC\\SQLEXPRESS; DataBase=MyDB; Integrated Security=True");

        public void OpenConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqlConnection GetConnection()
        {
            return connection;
        }

        public bool CheckAuthorization(string login, string password)
        {
            string queryString = "select login_user, password_user, mail_user from Register" +
                $" where login_user = '{login}' and password_user = '{password}'";

            OpenConnection();

            SqlCommand cmd = new SqlCommand(queryString, GetConnection());

            var responseFromDB = cmd.ExecuteScalar();
            CloseConnection();
            if (responseFromDB != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string RegisterNewUser(string login, string password, string passwordСonfirmation, string mail)
        {
            if (login.Length < 5 || password.Length < 5) return "Пароль или Логин меньше 5 символов недопустим!";
            if (password != passwordСonfirmation) return "Пароли не совпадают!";
            if (!(mail.Contains("@gmail.com") || mail.Contains("@yandex.ru") || mail.Contains("@mail.ru"))) return "Вы ввели неверную почту!";

            if (CheckAuthorization(login, password)) return "Такой пользователь уже существует!";


            string queryString = "insert into Register(login_user, password_user, mail_user)" +
                $" values ('{login}', '{password}', '{mail}')";
            OpenConnection();
            var command = new SqlCommand(queryString, GetConnection());
            command.ExecuteNonQuery();
            CloseConnection();

            return "Регистрация прошла успешно!";
        }

        public void DisplayTable(DataGrid dataGrid, string table)
        {
            OpenConnection();
            string queryString = $"select * from" + $" {table}";
            SqlCommand command = new SqlCommand(queryString, GetConnection());
            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable($"{table}");

            adapter.SelectCommand = command;
            adapter.Fill(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;
            CloseConnection();
        }
    }
}
