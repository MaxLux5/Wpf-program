using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using System.IO;
using System;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Data.Common;

namespace Wpf
{
    enum RegistrationResults
    {
        LoginOrPasswordLessThanFive,
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

        MySqlConnection connection = new MySqlConnection(
            "Server=localhost; DataBase=MyDB; password=24681357; username=root");

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
        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public bool CheckRegistration(string login, string password)
        {
            string queryString = "select login_user, password_user, mail_user from Register" +
                $" where login_user = '{login}' and password_user = '{password}'";

            OpenConnection();
            MySqlCommand cmd = new MySqlCommand(queryString, GetConnection());

            var responseFromDB = cmd.ExecuteScalar();
            CloseConnection();
            if (responseFromDB != null) return isAuthorized;
            else return !isAuthorized;
        }
        public RegistrationResults RegisterNewUser(string login, string password, string passwordСonfirmation, string mail)
        {
            if (login.Length < minLoginSize || password.Length < minPasswordSize) return RegistrationResults.LoginOrPasswordLessThanFive;
            if (password != passwordСonfirmation) return RegistrationResults.PasswordOrPasswordСonfirmationNotEqual;
            if (!(mail.Contains("@gmail.com") || mail.Contains("@yandex.ru") || mail.Contains("@mail.ru"))) return RegistrationResults.IncorrectMail;

            if (CheckRegistration(login, password)) return RegistrationResults.UserAlreadyExists;


            string queryString = "insert into Register(login_user, password_user, mail_user)" +
                $" values ('{login}', '{password}', '{mail}')";
            OpenConnection();
            var command = new MySqlCommand(queryString, GetConnection());
            command.ExecuteNonQuery();
            CloseConnection();

            return RegistrationResults.RegistrationSuccessful;
        }
        public DataTable ReturnTableFromDB(string tableName, string conditionForQuery = "")
        {
            OpenConnection();
            string queryString = $"select * from {tableName} {conditionForQuery}";
            var command = new MySqlCommand(queryString, GetConnection());
            command.ExecuteNonQuery();

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable(tableName);

            adapter.Fill(dataTable);
            CloseConnection();
            return dataTable;
        }
        public string[] ReturnArrayOfTableNamesFromDB()
        {
            OpenConnection();
            string queryString = $"show tables";
            var cmd = new MySqlCommand(queryString, GetConnection());

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            DataTable dataTable = new DataTable("Tables");

            dataAdapter.Fill(dataTable);

            int counter = dataTable.Rows.Count;
            string[] ArrayOfStrings = new string[counter];
            for (int i = 0; i < counter; i++)
            {
                ArrayOfStrings[i] = dataTable.Rows[i].ItemArray[0]?.ToString();
            }
            CloseConnection();
            return ArrayOfStrings;
        }
        public void DeleteDataFromDB(string nameOfTable, object selectedRow, object dataGridTable)
        {
            var dataView = dataGridTable as DataView;
            var columnName = dataView.Table.Columns[0];
            if (selectedRow is DataRowView row)
            {
                var dataName = row.Row.ItemArray[0];
                if (dataName?.ToString() == string.Empty) return;
                OpenConnection();
                string queryString = $"delete from {nameOfTable} where {columnName} = '{dataName}'";
                var cmd = new MySqlCommand(queryString, connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }
        public void InsertDataIntoDB(string nameOfTable, object selectedRow, object dataGridTable)
        {
            var dataView = dataGridTable as DataView;
            var columns = dataView.Table.Columns;
            var rowView = selectedRow as DataRowView;
            if (rowView == null) throw new Exception("Выберите строку для сохранения в базе данных!");
            var row = rowView.Row;

            int columnsCount = columns.Count;
            string queryString = $"insert into {nameOfTable}(";
            for (int i = 0; i < columnsCount; i++)
            {
                if (columns[i].ToString() == "id") continue;
                if(i == columnsCount - 1)
                {
                    queryString += $"{columns[i]})";
                    break;
                }
                queryString += $"{columns[i]}, ";
            }
            queryString += " values (";
            for (int i = 0; i < columnsCount; i++)
            {
                if (i == columnsCount - 1)
                {
                    queryString += $"'{row[i]}')";
                    break;
                }
                if (columns[i].ToString() == "id" && row[i].ToString() == string.Empty) continue;
                if (row[i].ToString() == string.Empty) throw new Exception("Обнаружена пустая ячейка в добавляемой строке!");

                queryString += $"'{row[i]}', ";
            }
            OpenConnection();
            var cmd = new MySqlCommand(queryString, connection);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public void ChangeDataInTableRowFromDB(string nameOfTable, object selectedRow, object dataGridTable)
        {
            var rowView = selectedRow as DataRowView;
            var row = rowView?.Row;
            if(row == null) return;
            string conditionForQuery = $"where id = {row[0]}";
            DataTable currentTable = ReturnTableFromDB(nameOfTable, conditionForQuery);
            var currentRows = currentTable.Rows;
            var currentColumns = currentTable.Columns;

            int columnsCount = currentColumns.Count;
            var queryString = $"update {nameOfTable} set";
            for (int i = 0; i < columnsCount; i++)
            {
                if (currentColumns[i].ToString() == "id") continue;
                if (i == columnsCount - 1)
                {
                    queryString += $" {currentColumns[i]} = '{row[i]}' where id = '{Convert.ToInt32(row[0])}'";
                    break;
                }
                queryString += $" {currentColumns[i]} = {row[i]},";
            }

            OpenConnection();
            var cmd = new MySqlCommand(queryString, connection);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
        public DataTable SearchRowsFromDB(string nameOfTable, string textWhichLookingFor)
        {
            DataTable currentTable = ReturnTableFromDB(nameOfTable);
            var columns = currentTable.Columns;
            var rows = currentTable.Rows;
            var newTable = new DataTable(nameOfTable);

            foreach (var item in columns)
            {
                newTable.Columns.Add(item.ToString());
            }
            for (int i = 0; i < rows.Count; i++)
            {
                object[] array = rows[i].ItemArray;
                foreach (var item in rows[i].ItemArray)
                {
                    if (item.ToString().IndexOf(textWhichLookingFor, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        newTable.Rows.Add(array);
                    }
                }
            }

            return newTable;
        }
    }
}
