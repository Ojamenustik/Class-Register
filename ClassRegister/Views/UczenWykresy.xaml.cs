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



            mcChart.Visibility = Visibility.Hidden;
            mcChartpie.Visibility = Visibility.Hidden;
            mcCharobec.Visibility = Visibility.Hidden;
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
            mcCharobec.Visibility = Visibility.Hidden;
            mcChartpie.Visibility = Visibility.Visible;
            ((PieSeries)mcChartpie.Series[0]).ItemsSource = valueList;
        }
        void wykres3()
        {

            

            List<obecnos> db = DBhelp.Obecnoscuczen(MainWindow.user.id);
            int obecny = db.Where(t => t.Value == "Obecny").Count();
            int nobecny = db.Where(t => t.Value == "nieobecny").Count();
            List<pomoc> ret = new List<pomoc>();
            ret.Add(new pomoc() { Value = obecny, Text = "obecny" });
            ret.Add(new pomoc() { Value = nobecny, Text = "nieobecny" });
            mcChart.Visibility = Visibility.Hidden;
            mcChartpie.Visibility = Visibility.Hidden;
            mcCharobec.Visibility = Visibility.Visible;
            ((PieSeries)mcCharobec.Series[0]).ItemsSource = ret;




        }
        void wykres2()
        {
            List<KeyValuePair<string, double>> valueList = new List<KeyValuePair<string, double>>();
            List<Uzytkownik> users = DBhelp.Uczniowie();


            Dictionary<string, double> tt = new Dictionary<string, double>();

            foreach (Uzytkownik temp in users)
            {
                
                double srednia = DBhelp.OcenyUczniaSrednia(temp.id);

                if (srednia == 0) continue;
                if(temp.id!=MainWindow.user.id)
                tt.Add(temp.Nazwisko, srednia);
                else
                    tt.Add("JA", srednia);


            }

            System.Windows.Controls.DataVisualization.Charting.ColumnSeries ColumnSeries = new System.Windows.Controls.DataVisualization.Charting.ColumnSeries();
            //columnChart.DataContext = tt;
            //Setting data for line chart
            // pieChart.Visibility = Visibility.Hidden;


            mcCharobec.Visibility = Visibility.Hidden;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            wykres3();
        }
    }
}
