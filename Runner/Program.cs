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

            GeneticParameters options = new GeneticParameters();
            /*{
                maxPopulation = 200,
                maxGeneration = 30,
                startAcceptanceRatio = 0.6,
                gridSize = 20,
                mutationChance = 0.2
            }*/


            logger.Log(options.ToString());

            var startTime = DateTimeOffset.UtcNow;
            try
            {
                GeneticTerrainGenerator generator = new GeneticTerrainGenerator(options, logger);
                
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
