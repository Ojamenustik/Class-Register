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
        User guser;
        //utworzenie tzw. ConnectionString do połączenia z bazą; ConnectionString zawiera lokalny adres bazy
        //jak widać w poniższym DataSource=lokalizacja\NazwaBazy.db ("db" to rozszerzenie pliku bazodanowego w SQLite)
        string ConString = @"Data Source=C:\Users\anngo\Desktop\WSEI STUDIA\Class-Register-master\CRDB.db";
        public Form1(User user1)
        {
            guser = user1;
            InitializeComponent();
            
            label1.Text = "Zalogowano jako: " + user1.imie + " " + user1.nazwisko;

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

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            DrawPieChart(DBhelp.OcenyUcznia(guser.id));
        }
        private void tabPage3_Enter(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();

            Dictionary<int, string> przed = DBhelp.Przedmioty();
            checkedListBox1.DisplayMember = "Text";
            checkedListBox1.ValueMember = "Value";
            int i = 0;
            foreach (KeyValuePair<int, string> temp in przed)
            {
                checkedListBox1.Items.Insert(i, new { Text = temp.Value, Value = temp.Key });
                i++;
            }

        }

        private void mojeDaneToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> tempVal = new List<int>();

            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                var checkedItems = itemChecked;
                var type = checkedItems.GetType();
                var props = type.GetProperties();
                var pairs = props.Select(x => x.GetValue(checkedItems, null)).ToArray();
                tempVal.Add((int)pairs[1]);

                //int? id = (int)castedItem["Value"];
            }
            List<OcenyPrzedmiot> occ = DBhelp.OcenyPrzedmioty(tempVal);
            dataGridView1.Rows.Clear();
            DataGridViewRow newRow = (DataGridViewRow)dataGridView1.RowTemplate.Clone();
            newRow.CreateCells(dataGridView1);

            foreach (OcenyPrzedmiot temp in occ)
            {

                dataGridView1.Rows.Add(temp.Przedmiot, temp.Ocena, temp.Dzień);
            }
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            Dictionary<int, string> help = DBhelp.Przedmioty();
            foreach (KeyValuePair<int, string> temp in help)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = temp.Value;
                item.Value = temp.Key;
                comboBox1.Items.Add(item);
            }

            List<Uzytkownik> uz = DBhelp.Uczniowie();
            foreach (Uzytkownik temp in uz)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = temp.Imie + " " + temp.Nazwisko;
                item.Value = temp.id;
                comboBox3.Items.Add(item);
                comboBox2.Items.Add(item);
            }
            ComboboxItem item1 = new ComboboxItem();
            item1.Text = "Tak";
            item1.Value = 1;
            ComboboxItem item2 = new ComboboxItem();
            item2.Text = "Nie";
            item2.Value = 1;
            comboBox4.Items.Add(item1);
            comboBox4.Items.Add(item2);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            ComboboxItem przedmiot = (ComboboxItem)comboBox1.SelectedItem;
            ComboboxItem user = (ComboboxItem)comboBox1.SelectedItem;
            int ocena = (int)numericUpDown1.Value;
            string data = dateTimePicker1.Value.ToShortDateString();
            var insert = DBhelp.Dodajocene((int)user.Value, (int)przedmiot.Value, ocena, data);
            if (insert != 0)
            {
                MessageBox.Show("Dodano");
            }
            else { MessageBox.Show("bląd"); }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            ComboboxItem user = (ComboboxItem)comboBox3.SelectedItem;
            ComboboxItem obecnosc = (ComboboxItem)comboBox4.SelectedItem;
            
            string data = dateTimePicker2.Value.ToShortDateString();
            var insert = DBhelp.DodajObecnosc((int)user.Value, (int)obecnosc.Value,  data);
            if (insert != 0)
            {
                MessageBox.Show("Dodano");
            }
            else { MessageBox.Show("bląd"); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (guser.typ == "uczen")
            {
                menuToolStripMenuItem.Visible = false;
                tabControl1.TabPages.Remove(tabPage1);
                
                tabControl1.TabPages.Remove(tabPage4);
                
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
