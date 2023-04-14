using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace EAN13
{
    internal class Ean
    {
        // stałe markery 
        private static readonly string _StartStop = "101";
        private static readonly string _CenterMarker = "01010";
        private static readonly string _LeftQuiteZone = "00000000000";

        // wielkości kodu kreskowego
        public static readonly float _Width = 37.29f;
        public static readonly float _Height = 25.93f;
        private static readonly float _DigitHeight = 3.08f;
        private static readonly float _Scale = 1.0f; // od 0.8 do 2.0
        private static readonly float _DigitSize = 1.8f;

        //Left A code
        private static readonly string[] _ACode =
            {"0001101", "0011001", "0010011", "0111101",
            "0100011", "0110001", "0101111", "0111011",
            "0110111", "0001011"};

        //Left B code
        private static readonly string[] _BCode =
            {"0100111", "0110011", "0011011", "0100001",
            "0011101", "0111001", "0000101", "0010001",
            "0001001", "0010111"};

        //Right C code
        private static readonly string[] _CCode =
        {
            "1110010", "1100110", "1101100", "1000010",
            "1011100", "1001110", "1010000", "1000100",
            "1001000", "1110100"};

        //kodowanie wedlug pierwszej cyfry
        private static readonly string[] _LeftCode =
        {
            "AAAAAA", "AABABB", "AABBAB", "AABBBA",
            "ABAABB", "ABBAAB", "ABBBAA", "ABABAB",
            "ABABBA", "ABBABA"};

        private Bitmap _barCodeImage;

        public Ean(string Code)
        {
            //if (!Regex.IsMatch(Code, "^\\d{12}$")) throw new Exception("Nieprawidłowy kod");
            BarCode = Code;
            CheckSum = CalculateControl().ToString();
        }

        // kod kreskowy bez sumy kontrolnej
        public string BarCode { get; }

        // smua kontrolna
        public string CheckSum { get; }
        public int CalculateControl()
        {
            int sum = 0;
            for (int i = 1; i < 12; i = i + 2)
                sum += Convert.ToInt32(BarCode.Substring(i, 1));
            sum = sum * 3;
            for (int i = 0; i < 12; i = i + 2)
                sum += Convert.ToInt32(BarCode.Substring(i, 1));
            return (10 - (sum % 10)) % 10;
        }

        // kodowanie cyfr kodu
        private string EncodeCode()
        {
            var encoded = "";
            var encoding = _LeftCode[(int)char.GetNumericValue(BarCode[0])];
            // Quite zone
            encoded += _LeftQuiteZone;
            // Marker początku
            encoded += _StartStop;
            // lewa część kodu
            for (var i = 1; i <= 6; i++)
            {
                var digit = (int)char.GetNumericValue(BarCode[i]);
                if (encoding[i - 1] == 'A')
                    encoded += _ACode[digit];
                else
                    encoded += _BCode[digit];
            }

            // Marker środka
            encoded += _CenterMarker;
            // prawa część kodu
            for (var i = 7; i < 12; i++) encoded += _CCode[(int)char.GetNumericValue(BarCode[i])];
            encoded += _CCode[(int)char.GetNumericValue(CheckSum[0])];

            // Marker końca
            encoded += _StartStop;

            return encoded;
        
        }

        // tworzenie grafiki kodu kreskowego
        public Bitmap GenerateBarCode()
        {
            // wymiary płótna
            var width = (int)(_Width * 100);
            var height = (int)(_Height * 100);
            // rozmiar cyfry
            var digitHeight = _DigitHeight * _Scale * 100;
            // szerokość jednej linii
            var lineWidth = width / 113f;
            // zakodowane cyfry kodu
            var encoded = EncodeCode();

            // bitmapa przechowująca kod kreskowy
            var bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            // rysowanie
            using (var grp = Graphics.FromImage(bitmap))
            {
                // czyszczenie
                grp.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);

                var currentPos = 0f;

                // uzupełnianie kodu
                for (var i = 0; i < encoded.Length; i++)
                {
                    if (encoded[i] == '1')
                    {
                        // elementy oznaczające cyfry ( są krótsze )
                        if (i > _LeftQuiteZone.Length + _StartStop.Length - 1 &&
                            i < _LeftQuiteZone.Length + _StartStop.Length + 42
                            || i > _LeftQuiteZone.Length + _StartStop.Length + _CenterMarker.Length + 41 &&
                            i < _LeftQuiteZone.Length + _StartStop.Length + _CenterMarker.Length + 84)
                            grp.FillRectangle(Brushes.Black, currentPos, 0, lineWidth, height - digitHeight);
                        else
                            grp.FillRectangle(Brushes.Black, currentPos, 0, lineWidth, height - digitHeight / 2);
                    }

                    currentPos += lineWidth;
                }

                // rysowanie cyfr
                var font = new Font("Arial", _DigitSize * _Scale * 100);

                grp.DrawString(BarCode[0].ToString(), font, Brushes.Black, new PointF(0f, height - grp.MeasureString(BarCode[0].ToString(), font).Height));
                var digitPlaceWidth = lineWidth * 7;
                // obecna wycentrowane pozycja
                var xPos = digitPlaceWidth / 2 + (_LeftQuiteZone.Length + _StartStop.Length) * lineWidth;
                // wszystkie cyfry pozostałe do wypisania
                var digits = BarCode.Substring(1) + CheckSum;
                // lewa część kodu
                for (var i = 0; i < 6; i++)
                {
                    var fontSize = grp.MeasureString(digits[i].ToString(), font);
                    grp.DrawString(digits[i].ToString(), font, Brushes.Black, new PointF(
                        xPos - fontSize.Width / 2,
                        height - fontSize.Height
                    ));

                    xPos += digitPlaceWidth;
                }

                // prawa część kodu
                xPos += lineWidth * _CenterMarker.Length;
                for (var i = 6; i < 12; i++)
                {
                    var fontSize = grp.MeasureString(digits[i].ToString(), font);
                    grp.DrawString(digits[i].ToString(), font, Brushes.Black, new PointF(
                        xPos - fontSize.Width / 2,
                        height - fontSize.Height
                    ));

                    xPos += digitPlaceWidth;
                }
            }

            if (_barCodeImage != null) _barCodeImage.Dispose();

            _barCodeImage = bitmap;

            return bitmap;
        }
        public Bitmap GetBarcodeBitmap()
        {
            return _barCodeImage;
        }
        public PointF GetCodeSize()
        {
            return new PointF(_Width * _Scale, _Height * _Scale);
        }
    }
}
