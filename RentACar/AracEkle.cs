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
    public partial class AracEkle : Form
    {

        private string ConnectionString = @"Data Source=DESKTOP-N8ABPSM;Initial Catalog=RentACar;Integrated Security=True";

        public AracEkle()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(ConnectionString);
            baglanti.Open();

            string komutCumlesi = "Insert Into Araclar Values (@plaka,@marka,@seri,@model,@renk,@kilometre,@yakit,@ucret,@durum)";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);

            komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut.Parameters.AddWithValue("@marka", comboMarka.SelectedItem);
            komut.Parameters.AddWithValue("@seri", comboSeri.SelectedItem);
            komut.Parameters.AddWithValue("@model", txtModel.Text);
            komut.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut.Parameters.AddWithValue("@kilometre", txtKilometre.Text);
            komut.Parameters.AddWithValue("@yakit", comboYakit.SelectedItem);
            komut.Parameters.AddWithValue("@ucret", txtKiraUcreti.Text);
            komut.Parameters.AddWithValue("@durum", comboDurum.SelectedItem);

            try
            {
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Başarılı");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

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
    }
}
