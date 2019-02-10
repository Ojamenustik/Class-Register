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
using ClassRegisterLibrary;
 

namespace ClassRegister
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = DBhelp.Login(UserNameTb.Text, PasswordTb.Password);
            if (user != null)
            {
                if (user.typ == "uczen")
                {
                    DashboardUczen dashboardUczen = new DashboardUczen(user);
                    dashboardUczen.Show();
                }else if (user.typ == "nauczyciel")
                {
                    DashboardNauczyciel dashboardNauczyciel = new DashboardNauczyciel();
                    dashboardNauczyciel.Show();
                }
                
                this.Close();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
