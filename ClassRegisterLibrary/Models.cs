using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegisterLibrary
{
        public class User
        {
            public int id { get; set; }
            public string user { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string typ { get; set; }
        }

        public class OcenyPrzedmiot
        {

            public string Przedmiot { get; set; }
        public string Dzień{ get; set; }
    public int Ocena { get; set; }
    }

        public class pomoc
        {

            public string Text { get; set; }
        public int Value { get; set; }


    }
    public class obecnos
    {

        public string Text { get; set; }
        public string Value { get; set; }


    }

    public class Uzytkownik
        {
            public int id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko{ get; set; }

}

        
    }


