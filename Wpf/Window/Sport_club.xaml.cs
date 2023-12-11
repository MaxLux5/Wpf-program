using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Sport_club.xaml
    /// </summary>
    public partial class Sport_club : Window
    {
        DataBase dataBase = new DataBase();

        Menu MenuWindow;





        public Sport_club()
        {
            InitializeComponent();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            dataBase.DisplayTable(Sport_clubGrid, table: "Sport_club");
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MenuWindow = new Menu();
            MenuWindow.Show();
            Close();
        }
    }
}
