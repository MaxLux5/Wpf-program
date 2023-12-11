using System.Windows;

namespace Wpf
{
    public partial class Menu : Window
    {
        #region Variables
        private MainWindow mainWindow;
        private Table tableWindow;

        private string sportClubNameOfTable = "Sport_club";
        private string trenerNameOfTable = "Trener";
        private string sportsmenNameOfTable = "Sportsmen";
        private string dogovorNameOfTable = "Dogovor";
        private string vidSportaNameOfTable = "Vid_sporta";
        private string trenirovkaNameOfTable = "Trenirovka";
        #endregion

        public Menu()
        {
            InitializeComponent();
        }
        private void ChangeWindow(string nameOfTable)
        {
            tableWindow = new Table(nameOfTable);
            tableWindow.Show();
            Close();
        }
        private void Button_Sport_club(object sender, RoutedEventArgs e) => ChangeWindow(sportClubNameOfTable);
        private void Button_Trener(object sender, RoutedEventArgs e) => ChangeWindow(trenerNameOfTable);
        private void Button_Sportsmen(object sender, RoutedEventArgs e) => ChangeWindow(sportsmenNameOfTable);
        private void Button_Dogovor(object sender, RoutedEventArgs e) => ChangeWindow(dogovorNameOfTable);
        private void Button_Vid_sporta(object sender, RoutedEventArgs e) => ChangeWindow(vidSportaNameOfTable);
        private void Button_Trenirovka(object sender, RoutedEventArgs e) => ChangeWindow(trenirovkaNameOfTable);

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
