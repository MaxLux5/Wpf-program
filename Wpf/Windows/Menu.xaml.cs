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
            ComboBox.ItemsSource = dataBase.ReturnTableWithTablesNamesFromDB();
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox.SelectedItem is null)
            {
                MessageBox.Show("Вы не выбрали таблицу!");
                return;
            }

            tableWindow = new Table(ComboBox.SelectedItem.ToString());
            dataBase.DisplayTable(tableWindow.ReturnDataGrid(), ComboBox.SelectedItem.ToString());
            tableWindow.Show();
            Close();
        }
    }
}
