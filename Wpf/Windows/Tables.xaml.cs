using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Wpf
{
    public partial class Table : Window
    {
        #region Variables
        private DataBase dataBase = new DataBase();
        private Menu MenuWindow;
        #endregion

        public Table(string title)
        {
            InitializeComponent();
            Title = title;
        }

        public DataGrid ReturnDataGrid() => TableGrid;
        private void DeleteDataFromTable()
        {
            var itemSource = TableGrid.ItemsSource as DataView;
            try
            {
                itemSource.Delete(TableGrid.SelectedIndex);
            }
            catch
            {
                MessageBox.Show("Выберите строку для удаления!");
            }
            TableGrid.ItemsSource = itemSource;
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MenuWindow = new Menu();
            MenuWindow.Show();
            Close();
        }
        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            bool answer = MessageBox.Show("Удалить эту строку?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (answer)
            {
                dataBase.DeleteDataFromDB(Title, TableGrid.SelectedItem, TableGrid.ItemsSource);
                DeleteDataFromTable();
            }
        }
        private void Button_Insert(object sender, RoutedEventArgs e)
        {
            bool answer = MessageBox.Show("Добавить эту строку?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (answer)
            {
                try
                {
                    dataBase.InsertDataIntoDB(Title, TableGrid.SelectedItem, TableGrid.ItemsSource);
                    MessageBox.Show("Данные добавлены!");
                }
                catch (MySqlException)
                {
                    MessageBox.Show("Эта строка уже была добавлена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void DisplayTableFromDB()
        {
            DataTable table = dataBase.ReturnTableFromDB(Title);
            TableGrid.ItemsSource = table.DefaultView;
        }
        private void Button_Update(object sender, RoutedEventArgs e)
        {
            DisplayTableFromDB();
        }
        private void Button_Change(object sender, RoutedEventArgs e)
        {
            bool answer = MessageBox.Show("Изменить эту строку?", "Изменение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (answer)
            {
                dataBase.ChangeDataInTableRowFromDB(Title, TableGrid.SelectedItem, TableGrid.ItemsSource);
            }
        }
        private void Button_Search(object sender, RoutedEventArgs e)
        {
            if(SearchTextBox.Text != string.Empty)
            {
                var newTable = dataBase.SearchRowsFromDB(Title, SearchTextBox.Text);
                TableGrid.ItemsSource = newTable.DefaultView;
            }
            else
            {
                DisplayTableFromDB();
            }
        }
    }
}
