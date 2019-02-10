using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassRegisterLibrary;

namespace ClassRegister.Views
{
    /// <summary>
    /// Interaction logic for NauczycielWykresy.xaml
    /// </summary>
    public partial class NauczycielWykresy : UserControl
    {
        public NauczycielWykresy()
        {
            InitializeComponent();
            mcChart.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.DataVisualization.Charting.ColumnSeries ColumnSeries = new System.Windows.Controls.DataVisualization.Charting.ColumnSeries();
            List<srednklasy> wy1=  DBhelp.SredniaKlasy();
            mcChart.Visibility = Visibility.Visible;
            mcCharobec.Visibility = Visibility.Hidden;
            mcCharobec2.Visibility = Visibility.Hidden;
            ((BarSeries)mcChart.Series[0]).ItemsSource = wy1;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mcChart.Visibility = Visibility.Hidden;
            mcCharobec.Visibility = Visibility.Visible;
            mcCharobec2.Visibility = Visibility.Hidden;
            List<pomoc> wy2= DBhelp.ObecnosciKlasy(1);
            ((PieSeries)mcCharobec.Series[0]).ItemsSource = wy2;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mcChart.Visibility = Visibility.Hidden;
            mcCharobec.Visibility = Visibility.Hidden;
            mcCharobec2.Visibility = Visibility.Visible;
            List<pomoc> wy3 = DBhelp.ObecnosciKlasy(0);
            ((PieSeries)mcCharobec2.Series[0]).ItemsSource = wy3;
        }
        private void OnLoadTable(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.DataVisualization.Charting.ColumnSeries ColumnSeries = new System.Windows.Controls.DataVisualization.Charting.ColumnSeries();
            List<srednklasy> wy1 = DBhelp.SredniaKlasy();
            mcChart.Visibility = Visibility.Visible;
            mcCharobec.Visibility = Visibility.Hidden;
            mcCharobec2.Visibility = Visibility.Hidden;
            ((BarSeries)mcChart.Series[0]).ItemsSource = wy1;
        }
    }
}
