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
    public partial class AracListele : Form
    {
        public AracListele()
        {
            InitializeComponent();
        }

        private string ConnectionString = @"Data Source=DESKTOP-N8ABPSM;Initial Catalog=RentACar;Integrated Security=True";

        private void comboMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboMarka.SelectedIndex == 0)
            {
                comboSeri.Items.Clear();
                comboSeri.Text = "";

                comboSeri.Items.Add("320 i");
                comboSeri.Items.Add("M4");
                comboSeri.Items.Add("M5");
                comboSeri.Items.Add("M6");
                comboSeri.Items.Add("M8");
            }
            else if (comboMarka.SelectedIndex == 1)
            {
                comboSeri.Items.Clear();
                comboSeri.Text = "";

                comboSeri.Items.Add("C63");
                comboSeri.Items.Add("E180");
                comboSeri.Items.Add("E200");
                comboSeri.Items.Add("S300");
                comboSeri.Items.Add("C200");
            }
            else if (comboMarka.SelectedIndex == 2)
            {
                comboSeri.Items.Clear();
                comboSeri.Text = "";

                comboSeri.Items.Add("Şahin");
                comboSeri.Items.Add("Doğan");
                comboSeri.Items.Add("Doğan SLX");
                comboSeri.Items.Add("Serçe");
                comboSeri.Items.Add("Kartal");
                comboSeri.Items.Add("Murat");
            }
            else if (comboMarka.SelectedIndex == 3)
            {
                comboSeri.Items.Clear();
                comboSeri.Text = "";

                comboSeri.Items.Add("Dokker");
                comboSeri.Items.Add("Sandero");
                comboSeri.Items.Add("Lodgy");
                comboSeri.Items.Add("Duster");
                comboSeri.Items.Add("Logan");
            }
        }

        public void arac_listele()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string komutCumlesi = "Select * From Araclar";
            SqlCommand command = new SqlCommand(komutCumlesi, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        private void AracListele_Load(object sender, EventArgs e)
        {
            arac_listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string komutCumlesi = "Update Araclar set plaka = @plaka, marka = @marka, seri=@seri, model = @model, renk = @renk, kilometre = @kilometre, yakit=@yakit, kira_ucreti = @kira_ucreti, durumu = @durum";
            SqlCommand command = new SqlCommand(komutCumlesi, connection);
            command.Parameters.AddWithValue("@marka", comboMarka.SelectedItem);
            command.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            command.Parameters.AddWithValue("@seri", comboSeri.SelectedItem);
            command.Parameters.AddWithValue("@model", txtModel.Text);
            command.Parameters.AddWithValue("@renk", txtRenk.Text);
            command.Parameters.AddWithValue("@kilometre", txtKilometre.Text);
            command.Parameters.AddWithValue("@yakit", comboYakit.SelectedItem);
            command.Parameters.AddWithValue("@kira_ucreti", txtKiraUcreti.Text);
            command.Parameters.AddWithValue("@durum", comboDurum.SelectedItem);
            command.ExecuteNonQuery();
            connection.Close();
            arac_listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string komutCumlesi = "Delete From Araclar where plaka='" +
                                  dataGridView1.CurrentRow.Cells["plaka"].Value.ToString() + "'";
            SqlCommand command = new SqlCommand(komutCumlesi, connection);
            command.ExecuteNonQuery();
            connection.Close();
            arac_listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPlaka.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboMarka.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboSeri.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtModel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtRenk.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtKilometre.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            comboYakit.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txtKiraUcreti.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            comboDurum.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
        }
    }
}
