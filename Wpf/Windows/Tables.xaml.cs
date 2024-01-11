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
            dataBase.DeleteDataFromDB(Title, TableGrid.SelectedItem);
            DeleteDataFromTable();
        }
        private void Button_Insert(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
