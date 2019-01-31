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

                SQLiteCommand com = new SQLiteCommand("select * from LogData", con);
                con.Open();
                SQLiteDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetString(1) == textBox1.Text && reader.GetString(2) == textBox2.Text)
                    {
                        Form1 form1 = new Form1();
                        this.Hide();
                        form1.Show();
                    }
                }
                reader.Close();

            }
        }
    }
}
