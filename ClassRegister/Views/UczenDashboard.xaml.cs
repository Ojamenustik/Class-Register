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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassRegisterLibrary;

namespace ClassRegister.Views
{
    /// <summary>
    /// Interaction logic for UczenDashboard.xaml
    /// </summary>
    public partial class UczenDashboard : UserControl
    {
        public UczenDashboard()
        {
            InitializeComponent();
        }

        public List<OcenyPrzedmiot> op(int i)
        {
            List<int> przedmioty = new List<int>();
            if (i == 0)
                for (int e = 1; e < 9; e++)
                    przedmioty.Add(e);
            else
                przedmioty.Add(i);

            List<OcenyPrzedmiot> ret = new List<OcenyPrzedmiot>();
            List<ClassRegisterLibrary.OcenyPrzedmiot> xd = DBhelp.OcenyPrzedmioty(przedmioty);
            foreach (ClassRegisterLibrary.OcenyPrzedmiot temp in xd)
            {
                ret.Add(new OcenyPrzedmiot() { Dzień = temp.Dzień, Ocena = temp.Ocena, Przedmiot = temp.Przedmiot });

            }
            return ret;
        }

        private void allb_Click(object sender, RoutedEventArgs e)
        {



            List<OcenyPrzedmiot> ret = op(0);

            ocenyucznia.ItemsSource = ret;
        }

        private void matb_Click(object sender, RoutedEventArgs e)
        {
            List<OcenyPrzedmiot> ret = op(1);

            ocenyucznia.ItemsSource = ret;
        }

        private void chemb_Click(object sender, RoutedEventArgs e)
        {
            List<OcenyPrzedmiot> ret = op(5);

            ocenyucznia.ItemsSource = ret;
        }

        private void polb_Click(object sender, RoutedEventArgs e)
        {
            List<OcenyPrzedmiot> ret = op(3);

            ocenyucznia.ItemsSource = ret;
        }

        private void angb_Click(object sender, RoutedEventArgs e)
        {
            List<OcenyPrzedmiot> ret = op(2);

            ocenyucznia.ItemsSource = ret;
        }

        private void wfb_Click(object sender, RoutedEventArgs e)
        {
            List<OcenyPrzedmiot> ret = op(6);

            ocenyucznia.ItemsSource = ret;
        }

        private void bio_Click(object sender, RoutedEventArgs e)
        {
            List<OcenyPrzedmiot> ret = op(4);

            ocenyucznia.ItemsSource = ret;
        }

        private void fiz_Click(object sender, RoutedEventArgs e)
        {
            List<OcenyPrzedmiot> ret = op(7);

            ocenyucznia.ItemsSource = ret;
        }

        private void his_Click(object sender, RoutedEventArgs e)
        {
            List<OcenyPrzedmiot> ret = op(8);

            ocenyucznia.ItemsSource = ret;
        }
    }
}
