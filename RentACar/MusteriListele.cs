using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RentACar
{
    public partial class MusteriListele : Form
    {
        public MusteriListele()
        {
            InitializeComponent();
        }
        private string connectionString = @"Data Source=DESKTOP-N8ABPSM;Initial Catalog=RentACar;Integrated Security=True";
        private void MusteriListele_Load(object sender, EventArgs e)
        {
            Musteri_Listele();
        }
        public void Musteri_Listele()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string komutCumlesi = "Select * From Musteriler";
            SqlCommand command = new SqlCommand(komutCumlesi, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTcNo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtMail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string komutCumlesi = "Update Musteriler set Ad_Soyad = @adsoyad, Telefon_Numarasi = @telno, Mail=@mail, Adres = @adres Where Tc_No=@tc";
            SqlCommand command = new SqlCommand(komutCumlesi, connection);
            command.Parameters.AddWithValue("@tc", txtTcNo.Text);
            command.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            command.Parameters.AddWithValue("@telno", maskedTextBox1.Text);
            command.Parameters.AddWithValue("@mail", txtMail.Text);
            command.Parameters.AddWithValue("@adres", txtAdres.Text);
            command.ExecuteNonQuery();
            connection.Close();
            Musteri_Listele();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string komutCumlesi = "Delete From Musteriler where Tc_No='" +
                                  dataGridView1.CurrentRow.Cells["Tc_No"].Value.ToString() + "'";
            SqlCommand command = new SqlCommand(komutCumlesi, connection);
            command.ExecuteNonQuery();
            connection.Close();
            Musteri_Listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
