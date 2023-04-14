using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAN13
{
    public partial class Form1 : Form
    {
        private Ean Ean13;
        public Form1()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            var code = inputBox.Text;
            if (code.Length == 13)
            {
                try
                {
                    Ean13 = new Ean(code);
                    inputBox.Text = Ean13.BarCode;
                    if (Convert.ToInt32(""+code[12]) != Convert.ToInt32(Ean13.CheckSum)) { MessageBox.Show("Nieprawidłowa cyfra kontrolna!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    else { 
                    controlBox.Text = Ean13.CheckSum;
                    if (pictureBoxBarCode.Image != null) pictureBoxBarCode.Image.Dispose();
                     pictureBoxBarCode.Image = Ean13.GenerateBarCode();
                    }
                }
                catch
                {
                    MessageBox.Show("Wystąpił problem podczas tworzenia kodu kreskowego", "Błąd", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else if (code.Length == 12)
            {
                try
                {
                    Ean13 = new Ean(code);
                    inputBox.Text = Ean13.BarCode;
                    controlBox.Text = Ean13.CheckSum;
                    if (pictureBoxBarCode.Image != null) pictureBoxBarCode.Image.Dispose();
                    pictureBoxBarCode.Image = Ean13.GenerateBarCode();
                }
                catch
                {
                    MessageBox.Show("Wystąpił problem podczas tworzenia kodu kreskowego", "Błąd", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Nieprawidłowa długość kodu!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
