using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApp58
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        ///  kişilerin bilgilerini veir tabanına aktaran proje 
        /// </summary>
        /// 
        OleDbConnection bgln = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:" +
            "\\Users\\Markos\\Documents\\kısıler.accdb");
       
        OleDbCommand cmd = new OleDbCommand();
        private void verilerimigoster()
        {
            listView1.Items.Clear();
            
            bgln.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = bgln;
            komut.CommandText=("Select * From T1");
            // T1= tablonun ismi
            OleDbDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                // tablomda tanımladıgım kimlik, ad , soyad,yas sütunlarıyla belirttigim işlevi gerçekleştirir
                ekle.Text= oku["kimlik"].ToString();
                ekle.SubItems.Add(oku["ad"].ToString());
                ekle.SubItems.Add(oku["soyad"].ToString());
                ekle.SubItems.Add(oku["yas"].ToString());

                listView1.Items.Add(ekle);
            
            }

            bgln.Close();
         }

        private void button1_Click(object sender, EventArgs e)
        {
            verilerimigoster();

        }

        private void button2_Click(object sender, EventArgs e)
        {


            bgln.Open();
            OleDbCommand komut = new OleDbCommand("Insert into T1(kimlik,ad,soyad,yas) Values('"+textBox1.Text.ToString()+"','"+textBox2.Text.ToString()+"','"+textBox3.Text.ToString()+"','"+textBox4.Text.ToString()+"')", bgln);
            komut.ExecuteNonQuery();
            bgln.Close();
            verilerimigoster();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
          

            bgln.Open();
            cmd.Connection = bgln;
            int a = Convert.ToInt32(textBox5.Text);
            cmd.CommandText = "DELETE FROM T1 WHERE Kimlik = " + a;
            cmd.ExecuteNonQuery();
            bgln.Close();
            verilerimigoster();
        }

        private void button4_Click(object sender, EventArgs e)
        {

           
         
            bgln.Open();
            cmd.Connection = bgln;

            // Parametreli sorgu kullanımı
            cmd.CommandText = "UPDATE T1 SET ad = @ad, soyad = @soyad, yas = @yas WHERE Kimlik = @kimlik";
            cmd.Parameters.AddWithValue("@ad", textBox2.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox3.Text);
            cmd.Parameters.AddWithValue("@yas", textBox4.Text);
            cmd.Parameters.AddWithValue("@kimlik", Convert.ToInt32(textBox1.Text));

            cmd.ExecuteNonQuery();
            bgln.Close();
            verilerimigoster();


        }
    }
}
