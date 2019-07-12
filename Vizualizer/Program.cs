using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using Ast;
using System.IO;
using GeneticTerrain;

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

            //double[,] matrix = Source.CreatePictureFromMysteryEquation(width, height);
            Console.WriteLine("Enter the terrain equation :");

            double[,] matrix = Source.CreatePictureFromEquation(width, height,Console.ReadLine());

            Console.WriteLine("Begin render");

            Source.Render(matrix, width, height) ;
 
            Console.WriteLine("Render Finished");
        }
    }
}
