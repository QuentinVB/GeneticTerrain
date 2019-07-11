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

            Console.WriteLine("initialize Simulation using following values:");
            Console.WriteLine(logger.Log("Simulation, initialization"));

            int maxPopulation = 200;
            int maxGeneration = 8;
            double startAcceptanceRatio = 0.6;
            int gridSize = 20;
            double mutationChance = 0.2;

            Console.WriteLine($"{nameof(maxPopulation)}:{maxPopulation}");
            Console.WriteLine($"{nameof(maxGeneration)}:{maxGeneration}");
            Console.WriteLine($"{nameof(startAcceptanceRatio)}:{startAcceptanceRatio}");
            Console.WriteLine($"{nameof(gridSize)}:{gridSize}");
            Console.WriteLine($"{nameof(mutationChance)}:{mutationChance}");

            
            GeneticTerrainGenerator generator = new GeneticTerrainGenerator(maxPopulation, maxGeneration, startAcceptanceRatio, gridSize, mutationChance);
            Console.WriteLine(logger.Log("Begin Simulation"));

            //THAR BE DRAGONZ
            //Algorithm best = generator.runSimulation();

            //Console.WriteLine(best);

            logger.Print();
            //vizualise ?
            Console.Read();
        }
    }
}
