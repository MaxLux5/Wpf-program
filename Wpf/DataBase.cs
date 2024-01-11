using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using System.IO;
using System;

namespace Wpf
{
    enum RegistrationResults
    {
        LoginOrPasswordLessThanFive,
        IncorrectLoginOrPasswordOrMail,
        PasswordOrPasswordСonfirmationNotEqual,
        IncorrectMail,
        UserAlreadyExists,
        RegistrationSuccessful
    }
    internal class DataBase
    {
        #region Variables
        private int minLoginSize = 5;
        private int minPasswordSize = 5;

        private bool isAuthorized = true;
        #endregion

        SqlConnection connection = new SqlConnection(
            "Server=HOME-PC\\SQLEXPRESS; DataBase=MyDB; Integrated Security=True");

        public void OpenConnection()
        {
            if(connection.State == ConnectionState.Closed)
                connection.Open();
        }
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        public SqlConnection GetConnection()
        {
            return connection;
        }

        public bool CheckRegistration(string login, string password)
        {
            string queryString = "select login_user, password_user, mail_user from Register" +
                $" where login_user = '{login}' and password_user = '{password}'";

            OpenConnection();
            SqlCommand cmd = new SqlCommand(queryString, GetConnection());

            var responseFromDB = cmd.ExecuteScalar();
            CloseConnection();
            if (responseFromDB != null) return isAuthorized;
            else return !isAuthorized;
        }
        public RegistrationResults RegisterNewUser(string login, string password, string passwordСonfirmation, string mail)
        {
            if (login.Length < minLoginSize || password.Length < minPasswordSize) return RegistrationResults.LoginOrPasswordLessThanFive;
            if (login == "Логин" || password == "Пароль" || mail.Contains("Почта")) return RegistrationResults.IncorrectLoginOrPasswordOrMail;
            if (password != passwordСonfirmation) return RegistrationResults.PasswordOrPasswordСonfirmationNotEqual;
            if (!(mail.Contains("@gmail.com") || mail.Contains("@yandex.ru") || mail.Contains("@mail.ru"))) return RegistrationResults.IncorrectMail;

            if (CheckRegistration(login, password)) return RegistrationResults.UserAlreadyExists;


            string queryString = "insert into Register(login_user, password_user, mail_user)" +
                $" values ('{login}', '{password}', '{mail}')";
            OpenConnection();
            var command = new SqlCommand(queryString, GetConnection());
            command.ExecuteNonQuery();
            CloseConnection();

            return RegistrationResults.RegistrationSuccessful;
        }
        public void DisplayTable(DataGrid dataGrid, string table)
        {
            OpenConnection();
            string queryString = $"select * from {table}";
            SqlCommand command = new SqlCommand(queryString, GetConnection());
            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable(table);

            adapter.Fill(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;
            CloseConnection();
        }
        public string[] ReturnTableWithTablesNamesFromDB()
        {
            OpenConnection();
            string queryString = $"select name from sys.tables";
            SqlCommand cmd = new SqlCommand(queryString, GetConnection());

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable("Tables");

            dataAdapter.SelectCommand = cmd;
            dataAdapter.Fill(dataTable);

            int counter = dataTable.Select().Length;
            string[] ArrayWithString = new string[counter - 1];
            for (int i = counter - 2, j = counter - 1; i >= 0; i--, j--)
            {
                if (dataTable.Rows[j][0]?.ToString() == "sysdiagrams") j--;
                ArrayWithString[i] = dataTable.Rows[j].ItemArray[0]?.ToString();
            }
            CloseConnection();
            return ArrayWithString;
        }
        public void DeleteDataFromDB(string nameOfTable, object selectedRow)
        {
            if (selectedRow is DataRowView row)
            {
                if (row.Row.ItemArray[0].ToString() == string.Empty) return;

                OpenConnection();
                string queryString = $"delete from {nameOfTable} where id = {row.Row.ItemArray[0]}";
                SqlCommand cmd = new SqlCommand(queryString, connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }
        public void InsertDataToTable()
        {
            
        }
    }
}
