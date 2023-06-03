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
    public partial class Satis : Form
    {
        public Satis()
        {
            InitializeComponent();
        }

        private string ConnectionString = @"Data Source=DESKTOP-N8ABPSM;Initial Catalog=RentACar;Integrated Security=True";

        private void Satis_Load(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(ConnectionString);
            baglanti.Open();

            string komutCumlesi = "Select * from Satis";
            SqlCommand command = new SqlCommand(komutCumlesi, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
    }
}
