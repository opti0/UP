using Accord.Video.FFMPEG;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Image = System.Drawing.Image;

namespace kamera
{
    public partial class Form1 : Form
    {
        private readonly object ramkaLock = new object();
        private readonly object nagrywanieLock = new object();
        private VideoCaptureDevice kamera;
        private Bitmap ramka;
        int bri = 0, con = 0;
        bool recording = false;
        VideoFileWriter video;
        Stopwatch fpsy;
        public Form1()
        {
            InitializeComponent();
        }
        //inicjalizacja
        private void button1_Click(object sender, EventArgs e)
        {
            //sprawdzenie ilości dostępnych kamer w systemie, gdy dostepne wybiera kamere
            var urzadzenia = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (urzadzenia.Count > 0)
                kamera = new VideoCaptureDevice(urzadzenia[0].MonikerString);
            else
            {
                MessageBox.Show("Nie wykryto kamery");
                return;
            }
            kamera.NewFrame += new NewFrameEventHandler(nowaRamka);
            kamera.Start();
            fpsy = new Stopwatch();
            fpsy.Start();
        }

        //robienie zdjec
        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap zdjecie;
            lock (ramkaLock)
            {
                zdjecie = ramka;
                SaveFileDialog lokalizacja = new SaveFileDialog();
                lokalizacja.AddExtension = true;
                lokalizacja.Filter = "Plik graficzny (*.jpeg)|*.jpeg";
                lokalizacja.DefaultExt = "jpeg";
                lokalizacja.ShowDialog();
                if (lokalizacja.FileName == "")
                {
                    MessageBox.Show("Pusta nazwa pliku! Operacja niedozwolona");
                    return;
                }


                zdjecie.Save(lokalizacja.FileName, ImageFormat.Jpeg);
            }

            
        }

        //nagrywanie wideo
        private void button3_Click(object sender, EventArgs e)
        {
            lock (nagrywanieLock)
            {
                if (recording)
                {
                    video.Close();
                }
                else
                {
                    SaveFileDialog lokalizacja = new SaveFileDialog();
                    lokalizacja.AddExtension = true;
                    lokalizacja.Filter = "Plik wideo (*.avi)|*.avi";
                    lokalizacja.DefaultExt = "jpeg";
                    lokalizacja.ShowDialog();

                    if (lokalizacja.FileName == "")
                    {
                        MessageBox.Show("Pusta nazwa pliku! Operacja niedozwolona");
                        return;
                    }

                    video = new VideoFileWriter();
                    video.Open(lokalizacja.FileName, ramka.Width, ramka.Height, 20, VideoCodec.Default, 5000_000);

                }

                recording = !recording;
                button3.Text = recording ? "Zatrzymaj nagrywanie" : "Wideo";
            }
        }
        //suwak do regulacji jasności
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            bri = trackBar1.Value;
        }

        //suwak do regulacji kontrastu
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            con = trackBar2.Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap zdjecie1;
            Bitmap zdjecie2;
            Bitmap zdjecie3;
            Bitmap zdjecieHDR;
            SaveFileDialog lokalizacja;
            lock (ramkaLock)
            {
                zdjecie1 = ramka;
                lokalizacja = new SaveFileDialog();
                lokalizacja.AddExtension = true;
                lokalizacja.Filter = "Plik graficzny (*.jpeg)|*.jpeg";
                lokalizacja.DefaultExt = "jpeg";
                //lokalizacja.ShowDialog();
                lokalizacja.FileName = "1.jpeg";
                zdjecie1.Save(lokalizacja.FileName, ImageFormat.Jpeg);
            }
            lock (ramkaLock)
            {
                bri = 100;
                BrightnessCorrection hdr1 = new BrightnessCorrection(bri);
                hdr1.ApplyInPlace(ramka);
                zdjecie2 = ramka;
                //trackBar2.Value = bri;
                lokalizacja = new SaveFileDialog();
                lokalizacja.AddExtension = true;
                lokalizacja.Filter = "Plik graficzny (*.jpeg)|*.jpeg";
                lokalizacja.DefaultExt = "jpeg";
                //lokalizacja.ShowDialog();
                lokalizacja.FileName = "2.jpeg";
                zdjecie2.Save(lokalizacja.FileName, ImageFormat.Jpeg);
            }
            lock (ramkaLock)
            {
                bri = -100;
                BrightnessCorrection hdr2 = new BrightnessCorrection(bri);
                hdr2.ApplyInPlace(ramka);
                zdjecie3 = ramka;
                //trackBar2.Value = bri;
                lokalizacja = new SaveFileDialog();
                lokalizacja.AddExtension = true;
                lokalizacja.Filter = "Plik graficzny (*.jpeg)|*.jpeg";
                lokalizacja.DefaultExt = "jpeg";
                //lokalizacja.ShowDialog();
                lokalizacja.FileName = "3.jpeg";
                zdjecie3.Save(lokalizacja.FileName, ImageFormat.Jpeg);
            }
            lock (ramkaLock)
            {
                zdjecieHDR = new Bitmap(zdjecie1.Width, zdjecie1.Height);
                for (int x = 0; x < zdjecieHDR.Width; x++)
                {
                    for (int y = 0; y < zdjecieHDR.Height; y++)
                    {
                        Color c1 = zdjecie1.GetPixel(x, y);
                        Color c2 = zdjecie2.GetPixel(x, y);
                        Color c3 = zdjecie3.GetPixel(x, y);
                        Color cHDR = Color.FromArgb(
                            ((int)c1.R + (int)c2.R + (int)c3.R) / 3,
                            ((int)c1.G + (int)c2.G + (int)c3.G) / 3,
                            ((int)c1.B + (int)c2.B + (int)c3.B) / 3
                            );
                        zdjecieHDR.SetPixel(x, y, cHDR);
                    }
                }
                lokalizacja = new SaveFileDialog();
                lokalizacja.AddExtension = true;
                lokalizacja.Filter = "Plik graficzny (*.jpeg)|*.jpeg";
                lokalizacja.DefaultExt = "jpeg";
                //lokalizacja.ShowDialog();
                lokalizacja.FileName = "HDR.jpeg";
                zdjecieHDR.Save(lokalizacja.FileName, ImageFormat.Jpeg);
            }
        }

        private void nowaRamka(object sender, NewFrameEventArgs eventArgs)
        {

            lock (ramkaLock)
            {
                ramka = (Bitmap)eventArgs.Frame.Clone();
                BrightnessCorrection bc = new BrightnessCorrection(bri);
                ContrastCorrection cc = new ContrastCorrection(con);

                bc.ApplyInPlace(ramka);
                cc.ApplyInPlace(ramka);

                pictureBox1.Image = new Bitmap(ramka, pictureBox1.Size);
            }

            lock (nagrywanieLock)
            {
                if (recording)
                {
                    video.WriteVideoFrame(ramka);
                }
            }
        }
    }
}
