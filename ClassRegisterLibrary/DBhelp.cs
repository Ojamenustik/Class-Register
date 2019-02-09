﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassRegisterLibrary;
using System.Data;

namespace ClassRegister
{
    public class DBhelp
    {
        static string ConString = @"Data Source = E:\C#_projects\Aniak\ClassRegister\Class-Register\CRDB2.db";

       /// <summary>
       /// Logowanie do bazy danych
       /// </summary>
       /// <param name="user"> nazwa uzytkownika </param>
       /// <param name="pass"> haslo uzytkownika </param>
       /// <returns></returns>
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

        public static List<DataTable> OcenyPrzedmioty(List<int> tempVal)
        {
            
            List<DataTable> ret = new List<DataTable>();
            int i = 0;
            foreach (int temp in tempVal) { 
                using (SQLiteConnection con = new SQLiteConnection(ConString))
                {
                    SQLiteCommand com = new SQLiteCommand(
                        $@"SELECT p.Przedmiot, ou.Ocena, ou.Dzien from OcenyUcznia ou join Przedmioty p 
                          on p.IdPrzedmiotu = ou.IdPrzedmiotu where IdUcznia = 1 and p.IdPrzedmiotu={temp}",con);

                    con.Open();
                    ret.Add(new DataTable());
                    //SQLiteDataReader reader = com.ExecuteReader();
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(com);

//                    while (reader.Read())
//                    {
//
//                        ret.Add(new OcenyPrzedmiot(){ Przedmiot= reader.GetString(0),Ocena= reader.GetInt32(1),Dzień= reader.GetString(2) });
//
//                    }
//                    reader.Close();
                    dataAdapter.Fill(ret[i]);
                    ++i;
                }
            }
            return ret;
        }

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

        public static int Dodajocene(int idu,int idp,int oce,string date)
        {
            int reader = 0;
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {

                SQLiteCommand com = new SQLiteCommand(
                    "insert into OcenyUcznia (IdUcznia,IdPrzedmiotu,Ocena,Dzien) values("+idu+","+idp+","+oce+",'"+date+"')",
                    con);
                con.Open();

                 reader = com.ExecuteNonQuery();

            }
            return reader;
        }

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