﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizualizer
{
    class Source
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
            throw new NotImplementedException("ready for the asct !");
            // PUT AST PARSING HERE
            double[,] matrix = new double[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    
                    matrix[i, j] = 0;//COMPUTE AST Here
                }
            }
            return matrix;
        }

    }
}
