using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRegister
{
    public class User
    {
        public  int id;
        public  string user, imie, nazwisko,typ;
    }
    public class OcenyPrzedmiot
    {
        
        public string  Przedmiot, Dzień;
        public int Ocena;
    }
    public class pomoc
    {
         
        public string Text;
        public int Value;


    }
    public class Uzytkownik
    {
        public int id;
        public string Imie,Nazwisko;
        


    }
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
