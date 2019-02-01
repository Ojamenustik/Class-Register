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
    public partial class Form2 : Form
    {
        string ConString = @"data source=C:\Users\Mięso\source\repos\Ojamenustik\Class-Register\login.db";

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                //tworze obiekt com klasy SQLiteCommand ktor ma w tym przeciązeniu te same parametry co DataAdapter
                SQLiteCommand com = new SQLiteCommand("select * from LogData", con);
                con.Open();
                //DataReader czyta wiersz po wierszu tabelę z com czyli całą LogData z bazy login.db
                SQLiteDataReader reader = com.ExecuteReader();
                bool match = false;

                //dopoki reader czyta wiersze...
                while (reader.Read())
                {
                    //...sprawdz czy podany przez usera login i password zgadzają sie z tymi z wiersza
                    if (reader.GetString(1) == textBox1.Text && reader.GetString(2) == textBox2.Text)
                    {
                        Form1 form1 = new Form1();
                        this.Hide();
                        form1.Show();
                        match = true;
                    }
                }
                //jesli sie nie zgadzaja, pokaz MessageBox z informacją ze nie ma w LogData takiego wiersza
                //w ktorym i login i password odpowiadaja tym podanym przez usera
                if (match == false) { MessageBox.Show("Wrong Username or Password"); }
                reader.Close();

            }
        }
    }
}
