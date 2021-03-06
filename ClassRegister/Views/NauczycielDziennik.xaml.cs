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
    /// Interaction logic for NauczycielDziennik.xaml
    /// </summary>
    public partial class NauczycielDziennik : UserControl
    {
        public NauczycielDziennik()
        {
            InitializeComponent();
            Datadzis.Text= DateTime.Now.ToString("M/d/yyyy");
            przedmiot.ItemsSource = DBhelp.Przedmioty();
            uczen.ItemsSource = DBhelp.Uczniowie();
            uczen2.ItemsSource = DBhelp.Uczniowie();
            ocena.ItemsSource = new int []{ 1, 2, 3, 4, 5, 6 };
            Dictionary<int, string> czyobecny = new Dictionary<int, string>();
            czyobecny.Add(0, "Nie");
            czyobecny.Add(1, "Tak");
            obecnosc.ItemsSource = czyobecny;

        }

        private void dodajocene_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Uzytkownik usr = (Uzytkownik)uczen.SelectedItem;
                KeyValuePair<int, string> przedmiotv = (KeyValuePair<int, string>)przedmiot.SelectedValue;
                int ocenav = ocena.SelectedIndex + 1;
                string datav = data.SelectedDate.Value.ToShortDateString();
                DBhelp.Dodajocene(usr.id, przedmiotv.Key, ocenav, datav);
            }
            catch (NullReferenceException exception)
            {

            }
           
        }

        private void dodajobecnosc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Uzytkownik usr = (Uzytkownik)uczen2.SelectedItem;
                int czy = obecnosc.SelectedIndex;
                string datav = data2.SelectedDate.Value.ToShortDateString();
                DBhelp.DodajObecnosc(usr.id, czy, datav);
                dziennik.ItemsSource = DBhelp.ObecnosciKlasa(1);
            }
            catch (NullReferenceException exception)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            dziennik.ItemsSource = DBhelp.ObecnosciKlasa(1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dziennik.ItemsSource = DBhelp.ObecnosciKlasa(2);
        }

        private void OnLoadTable(object sender, RoutedEventArgs e)
        {
            dziennik.ItemsSource = DBhelp.ObecnosciKlasa(1);
        }
    }
}
