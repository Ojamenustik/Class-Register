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
        string ConString = @"Data Source=C:\Users\Mięso\source\repos\ClassRegister\CRDB.db";
        public Form1()
         {
                InitializeComponent();
                using (SQLiteConnection con = new SQLiteConnection(ConString))
                {
                    con.Open();
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter("select * from Klasa", con);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgv.DataSource = table;
                }
            }
        
        
    }
}
