using GeneticTerrain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizualizer;

namespace Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            logger.Log("Simulation, initialization");

            Console.WriteLine("initialize Simulation using following values:");

            GeneticParameters options = new GeneticParameters()
            {
                MaxPopulation = 200,
                MaxGeneration = 100,
                MutationChance = 0.7,
                StartAcceptanceRatio = 0.2
            };
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
                Algorithm best = generator.RunSimulation();

                logger.Log("End Simulation");
                logger.Log(best.ToString());
                /*
                logger.Log("Rendering...");
                Source.Render(
                    Source.CreatePictureFromGraph(20, 20, best.RootNode)
                    , 20, 20);
*/
            }
            finally 
            {
                var elapsedTime = DateTimeOffset.UtcNow.Subtract(startTime);
                logger.Log($"{nameof(elapsedTime)}:{elapsedTime}");

                logger.Print();
            }
            
            Console.Read();
        }
    }
}
