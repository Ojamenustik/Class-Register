﻿using System;
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

namespace ClassRegister
{
    /// <summary>
    /// Interaction logic for DashboardNauczyciel.xaml
    /// </summary>
    public partial class DashboardNauczyciel : Window
    {
        public DashboardNauczyciel()
        {
            InitializeComponent();
        }
        private void FaceBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AccountBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();

        }
        private void BellBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new NauczycielDashboardModel();

        }

        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new NauczycielProfilModel();
        }

        private void Wykresy_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new NauczycielWykresyModel();
        }

        private void Dziennik_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new NauczycielDziennikModel();
        }

        private void OnLoadDashboard(object sender, RoutedEventArgs e)
        {
            DataContext = new NauczycielDziennikModel();
        }
    }
}
