using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace Vizualizer
{
    class Program
    {

        static void Main(string[] args)
        {
            int width = 20;
            int height = 20;
            Console.WriteLine("Generate terrain");

            //Generate fake picture
            Random rand = new Random();
            double[,] matrix = new double[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matrix[i, j] = rand.NextDouble();
                }
            }

            Console.WriteLine("Begin render");

            try
            {
                //draw
                Bitmap bmp = new Bitmap(width, height);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int value = (int)(matrix[x, y] * 255);
                        if (value > 255) value = 255;
                        Color newColor = Color.FromArgb(value, value, value);
                        bmp.SetPixel(x, y, newColor);
                    }
                }
                //save

                bmp.Save($".\\output\\{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.png", ImageFormat.Png);
            }
            catch (Exception)
            {
                throw new Exception("An error occured while generating the bitmap");
            }
            Console.WriteLine("Render Finished");
        }
    }
}
