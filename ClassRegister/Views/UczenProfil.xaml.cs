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
    /// Interaction logic for UczenProfil.xaml
    /// </summary>
    public partial class UczenProfil : UserControl
    {
        public UczenProfil()
        {
            InitializeComponent();
            imiev.Text = MainWindow.user.imie;
            nazwiskov.Text = MainWindow.user.nazwisko; 
            var ocenyUczniaSrednia = DBhelp.OcenyUczniaSrednia(MainWindow.user.id).ToString();
            sredniav.Text = ocenyUczniaSrednia;



        }
    }
}
