using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassRegisterLibrary;

using System.Data;
using System.IO;


namespace ClassRegister
{
    /// <summary>
    /// Klasa <c>DBhelp</c> jest kolekcja metod statycznych wykorzystujacych 
    /// klasy biblioteki <c>System.Data.SQLite</c> do komunikacji z baza danych CRDB2.db
    /// zwlaszcza do pobierania z niej danych do wyswietlania w formie wykresow 
    /// </summary>
    public static class DBhelp
    {



        static string ConString = $@"Data Source = {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\CRDB2.db")}";
        
       /// <summary>
       /// Metoda Login odpowiada za logowanie do bazy danych
       /// </summary>
       /// <param name="user"> nazwa uzytkownika </param>
       /// <param name="pass"> haslo uzytkownika </param>
       /// <returns>obiekt <c>User</c></returns>
        public static User Login(string user,string pass)
        {
            User ret = null;
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                //tworze obiekt com klasy SQLiteCommand ktor ma w tym przeciązeniu te same parametry co DataAdapter
                SQLiteCommand com = new SQLiteCommand("select * from Login l join Uzytkownik u on l.id = u.id", con);
                con.Open();
                //DataReader czyta wiersz po wierszu tabelę z com czyli całą LogData z bazy login.db
                SQLiteDataReader reader = com.ExecuteReader();
                

                //dopoki reader czyta wiersze...
                while (reader.Read())
                {
                    //...sprawdz czy podany przez usera login i password zgadzają sie z tymi z wiersza
                    if (reader.GetString(0) == user && reader.GetString(1) == pass)
                    {
                        ret = new User();
                        ret.id = reader.GetInt32(2);
                        ret.user = reader.GetString(0);
                        ret.imie = reader.GetString(5);
                        ret.nazwisko = reader.GetString(4);
                        ret.typ = reader.GetString(6); 


                    }
                }
                //jesli sie nie zgadzaja, pokaz MessageBox z informacją ze nie ma w LogData takiego wiersza
                //w ktorym i login i password odpowiadaja tym podanym przez usera
                 
                reader.Close();

            }
            return ret;
        }
        /// <summary>
        /// Metoda OcenyUczniaSrednia oblicza srednia ocen dla ucznia o podanym id
        /// Jesli uczen nie ma ocen, srednia = 0.0
        /// </summary>
        /// <param name="uId">id ucznia</param>
        /// <returns>srednia ocen ucznia typ <c>double</c></returns>
        public static double OcenyUczniaSrednia(int uId)
        {
            double ret = 0.0;
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {

                SQLiteCommand com = new SQLiteCommand(
                    "Select  avg(Ocena) as value from OcenyUcznia where Iducznia ="+ uId.ToString(),
                    con);
                con.Open();

                SQLiteDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.IsDBNull(0) == true)
                        ret = 0.0;
                    else
                    ret = Math.Round(reader.GetDouble(0),2);

                }
            }
            return ret;
        }
        /// <summary>
        /// Metoda SredniaKlasy dodaje do listy srednia ocen i nazwe klasy dla ktorej srednia jest liczona
        /// jesli srednia ocen dla danej klasy nie moze byc wyliczona ze wzgledu na brak ocen uczniow, do listy
        /// dodawana jest srednia 0.0
        /// </summary>
        /// <returns><c>List</c> obiektow <c>srednklasy</c></returns>
        public static List<srednklasy> SredniaKlasy( )
        {
            List<srednklasy> ret = new List<srednklasy>();
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {

                SQLiteCommand com = new SQLiteCommand(
                    @"Select  avg(Ocena) as value , k.nazwa from OcenyUcznia o
                    join Uzytkownik u on o.IdUcznia=u.id
                    Join Klasa k on u.klasaId=k.IdKlasa
                    group by u.klasaId",
                    con);
                con.Open();

                SQLiteDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    double temp;
                    if (reader.IsDBNull(0) == true)
                        temp = 0.0;
                    else
                        temp = Math.Round(reader.GetDouble(0), 2);

                    ret.Add(new srednklasy() { srednia = temp, klasa = reader.GetString(1) });
                }
            }
            return ret;
        }
        /// <summary>
        /// Metoda ObecnosciKlasy podaje zagregowana ilosc obecnosci
        /// lub nieobecnosci wraz z nazwa klasy dla ktorej liczona jest obecnosc
        /// </summary>
        /// <param name="czy">dla czy=0 nieobecnosci, dla czy=1 obecnosci</param>
        /// <returns><c>List</c> nazwa klasy + ilosc obecnych/nieobecnych</returns>
        public static List<pomoc> ObecnosciKlasy(int czy)
        {
            List<pomoc> ret = new List<pomoc>();
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {

                SQLiteCommand com = new SQLiteCommand(
                    @"Select  k.nazwa, count(o.JestObecny) as obecnosc from Obecnosc o
                     join Uzytkownik u on o.IdUcznia=u.id
                     Join Klasa k on u.klasaId=k.IdKlasa
                     Where o.JestObecny="+ czy.ToString()+ " group by u.klasaId ",
                    con);
                con.Open();

                SQLiteDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                   

                    ret.Add(new pomoc() { Text= reader.GetString(0),Value= reader.GetInt32(1) });
                }
            }
            return ret;
        }
        /// <summary>
        /// Metoda OcenyUcznia zwraca ilosc kazdej z ocen dla ucznia o wybranym id
        /// </summary>
        /// <param name="uId">id ucznia</param>
        /// <returns><c>Dictionary</c> para (<c>int</c> ocena, <c>int</c> count(ocena))</returns>
        public static Dictionary<int, int> OcenyUcznia(int uId)
        {
            Dictionary<int, int> ret = new Dictionary<int, int>();
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                
                SQLiteCommand com = new SQLiteCommand(
                    "SELECT Ocena, COUNT(Ocena) AS ilosc FROM OcenyUcznia Where IdUcznia = "+ uId.ToString()+ " GROUP BY Ocena",
                    con);
                con.Open();
                
                SQLiteDataReader reader = com.ExecuteReader();

                
               
                while (reader.Read())
                {
                    
                        ret.Add(reader.GetInt32(0), reader.GetInt32(1));
                       
                }
               

                reader.Close();

            }
            return ret;
        }
        /// <summary>
        /// Metoda Przedmioty zwraca z bazy rekordy z tabeli Przedmioty
        /// </summary>
        /// <returns><c>Dictionary</c> para (<c>int</c> id, <c>string</c> nazwa przedmiotu)</returns>
        public static Dictionary<int, string> Przedmioty()
        {
            Dictionary<int, string> ret = new Dictionary<int, string>();

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
               
                SQLiteCommand com = new SQLiteCommand(
                    "SELECT * from Przedmioty",
                    con);
                con.Open();
                
                SQLiteDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    ret.Add(reader.GetInt32(0), reader.GetString(1));
                }

                reader.Close();
            }
            return ret;
        }
        /// <summary>
        /// Metoda Obecnoscuczen kataloguje obecnosci w konkretnych dniach ucznia o podanym id
        /// dla JestObecny = 0 rejestrowana jest nieobecnosc, dla JestObecny = 1 obecnosc
        /// w obiekcie obecnos z wlasciwosciami <c>string</c><c>Value</c>=Obecny/Nieobecny
        /// i <c>string</c><c>Text</c>=Data
        /// </summary>
        /// <param name="id">id ucznia</param>
        /// <returns><c>List</c> <c>obecnos</c></returns>
        public static List<obecnos> Obecnoscuczen(int id)
        {

            List<obecnos> ret = new List<obecnos>();
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                int i = 0;
                SQLiteCommand com = new SQLiteCommand(
                    "select JestObecny, Dzien from Obecnosc  Where IdUcznia="+ id,
                    con);
                con.Open();

                SQLiteDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetInt32(0)==1)
                    ret.Add(new obecnos() { Value = "Obecny", Text = reader.GetString(1) });
                    else
                        ret.Add(new obecnos() { Value = "nieobecny", Text = reader.GetString(1) });
                }

                reader.Close();
            }
            return ret;
        }
        /// <summary>
        /// Metoda OcenyPrzedmioty zapisuje do <c>List</c> obiekty <c>OcenyPrzedmiot</c> 
        /// z ocena z przedmiotu ktora otrzymal uczen o podanym id oraz date w ktora ta ocene otrzymal
        /// </summary>
        /// <param name="tempVal">lista id przedmiotow</param>
        /// <param name="uid">id ucznia</param>
        /// <returns><c>List</c><c>OcenyPrzedmiot</c></returns>
        public static List<OcenyPrzedmiot> OcenyPrzedmioty(List<int> tempVal,int uid)
        {

            List<OcenyPrzedmiot> ret = new List<OcenyPrzedmiot>();
            foreach (int temp in tempVal) { 
                using (SQLiteConnection con = new SQLiteConnection(ConString))
                {
                    SQLiteCommand com = new SQLiteCommand(
                        @"SELECT p.Przedmiot, ou.Ocena, ou.Dzien from OcenyUcznia ou join Przedmioty p 
                          on p.IdPrzedmiotu = ou.IdPrzedmiotu where IdUcznia = "+uid.ToString()+" and p.IdPrzedmiotu="+temp.ToString(),con);

                    con.Open();
                    
                    SQLiteDataReader reader = com.ExecuteReader();
                    
                    while (reader.Read())
                    {

                        ret.Add(new OcenyPrzedmiot(){ Przedmiot= reader.GetString(0),Ocena= reader.GetInt32(1),Dzień= reader.GetString(2) });

                    }
                    reader.Close();
                }
            }
            return ret;
        }
        /// <summary>
        /// Metoda OcenyPrzedmioty kataloguje oceny wraz z imionami i nazwiskami uczniow oraz dniem w jakim
        /// je otrzymali dla wybranej klasy
        /// </summary>
        /// <param name="klasa">id klasy</param>
        /// <returns><c>List</c> obiektow <c>OcenyNauczyciel</c></returns>
        public static List<OcenyNauczyciel> OcenyPrzedmioty( int klasa)
        {

            List<OcenyNauczyciel> ret = new List<OcenyNauczyciel>();
            
                using (SQLiteConnection con = new SQLiteConnection(ConString))
                {
                    SQLiteCommand com = new SQLiteCommand(
                        @"select p.Przedmiot, u.Imie,u.Nazwisko,o.Dzien, o.Ocena from OcenyUcznia o
                        join Uzytkownik u on u.id=o.IdUcznia 
                        join Przedmioty p on p.IdPrzedmiotu=o.IdPrzedmiotu
                        where u.klasaId="+klasa.ToString(), con);

                    con.Open();

                    SQLiteDataReader reader = com.ExecuteReader();

                    while (reader.Read())
                    {

                    ret.Add(new OcenyNauczyciel() { Przedmiot = reader.GetString(0), Imie = reader.GetString(1), Nazwisko=reader.GetString(2), Dzien = reader.GetString(3), Ocena = reader.GetInt32(4),  });

                    }
                    reader.Close();
                
            }
            return ret;
        }
        /// <summary>
        /// Metoda ObecnosciKlasa kataloguje obecnosci w dany dzien wraz z imionami i nazwiskami uczniow 
        /// dla wybranej klasy; jesli uczen nie jest obecny, <c>ObecnoscNauczyciel.Obecnosc="nie"</c>
        /// </summary>
        /// <param name="klasa">id klasy</param>
        /// <returns><c>List</c> obiektow <c>ObecnoscNauczyciel</c></returns>
        public static List<ObecnoscNauczyciel> ObecnosciKlasa(int klasa)
        {

            List<ObecnoscNauczyciel> ret = new List<ObecnoscNauczyciel>();

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                SQLiteCommand com = new SQLiteCommand(
                    @"select u.Imie,u.Nazwisko,o.Dzien, o.JestObecny from Obecnosc o
join Uzytkownik u on u.id=o.IdUcznia 
where u.klasaId=" + klasa.ToString(), con);

                con.Open();

                SQLiteDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    string czy;
                    if (reader.GetInt32(3) == 1)
                        czy = "tak";
                    else
                        czy = "nie";
                    ret.Add(new ObecnoscNauczyciel() { Imie = reader.GetString(0), Nazwisko = reader.GetString(1), Dzien = reader.GetString(2), Obecnosc = czy });

                }
                reader.Close();

            }
            return ret;
        }
        /// <summary>
        /// Metoda Klasy zwraca tabele Klasa w formie listy par id, nazwa klasy
        /// </summary>
        /// <param name="klasa">id klasy</param>
        /// <returns><c>List</c> obiektow <c>pomoc</c></returns>
        public static List<pomoc> Klasy(int klasa)
        {

            List<pomoc> ret = new List<pomoc>();

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                SQLiteCommand com = new SQLiteCommand(
                    @"select * from Klasa", con);

                con.Open();

                SQLiteDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {

                    ret.Add(new pomoc() { Value = reader.GetInt32(0),Text= reader.GetString(1) });

                }
                reader.Close();

            }
            return ret;
        }

        /// <summary>
        /// Metoda Uczniowie zwraca tabele Uzytkownik w formie listy uczniow z ich id, imieniem, nazwiskiem
        /// (z wylaczeniem Uzytkownikow o typie "nauczyciel")
        /// </summary>
        /// <returns><c>List</c> obiektow <c>Uzytkownik</c></returns>
        public static List<Uzytkownik> Uczniowie()
        {
            List<Uzytkownik> ret = new List<Uzytkownik>();
            
                using (SQLiteConnection con = new SQLiteConnection(ConString))
                {

                    SQLiteCommand com = new SQLiteCommand(
                        "SELECT * from Uzytkownik where typ != 'nauczyciel'",
                        con);
                    con.Open();

                    SQLiteDataReader reader = com.ExecuteReader();

                    while (reader.Read())
                    {

                        ret.Add(new Uzytkownik() { id = reader.GetInt32(0), Imie = reader.GetString(1), Nazwisko = reader.GetString(2) });

                    }

                    reader.Close();
               
            }
            return ret;
        }
        /// <summary>
        /// Metoda Dodajocene dodaje do tabeli OcenyUcznia nowa ocene
        /// </summary>
        /// <param name="idu">id ucznia</param>
        /// <param name="idp">id przedmiotu</param>
        /// <param name="oce">ocena <c>int</c></param>
        /// <param name="date">data</param>
        /// <returns></returns>
        public static int Dodajocene(int idu,int idp,int oce,string date)
        {
            int reader = 0;
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {

                SQLiteCommand com = new SQLiteCommand(
                    "insert into OcenyUcznia (IdUcznia,IdPrzedmiotu,Ocena,Dzien) values("+idu.ToString()+","+idp.ToString() + ","+oce.ToString() + ",'"+date.ToString() + "')",
                    con);
                con.Open();

                 reader = com.ExecuteNonQuery();

            }
            return reader;
        }
        /// <summary>
        /// Metoda DodajObecnosc dodaje do tabeli Obecnosc nowa obecnosc w podanym dniu dla podanego ucznia
        /// obecnosc=0 uczen nieobecny, obecnosc=1 uczen obecny
        /// </summary>
        /// <param name="idu">id ucznia</param>
        /// <param name="obecnosc">obecnosc</param>
        /// <param name="date">data</param>
        /// <returns></returns>
        public static int DodajObecnosc(int idu,  int obecnosc, string date)
        {  
            int reader = 0;
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {

                SQLiteCommand com = new SQLiteCommand(
                    "insert into Obecnosc (IdUcznia,JestObecny,Dzien) values(" + idu + ","  + obecnosc + ",'" + date + "')",
                    con);
                con.Open();

                reader = com.ExecuteNonQuery();
            }
            return reader;
        }

    }
}
