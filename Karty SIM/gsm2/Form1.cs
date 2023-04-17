using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCSC;

namespace gsm2
{
    public partial class Form1 : Form
    {
        private readonly SCardContext _context = new SCardContext();
        public Form1()
        {
            InitializeComponent();
            _context.Establish(SCardScope.System);
            comboBox1.Items.AddRange(_context.GetReaders());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            responseTextBox.Text = "";

            var cardName = comboBox1.SelectedText;

            var command = commandTextBox.Text;
            var commandBytes = ConvertToByteArray(command);

            var response = GetResponse(commandBytes);
            var hexresponse = BitConverter.ToString(response);
            responseTextBox.Text = hexresponse + "\r\n";
            string ascii = ConvertHex(hexresponse);
            responseTextBox.Text += ascii;

        }
        private byte[] GetResponse(byte[] input)
        {
            var output = new byte[256];

            try
            {
                var reader = new SCardReader(_context);
                var readerName = comboBox1.SelectedItem.ToString();

                reader.Connect(readerName, SCardShareMode.Shared, SCardProtocol.T0);

                reader.Transmit(input, ref output);
                return output;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new byte[0];
            }

        }
        private static byte[] ConvertToByteArray(string input)
        {
            string[] s = input.Split(',');
            byte[] data = new byte[s.Length];

            try
            {
                for (int i = 0; i < data.Length; i++)
                    data[i] = byte.Parse(s[i].Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return data;
        }
        public static string ConvertHex(String hexString)
        {
            try
            {
                string ascii = string.Empty;

                for (int i = 0; i < hexString.Length; i += 2)
                {
                    String hs = string.Empty;

                    hs = hexString.Substring(i, 2);
                    uint decval = System.Convert.ToUInt32(hs, 16);
                    char character = System.Convert.ToChar(decval);
                    ascii += character;

                }

                return ascii;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return string.Empty;
        }
    }
}
