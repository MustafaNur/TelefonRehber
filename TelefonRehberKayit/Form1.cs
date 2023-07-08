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

namespace TelefonRehberKayit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-ABE0UME;Initial Catalog=dbRehber;Integrated Security=True");

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Kisiler",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource= dt;
        }
        void temizle()
        {
            txtAd.Text = "";
            textSoyad.Text = "";
            mskdMeil.Text = "";
            mskdTel.Text = "";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutEkle= new SqlCommand("insert into Kisiler (Ad,Soyad,Telefon,Mail) values (@p1,@p2,@p3,@p4)",baglanti);
            komutEkle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutEkle.Parameters.AddWithValue("@p2", textSoyad.Text);
            komutEkle.Parameters.AddWithValue("@p3", mskdTel.Text);
            komutEkle.Parameters.AddWithValue("@p4", mskdMeil.Text);
            komutEkle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Sisteme Kaydedildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            mskdTel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskdMeil.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show("Kişiyi Silmek İstiyor musunuz=", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

            if (result == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutSil = new SqlCommand("Delete From Kisiler Where ID=" + txtID.Text, baglanti);
                komutSil.ExecuteNonQuery();
                baglanti.Close();
            }
            else if (result == DialogResult.No)
            {
                txtID.Text = "";
            }

            listele();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komuGuncelle = new SqlCommand("update Kisiler set Ad=@p1, Soyad=@p2,Telefon=@p3,Mail=@p4 where ID=@p5", baglanti);
            komuGuncelle.Parameters.AddWithValue("@p1", txtAd.Text);
            komuGuncelle.Parameters.AddWithValue("@p2", textSoyad.Text);
            komuGuncelle.Parameters.AddWithValue("@p3", mskdTel.Text);
            komuGuncelle.Parameters.AddWithValue("@p4", mskdMeil.Text);
            komuGuncelle.Parameters.AddWithValue("@p5", txtID.Text);
            komuGuncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();

        }
    }
}
