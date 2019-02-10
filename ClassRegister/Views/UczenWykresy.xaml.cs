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
    /// Interaction logic for UczenWykresy.xaml
    /// </summary>
    public partial class UczenWykresy : UserControl
    {
        public UczenWykresy()
        {
            InitializeComponent();
           
            
            


        }
        void wykres1()
        {

            List<KeyValuePair<int, int>> valueList = new List<KeyValuePair<int, int>>();


            Dictionary<int, int> db = DBhelp.OcenyUcznia(MainWindow.user.id);
            foreach (KeyValuePair<int, int> temp in db)
            {
                valueList.Add(temp);
            }
            //Setting data for line chart
           
            
            mcChart.Visibility = Visibility.Hidden;
            mcChartpie.Visibility = Visibility.Visible;
            ((PieSeries)mcChartpie.Series[0]).ItemsSource = valueList;
        }
        void wykres2()
        {
            List<KeyValuePair<string, int>> valueList = new List<KeyValuePair<string, int>>();
            List<Uzytkownik> users = DBhelp.Uczniowie();


            Dictionary<string, int> tt = new Dictionary<string, int>();

            foreach (Uzytkownik temp in users)
            {
                
                double srednia = DBhelp.OcenyUczniaSrednia(temp.id);

                if (srednia == 0) continue;
                if(temp.id!=MainWindow.user.id)
                tt.Add(temp.Nazwisko, (int)srednia);
                else
                    tt.Add("JA", (int)srednia);


            }

            System.Windows.Controls.DataVisualization.Charting.ColumnSeries ColumnSeries = new System.Windows.Controls.DataVisualization.Charting.ColumnSeries();
            //columnChart.DataContext = tt;
            //Setting data for line chart
            // pieChart.Visibility = Visibility.Hidden;


            
            mcChartpie.Visibility = Visibility.Hidden;
            mcChart.Visibility = Visibility.Visible;
            ((BarSeries)mcChart.Series[0]).ItemsSource = tt;
            //columnChart.Visibility = Visibility.Visible;



        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            wykres2();
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            wykres1();
        }
    }
}
