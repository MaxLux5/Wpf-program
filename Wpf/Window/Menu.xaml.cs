using System.Windows;

namespace Wpf
{
    public partial class Menu : Window
    {
        MainWindow mainWindow;
        Sport_club sport_clubWindow;
        Trener trenerWindow;
        Vid_sporta vid_sportaWindow;
        Sportsmen SportsmenWindow;
        Dogovor dogovorWindow;
        Trenirovka trenirovkaWindow;





        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Sport_club(object sender, RoutedEventArgs e)
        {
            sport_clubWindow = new Sport_club();
            sport_clubWindow.Show();
            Close();
        }

        private void Button_Trener(object sender, RoutedEventArgs e)
        {
            trenerWindow = new Trener();
            trenerWindow.Show();
            Close();
        }

        private void Button_Sportsmen(object sender, RoutedEventArgs e)
        {
            SportsmenWindow = new Sportsmen();
            SportsmenWindow.Show();
            Close();
        }

        private void Button_Dogovor(object sender, RoutedEventArgs e)
        {
            dogovorWindow = new Dogovor();
            dogovorWindow.Show();
            Close();
        }

        private void Button_Vid_sporta(object sender, RoutedEventArgs e)
        {
            vid_sportaWindow = new Vid_sporta();
            vid_sportaWindow.Show();
            Close();
        }

        private void Button_Trenirovka(object sender, RoutedEventArgs e)
        {
            trenirovkaWindow = new Trenirovka();
            trenirovkaWindow.Show();
            Close();
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
