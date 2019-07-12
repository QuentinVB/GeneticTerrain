using GeneticTerrain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            logger.Log("Simulation, initialization");

            Console.WriteLine("initialize Simulation using following values:");

            int maxPopulation = 200;
            int maxGeneration = 30;
            double startAcceptanceRatio = 0.6;
            int gridSize = 20;
            double mutationChance = 0.2;

            //ugly var display, should be better with encapsulation
            Console.WriteLine("");
            Console.WriteLine($"{nameof(maxPopulation)}:{maxPopulation}");
            Console.WriteLine($"{nameof(maxGeneration)}:{maxGeneration}");
            Console.WriteLine($"{nameof(startAcceptanceRatio)}:{startAcceptanceRatio}");
            Console.WriteLine($"{nameof(gridSize)}:{gridSize}");
            Console.WriteLine($"{nameof(mutationChance)}:{mutationChance}");
            Console.WriteLine("");

            var startTime = DateTimeOffset.UtcNow;
            try
            {
                GeneticTerrainGenerator generator = new GeneticTerrainGenerator(maxPopulation, maxGeneration, startAcceptanceRatio, gridSize, mutationChance, logger);
                
                logger.Log("Begin Simulation");

                //THAR BE DRAGONZ
                Algorithm best = generator.runSimulation();

                logger.Log("End Simulation");
                logger.Log(best.ToString());
            }
            finally 
            {
                var elapsedTime = DateTimeOffset.UtcNow.Subtract(startTime);
                logger.Log($"{nameof(elapsedTime)}:{elapsedTime}");

                logger.Print();
            }          
            //vizualise ?
            Console.Read();
        }
    }
}
