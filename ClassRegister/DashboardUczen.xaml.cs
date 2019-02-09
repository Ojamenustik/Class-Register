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
using ClassRegister.ViewModels;
using ClassRegister.Views;
using ClassRegisterLibrary;

namespace ClassRegister
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class DashboardUczen : Window
    {
        public DashboardUczen()
        {
            InitializeComponent();
            
        }

        private void BellBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new UczenDashboardModel();
            
           
        }

        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new UczenProfilModel();
        }

        private void Wykresy_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new UczenWykresyModel();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    ObjectQuery<Product> products = dataEntities.Products;

        //    var query =
        //        from product in products
        //        where product.Color == "Red"
        //        orderby product.ListPrice
        //        select new { product.Name, product.Color, CategoryName = product.ProductCategory.Name, product.ListPrice };

        //    dataGrid1.ItemsSource = query.ToList();
        //}
    }
}
