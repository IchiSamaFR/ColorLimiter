using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace ColorLimiter
{
    class Program
    {
        public static string url = @"C:\Users\IchiSamaFR\Pictures\Shooting06.06.21\IMGP5234.jpg";
        public const int ColorsContrate = 5;
        public static int Multiplicator { get => (int)(255 / ColorsContrate); }
        static void Main(string[] args)
        {
            ChangeColor(GetImage(url, 1200));
            Console.ReadLine();
        }
        static Bitmap GetImage(string url, int size = 0)
        {
            if (size == 0) return (Bitmap)Image.FromFile(url);

            int width = size;
            int height = size;
            Bitmap original = (Bitmap)Image.FromFile(url);
            double ratioX = (double)width / (double)original.Width;
            double ratioY = (double)height / (double)original.Height;

            double ratio = ratioX < ratioY ? ratioX : ratioY;

            height = Convert.ToInt32(original.Height * ratio);
            width = Convert.ToInt32(original.Width * ratio);

            return new Bitmap(original, new Size(width, height));
        }

        static void ChangeColor(Bitmap img)
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Color c = img.GetPixel(x, y);
                    c = Color.FromArgb(GetNearestValue(c.R),
                                        GetNearestValue(c.G),
                                        GetNearestValue(c.B));
                    img.SetPixel(x, y, c);
                }
            }
            //img.Save(Path.GetDirectoryName(url) + "\\" + Path.GetFileNameWithoutExtension(url) + "_c" + Path.GetExtension(url));
            img.Save(Path.GetFileNameWithoutExtension(url) + "_c" + Path.GetExtension(url));
            Console.WriteLine(Path.GetDirectoryName(url) + "\\" + Path.GetFileNameWithoutExtension(url) + "_c" + Path.GetExtension(url));
        }

        static int GetNearestValue(int color)
        {
            for (int i = 0; i < ColorsContrate; i++)
            {
                if (color < i * Multiplicator)
                {
                    return i * Multiplicator;
                }
            }
            return 255;
        }
    }
}
