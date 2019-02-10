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
    /// Interaction logic for UczenProfil.xaml
    /// </summary>
    public partial class UczenProfil : UserControl
    {
        public UczenProfil()
        {
            InitializeComponent();
            imiev.Text = DashboardUczen.User.imie;
            nazwiskov.Text = DashboardUczen.User.nazwisko;
            var srednia = DBhelp.OcenyUczniaSrednia(DashboardUczen.User.id).ToString("F1");
            sredniav.Text = srednia;
        }
    }
}
