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

namespace ClassRegister.Views
{
    /// <summary>
    /// Interaction logic for NauczycielProfil.xaml
    /// </summary>
    public partial class NauczycielProfil : UserControl
    {
        public NauczycielProfil()
        {
            InitializeComponent();
            imie.Text = Usr.usss.imie;
            nazwisko.Text = Usr.usss.nazwisko;
        }
    }
}
