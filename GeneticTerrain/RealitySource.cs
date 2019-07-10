using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTerrain
{
    public class RealitySource
    {
        /// <summary>
        /// Gets the z from mystery equation.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static double GetZFromMysteryEquation(int x, int y)
        {
            /*
                    double x = (i - 1 / 2) * 0.1;
                    double y = -(j - 1 / 2) * 0.1;*/

            return x + y > 0 ? x + 1 : -x;
                    
        }
        /*
            z(x,y) = via a picture
         */
    }
}
