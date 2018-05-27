using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System;
namespace sahte_renklendirme
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }
        
        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void pictureBox2_Click(object sender, EventArgs e) { }

        private void Form1_Load_1(object sender, EventArgs e)
        {
           
        }

        int size;
        int count = 0;
        Bitmap image;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics graphicsObj;
            graphicsObj = this.CreateGraphics();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //yükleme

            OpenFileDialog ze = new OpenFileDialog();
            ze.Filter = "Resim Dosyaları|*.bmp;*.jpg;*.gif;*.wmf;*.tif;*.png";
            if (ze.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            pictureBox1.ImageLocation = ze.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog(); //yeni bir kaydetme diyaloğu oluşturuyoruz.

            sfd.Filter = "jpeg dosyası(*.jpg)|*.jpg|Bitmap(*.bmp)|*.bmp"; //.bmp veya .jpg olarak kayıt imkanı sağlıyoruz.

            sfd.Title = "Kaydet";  //diyalogumuzun başlığını belirliyoruz.

            sfd.FileName = "resim";  //kaydedilen resmimizin adını 'resim' olarak belirliyoruz.

            DialogResult sonuc = sfd.ShowDialog();

            if (sonuc == DialogResult.OK)
            {
                pictureBox2.Image.Save(sfd.FileName);  //Böylelikle resmi istediğimiz yere kaydediyoruz.
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //temizleme butonu
            pictureBox1.Image = null;
        }
        public float GetBrightness(int R,int G,int B)
        {
            float r = (float)R / 255.0f;
            float g = (float)G / 255.0f;
            float b = (float)B / 255.0f;

            float max, min;

            max = r; min = r;

            if (g > max) max = g;
            if (b > max) max = b;

            if (g < min) min = g;
            if (b < min) min = b;

            return (max + min) / 2;
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            //pseudo-coloring

            Bitmap rgb = new Bitmap(pictureBox1.Image);    // pseudo coloring yapılacak image
            Bitmap rrgb = new Bitmap(rgb);                 // sonuc image
          
            for (int i = 0; i < rgb.Height; i++)           // sütun pixellere erişim için
            {
                for (int j = 0; j < rgb.Width; j++)        // satır pixellere erişim için
                {
                    Color rk = rgb.GetPixel(j, i);
                    int r = rk.R;        // blue değeri
                    int g = rk.G;// green değeri
                    int b = rk.B;
                             
                    int a2 = rk.A;                          // pixelin renk değerini erişim
                    float brightness = GetBrightness(r, g, b); // pixelin parlaklığı bulunuyor (0 ile 1 arasındaa değer döner).            
                    int r2 = getRed(brightness);            // blue değeri
                    int g2= getGreen(brightness);          // green değeri
                    int b2 = getBlue(brightness);           // red değeri

                    rrgb.SetPixel(j, i, Color.FromArgb(a2, r2, g2,b2));   // sonuc görselin pixeline set ediliyor.

                }
            }

            pictureBox2.Image = rrgb;                      // sonuc image ekrana çizdiriliyor.
        }


        public int getRed(float x)
        {
             float fonk = - 4 * x * x + 8 * x - 3;
            //float fonk= -x*x+2*x;
            int value = 0;
            if (fonk >= 0 && fonk <= 1)
                value = (int)Math.Round(fonk * 255.0); 
            else
                value = 0;

            return value;
        }
        public int getGreen(float x)
        {
            float fonk = -4*x*x + 4*x;
           // float fonk = -1 / 2 * x * x + 9 / 8;
            int value = (int)Math.Round(fonk * 255.0);     
            return value;
        }
        public int getBlue(float x)
        {
           // float fonk = -x *x + 1;
            float fonk = -4 * x * x + 1;
            int value = 0;
            if (fonk >= 0 && fonk <= 1)
                value = (int)Math.Round(fonk * 255.0); 
            else
                value = 0;

            return value;       
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
        }
    }

}
