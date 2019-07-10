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
            double[,] matrix = Source.CreatePictureFromMysteryEquation(width, height);

            Console.WriteLine("Begin render");

            try
            {
                //draw
                Bitmap bmp = new Bitmap(width, height);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        double matrixvalue = Math.Abs(matrix[x, y]);

                        int value = (matrixvalue > 1) ? 255 : (int)(matrixvalue * 255);
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
