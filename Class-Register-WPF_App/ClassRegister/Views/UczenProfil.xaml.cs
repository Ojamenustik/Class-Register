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
            imiev.Text = Usr.usss.imie;
            nazwiskov.Text = Usr.usss.nazwisko;
            var xdd= DBhelp.OcenyUczniaSrednia(Usr.usss.id).ToString();
            sredniav.Text = xdd;



        }
    }
}
