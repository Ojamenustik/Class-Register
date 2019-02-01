using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ClassRegister
{
    public partial class Form1 : Form
    {
        //utworzenie tzw. ConnectionString do połączenia z bazą; ConnectionString zawiera lokalny adres bazy
        //jak widać w poniższym DataSource=lokalizacja\NazwaBazy.db ("db" to rozszerzenie pliku bazodanowego w SQLite)
        string ConString = @"Data Source=C:\Users\Mięso\source\repos\ClassRegister\CRDB.db";
        public Form1()
         {
                InitializeComponent();
            //utworzenie nowego połączenia "con" z parametrem ConString, czyli łączenie z naszą bazą CRDB.db
                using (SQLiteConnection con = new SQLiteConnection(ConString))
                {
                //otwarcie połączenia metodą Open()
                    con.Open();
                //DataAdapter zczytuje dane ze źródła (w konstruktorze parametry commandText i connection)
                //commandText="select*from Klasa" - fajna sprawa, uzywa sie komend SQLowych wiec nic nowego
                //connection=con - połączenie z naszą bazą które stworzyłam wyzej
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter("select * from Klasa", con);

                //DataTable tworzy w pamięci obiekt mający przechowywać pobrane za pomoca DataAdaptera dane
                DataTable table = new DataTable();
                //Fill() wypełnia DataTable table danymi z tabeli Klasa
                    adapter.Fill(table);
                //dostęp do właściwosci DataSource naszej kontrolki DataGridView w oknie Form1
                //DataGridView wyswietli pobrane do table dane w formie grid czyli siatki 
                //DataSource co robi wystarczy najechać kursorem - sets the data source that DataGridView is displaying data for
                    dgv.DataSource = table;
                }
            }
        
        
    }
}
