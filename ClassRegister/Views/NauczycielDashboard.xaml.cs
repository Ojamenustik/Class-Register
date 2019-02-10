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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassRegister.Views
{
    /// <summary>
    /// Interaction logic for NauczycielDashboard.xaml
    /// </summary>
    public partial class NauczycielDashboard : UserControl
    {
        public NauczycielDashboard()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            oceny.ItemsSource = DBhelp.OcenyPrzedmioty(1);
            obecnosci.ItemsSource = DBhelp.ObecnosciKlasa(1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            oceny.ItemsSource = DBhelp.OcenyPrzedmioty(2);
            obecnosci.ItemsSource = DBhelp.ObecnosciKlasa(2);
        }
    }
}
