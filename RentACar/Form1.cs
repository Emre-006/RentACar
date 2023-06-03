using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentACar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EkleMusteri eklemusteri = new EkleMusteri();
            eklemusteri.Show();
        }

        private void btnListeleMusteri_Click(object sender, EventArgs e)
        {
            MusteriListele musteriListele = new MusteriListele();
            musteriListele.Show();
        }

        private void btnAracEkle_Click(object sender, EventArgs e)
        {
            AracEkle aracEklefrm = new AracEkle();
            aracEklefrm.Show();
        }

        private void btnAracListele_Click(object sender, EventArgs e)
        {
            AracListele aracListeleFrm = new AracListele();
            aracListeleFrm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Sozlesme sozlesme = new Sozlesme();
            sozlesme.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Satis satisFrm = new Satis();
            satisFrm.Show();
        }
    }
}
