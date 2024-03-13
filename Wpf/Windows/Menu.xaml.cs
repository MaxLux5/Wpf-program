using System.Data;
using System.Windows;
using System.Windows.Documents.DocumentStructures;

namespace Wpf
{
    public partial class Menu : Window
    {
        #region Variables
        private DataBase dataBase = new DataBase();
        private MainWindow mainWindow;
        private Table tableWindow;
        #endregion

        public Menu()
        {
            InitializeComponent();
            ComboBox.ItemsSource = dataBase.ReturnArrayOfTableNamesFromDB();
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Display(object sender, RoutedEventArgs e)
        {
            if (ComboBox.SelectedItem is null)
            {
                MessageBox.Show("Вы не выбрали таблицу!");
                return;
            }

            tableWindow = new Table(ComboBox.SelectedItem.ToString());
            DataTable dataTable = dataBase.ReturnTableFromDB(ComboBox.SelectedItem.ToString());
            var dataGrid = tableWindow.ReturnDataGrid();
            dataGrid.ItemsSource = dataTable.DefaultView;
            tableWindow.Show();
            Close();
        }
    }
}
