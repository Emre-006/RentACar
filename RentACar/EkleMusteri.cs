using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentACar
{
    public partial class EkleMusteri : Form
    {
        public EkleMusteri()
        {
            InitializeComponent();
        }

        private string ConnectionString = @"Data Source=DESKTOP-N8ABPSM;Initial Catalog=RentACar;Integrated Security=True";

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(ConnectionString);
            baglanti.Open();

            string komutCumlesi = "Insert Into Musteriler Values (@TcNo,@AdSoyad,@TelefonNo,@Mail,@Adres)";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);

            komut.Parameters.AddWithValue("@Tcno", txtTcNo.Text);
            komut.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@TelefonNo", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@Mail", txtMail.Text);
            komut.Parameters.AddWithValue("@Adres", txtAdres.Text);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
