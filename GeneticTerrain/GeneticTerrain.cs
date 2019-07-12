using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ast;


namespace GeneticTerrain
{
    public class GeneticTerrainGenerator
    {
        int maxPopulation;
        int maxGeneration;
        double startAcceptanceRatio;
        int gridSize;
        double mutationChance;

        List<Algorithm> _population;
        BestKeeper<Algorithm> _incubator;

        //Only for the tests
        public List<Algorithm> Population { get => _population;  }
        public BestKeeper<Algorithm> Incubator { get => _incubator; }

        private GenerateStringGraph _string_graph = new GenerateStringGraph();
        private AstWrapper _wrapper;
        private Logger logger;

        /// <summary>
        /// Allow to generate an algorithm that match the Reality
        /// </summary>
        /// <param name="parameter">the size of the terrain</param>
        /// <param name="logger">the logger object</param>
        public GeneticTerrainGenerator(GeneticParameters options, Logger logger=null)
        {
            this.maxPopulation = options.MaxPopulation;
            this.maxGeneration = options.MaxGeneration;
            this.startAcceptanceRatio = options.StartAcceptanceRatio;
            this.gridSize = options.GridSize;
            this.mutationChance = options.MutationChance;
            this.logger = logger ?? new Logger();

            this._population = new List<Algorithm>();
            this._incubator = new BestKeeper<Algorithm>(1);

            _wrapper = new AstWrapper();

        }

        /// <summary> OK
        /// https://www.youtube.com/watch?v=PL6jwxw9T3c
        /// evaluate each algorithm and sort them by delta in the incubator
        /// compute delta foreach case the average to produce the delta of the algorithm
        /// Delta close to 0
        /// incubator heap size :inverse law (converging toward 0)
        /// </summary>
        /// <param name="population">The population.</param>
        /// <param name="startAcceptanceRatio">The start acceptance ratio.</param>
        /// <param name="generation">The generation.</param>
        public void NaturalSelection(List<Algorithm> population, int generation)
        {
            if (generation == 0) throw new DivideByZeroException();

            int incubatorSize = (int)Math.Ceiling( (1/generation)*maxPopulation * startAcceptanceRatio);

            this._incubator = new BestKeeper<Algorithm>(incubatorSize<=0?1: incubatorSize, (a, b) => a.CompareTo(b));

            foreach (Algorithm candidate in population)
            {
                Evaluate(candidate);
                //confront candidate to other (may the odd be ever in his favor)
                _incubator.Add(candidate);
            }
            //peek best delta
            logger.Log($" Best delta {population.Count}");

        }

        /// <summary>
        /// Nested evaluation for reusability
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        private double Evaluate(Algorithm candidate)
        {
            double deltaSum = 0;
            //generate delta from matrix
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    double x = (i - gridSize / 2) * 0.1;
                    double y = -(j - gridSize / 2) * 0.1;

                    //compute local delta
                    double value = _wrapper.Compute(candidate.RootNode, x, y);

                    //Confront to the reality and sum
                    deltaSum += Math.Pow(RealitySource.GetZFromMysteryEquation(x, y) - value, 2);
                    // improvement => store the value of Z from mystery equation to save CPU at a little cost of memory
                }
            } 
            return candidate.Delta = deltaSum / (gridSize * gridSize);
        }


        /// <summary>
        /// Meetic : who fucks who ?
        /// each algorithm choose randomly another algorithm to mate with
        /// ,it loop until number of couple is back to the max population
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<(Algorithm, Algorithm)> Meetic(List<Algorithm> p)
        {
            var AlgorithmCouples = new List<(Algorithm, Algorithm)>();
            var random = new Random();
            foreach (var elmt in p)
            {
                for (int i = 0; i <= p.Count; i++)
                {
                    int index = random.Next(p.Count);
                    AlgorithmCouples.Add((elmt, p[index]));
                    if (AlgorithmCouples.Count == maxPopulation) return AlgorithmCouples;
                }
            }
            return AlgorithmCouples;
        }

        /// <summary>
        /// Mutation
        /// </summary>
        /// <param name="population">The population.</param>
        /// <param name="mutationChance">The mutation chance.</param>
        private void Mutation(List<Algorithm> population, double mutationChance)
        /*a visitor who travel the tree 
        * 20% chance to mutate a node
        * the node will be substituate with another node chosed randomly among the 5 potential nodes
        * a binary node have to choose randomly between the 4 operation
        * then fill the leaf with a constant node OR a identifier
        * the constant node will be a random number
        */
        {
            double mutationSum = 0;
            foreach (Algorithm candidate in population)
            {
                
                candidate.RootNode = _wrapper.MutateGraph(candidate.RootNode, mutationChance, out int mutationCount);

                candidate.RootNode = _wrapper.OptimizeGraph(candidate.RootNode);

                mutationSum += mutationCount / candidate.NodeCount;

            }
            logger.Log($" {Math.Round( mutationSum/population.Count *100.0,2)}% of mutations on {population.Count} elements");
        }



        public Algorithm runSimulation()
        {
            int generation = 1;
            logger.Log("Create initial population");

            //Fichier en dur pour les tests de Quentin
            //string[] lines = File.ReadAllLines(@"../../../init_pop.txt", Encoding.UTF8);
            //string[] t = lines[0].Split(',');

            string line = _string_graph.CreateRandomPopulation(maxPopulation);
            string[] t = line.Split(',');

            foreach (string parsedEquation in t)
            {
                /*
                 throw exception : if : another identifier than x or y
                 */
                //if(parsedEquation.Contains()|| ) throw new ArgumentException("The identifier doesn't exist")
                _population.Add(new Algorithm(_wrapper.Parse(parsedEquation)));
            }

            do
            {
                logger.Log($"Generation {generation}");
                //Natural selection
                NaturalSelection(_population, generation);
                //Since we didnt make babies yet, the population must not be destroyed !
                //_population.Clear();

                //Making couple
                List<(Algorithm, Algorithm)> couples = Meetic(_incubator.ToList());

                //Making Children
                // Shuffle genome between A and B
                /* choose a method randomly => creationnnnn
                 * 1 :  choose a leaf from A randomly and substitute it with the tree from B
                 * 2 : 
                 */

                // Mutate the children in place
                Mutation(_population, mutationChance);   

                //add a peek best on incubator ?
                generation++;
            } while (generation < maxGeneration);
            logger.Log($"End Generations");

            //evaluateAtLeast
            NaturalSelection(_population, generation);
            //give me the best one baby !
            return _incubator.RemoveMax();
        }       
    }
}
