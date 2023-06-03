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
    public partial class Sozlesme : Form
    {
        public Sozlesme()
        {
            InitializeComponent();
        }

        private string ConnectionString = @"Data Source=DESKTOP-N8ABPSM;Initial Catalog=RentACar;Integrated Security=True";

        public void Arac_Listele()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string komutCumlesi = "Select * From Araclar where durumu = 'Boş'";
            SqlCommand command = new SqlCommand(komutCumlesi, connection);
            SqlDataReader read = command.ExecuteReader();
            while (read.Read())
            {
                comboArac.Items.Add(read["plaka"]);
            }
            connection.Close();
        }

        public void Sozlesme_Listele()
        {
            SqlConnection baglanti = new SqlConnection(ConnectionString);
            baglanti.Open();

            string komutCumlesi = "select * from Sozlesme";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataAdapter adapter = new SqlDataAdapter(komut);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void Sozlesme_Load(object sender, EventArgs e)
        {
            Arac_Listele();
            Sozlesme_Listele();
        }

        private void comboArac_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(ConnectionString);
            baglanti.Open();

            string komutCumlesi = "select * from Araclar where Plaka like '" + comboArac.SelectedItem + "'";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                txtMarka.Text = read["Marka"].ToString();
                txtSeri.Text = read["Seri"].ToString();
                txtModel.Text = read["Model"].ToString();
                txtRenk.Text = read["Renk"].ToString();
            }
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            TimeSpan gunFarki = DateTime.Parse(dtpDonus.Text) - DateTime.Parse(dtpCikis.Text);
            int gunhesap = gunFarki.Days;
            txtGun.Text = gunhesap.ToString(); 
            txtTutar.Text = (gunhesap * int.Parse(txtKiraUcreti.Text)).ToString();
        }

        private void comboKiraSekli_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(ConnectionString);
            baglanti.Open();

            string komutCumlesi = "select Kira_Ucreti from Araclar";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                if(comboKiraSekli.SelectedIndex == 0)
                {
                    txtKiraUcreti.Text = (int.Parse(read["Kira_Ucreti"].ToString()) * 1).ToString();
                } else if (comboKiraSekli.SelectedIndex == 1)
                {
                    txtKiraUcreti.Text = (int.Parse(read["Kira_Ucreti"].ToString()) * 0.8).ToString();
                } else if (comboKiraSekli.SelectedIndex == 2)
                {
                    txtKiraUcreti.Text = (int.Parse(read["Kira_Ucreti"].ToString()) * 0.5).ToString();
                }
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(ConnectionString);
            baglanti.Open();

            string komutCumlesi = "insert into Sozlesme values (@tcno, @adsoyad, @telefon, @ehliyetno, @ehliyettarih, @plaka, @marka, @seri, @model, @renk, @kira_sekli, @kiraucreti, @kirasuresi, @tutar, @cikistarihi, @donustarihi)";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);

            komut.Parameters.AddWithValue("@tcno", txtTc.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@ehliyetno", txtEhliyetNo.Text);
            komut.Parameters.AddWithValue("@ehliyettarih", txtEhliyetTarihi.Text);
            komut.Parameters.AddWithValue("@plaka", comboArac.SelectedItem);
            komut.Parameters.AddWithValue("@marka", txtMarka.Text);
            komut.Parameters.AddWithValue("@seri", txtSeri.Text);
            komut.Parameters.AddWithValue("@model", txtModel.Text);
            komut.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut.Parameters.AddWithValue("@kira_sekli", comboKiraSekli.SelectedItem);
            komut.Parameters.AddWithValue("@kiraucreti", txtKiraUcreti.Text);
            komut.Parameters.AddWithValue("@kirasuresi", txtGun.Text);
            komut.Parameters.AddWithValue("@tutar", txtTutar.Text);
            komut.Parameters.AddWithValue("@cikistarihi", dtpCikis.Text);
            komut.Parameters.AddWithValue("@donustarihi", dtpDonus.Text);

            string komutCumlesiUp = "Update Araclar set durumu= 'Dolu' where plaka='" + comboArac.SelectedItem + "'";
            SqlCommand komutUp = new SqlCommand(komutCumlesiUp, baglanti);

            komut.ExecuteNonQuery();
            komutUp.ExecuteNonQuery();
            baglanti.Close();
            Sozlesme_Listele();
            Arac_Listele();

            MessageBox.Show("Kayıt Başarılı");
        }

        private void txtTcArea_TextChanged(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(ConnectionString);
            baglanti.Open();

            string komutCumlesi = "select * from Musteriler where Tc_No like '" + txtTcArea.Text + "'";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                txtTc.Text = read["Tc_No"].ToString();
                txtAdSoyad.Text = read["Ad_Soyad"].ToString();
                txtTelefon.Text = read["Telefon_Numarasi"].ToString();
            }
        }

        private void btnAracTeslim_Click(object sender, EventArgs e)
        {
            DataGridViewRow satir = dataGridView1.CurrentRow;
            DateTime bugün = DateTime.Parse(DateTime.Now.ToShortDateString());
            int ucret = int.Parse(satir.Cells["Kira_Ucreti"].Value.ToString());
            int tutar = int.Parse(satir.Cells["Tutar"].Value.ToString());
            DateTime cikis = DateTime.Parse(satir.Cells["Cikis_Tarihi"].Value.ToString());
            TimeSpan gun = bugün - cikis;
            int gunu = gun.Days;
            int toplamTutar = gunu * ucret;

            SqlConnection baglanti = new SqlConnection(ConnectionString);
            baglanti.Open();

            string komutCumlesi = "delete from Sozlesme where Plaka = '" + satir.Cells["Plaka"].Value.ToString() + "'";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            komut.ExecuteNonQuery();

            string komutCumlesiUp = "Update Araclar set Durumu = 'Boş' where Plaka = '" + satir.Cells["Plaka"].Value.ToString() + "'";
            SqlCommand komutUp = new SqlCommand(komutCumlesiUp, baglanti);
            komutUp.ExecuteNonQuery();

            string komutCumlesiSatis = "insert into Satis Values (@tcno, @adsoyad, @telefon, @plaka, @gun, @kira_sekli, @kiraucreti, @tutar, @cikistarihi, @donustarihi)";
            SqlCommand komutSatis = new SqlCommand(komutCumlesiSatis, baglanti);

            komutSatis.Parameters.AddWithValue("@tcno", satir.Cells["Tc_No"].Value.ToString());
            komutSatis.Parameters.AddWithValue("@adsoyad", satir.Cells["Ad_Soyad"].Value.ToString());
            komutSatis.Parameters.AddWithValue("@telefon", satir.Cells["Telefon"].Value.ToString());
            komutSatis.Parameters.AddWithValue("@plaka", satir.Cells["Plaka"].Value.ToString());
            komutSatis.Parameters.AddWithValue("@gun",gunu);
            komutSatis.Parameters.AddWithValue("@kira_sekli", satir.Cells["Kira_Sekli"].Value.ToString());
            komutSatis.Parameters.AddWithValue("@kiraucreti", ucret);
            komutSatis.Parameters.AddWithValue("@tutar", toplamTutar);
            komutSatis.Parameters.AddWithValue("@cikistarihi", satir.Cells["Cikis_Tarihi"].Value.ToString());
            komutSatis.Parameters.AddWithValue("@donustarihi", satir.Cells["Donus_Tarihi"].Value.ToString());

            komutSatis.ExecuteNonQuery();
            MessageBox.Show("Araç Teslim Edildi");
        }
    }
}
