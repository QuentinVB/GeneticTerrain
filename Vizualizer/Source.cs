using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using Ast;
using System.IO;

namespace Vizualizer
{
    public class Source
    {
        public static double[,] CreateRandomPicture(int width, int height)
        {
            Random rand = new Random();
            double[,] matrix = new double[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matrix[i, j] = rand.NextDouble();
                }
            }
            return matrix;
        }
        public static double[,] CreatePictureFromMysteryEquation(int width, int height)
        {
            double[,] matrix = new double[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double x = (i - width / 2)*0.1;
                    double y = -(j - height / 2)*0.1;
                    double value = x + y > 0 ? x + 1 : -x;
                    matrix[i, j] = value;
                }
            }
            return matrix;
        }

        

        public static double[,] CreatePictureFromEquation(int width, int height, string equation)
        {
            //throw new NotImplementedException("ready for the ast !");
            AstWrapper astWrapper = new AstWrapper();

            Node computeGraphRoot = astWrapper.Parse(equation);

            double[,] matrix = new double[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double x = (i - width / 2) * 0.1;
                    double y = -(j - height / 2) * 0.1;
                    matrix[i, j] = astWrapper.Compute(computeGraphRoot, x, y) ;//COMPUTE AST Here
                }
            }
            return matrix;
        }

        public static double[,] CreatePictureFromGraph(int width, int height, Ast.Node root)
        {
            AstWrapper astWrapper = new AstWrapper();


            double[,] matrix = new double[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double x = (i - width / 2) * 0.1;
                    double y = -(j - height / 2) * 0.1;
                    matrix[i, j] = astWrapper.Compute(root, x, y);//COMPUTE AST Here
                }
            }
            return matrix;
        }
        public static void Render(double[,] matrix, int width, int height)
        {
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

                Directory.CreateDirectory(Path.GetDirectoryName(Directory.GetCurrentDirectory() + "\\output\\"));
                bmp.Save($".\\render\\{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.png", ImageFormat.Png);
            }
            catch (Exception)
            {
                throw new Exception("An error occured while generating the bitmap");
            }
        }
    }
}

