using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ast;


namespace GeneticTerrain
{
    public class GeneticTerrain
    {
        int maxPopulation;
        int maxGeneration;
        double startAcceptanceRatio;
        int gridSize;


        List<Algorithm> _population;
        BestKeeper<Algorithm> _incubator;

        public List<Algorithm> Population { get => _population;  }
        public BestKeeper<Algorithm> Incubator { get => _incubator; }

        /// <summary>
        /// Allow to generate an algorithm that match the Reality
        /// </summary>
        /// <param name="maxPopulation">the maximum population generated</param>
        /// <param name="maxGeneration">the maximum number of generation</param>
        /// <param name="startAcceptanceRatio">the first ratio for natural selection</param>
        /// <param name="gridSize">the size of the terrain</param>
        public GeneticTerrain(int maxPopulation, int maxGeneration, double startAcceptanceRatio, int gridSize)
        {
            if (maxPopulation <= 0) throw new ArgumentException("The max population must be higher than 0",nameof(maxPopulation));
            if (maxGeneration <= 0) throw new ArgumentException("Generations must be higher than 0",nameof(maxGeneration));
            if (startAcceptanceRatio <= 0) throw new ArgumentException("Acceptance ratio must be higher than 0", nameof(maxGeneration));
            if (gridSize <= 0) throw new ArgumentException("The grid size must be higher than 0", nameof(maxGeneration));

            this.maxPopulation = maxPopulation;
            this.maxGeneration = maxGeneration;
            this.startAcceptanceRatio = startAcceptanceRatio;
            this.gridSize = gridSize;

            this._population = new List<Algorithm>();
            this._incubator = new BestKeeper<Algorithm>(1);
        }

        /// <summary>
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

            this._incubator = new BestKeeper<Algorithm>(incubatorSize, (a, b) => a.CompareTo(b));

            foreach (Algorithm candidate in population)
            {
                double deltaSum = 0;
                //generate delta for matrix
                for (int i = 0; i < gridSize; i++)
                {
                    for (int j  = 0; j< gridSize;j++)
                    {
                        double x = (i - gridSize / 2) * 0.1;
                        double y = -(j - gridSize / 2) * 0.1;

                        //compute local delta
                        //INSERT COMPUTE FROM WRAPPER HERE
                        //double value = AstWrapper.Compute(candidate.RootNode, x,y);

                        //TEMP MAGICAL VALUE <- reeeeally bad !
                        double value = 0.5;

                        //Confront to the reality and sum
                        deltaSum += Math.Pow(RealitySource.GetZFromMysteryEquation(x, y) - value,2);
                        // improvement => store the value of Z from mystery equation to save CPU at a little cost of memory
                    }
                }
                candidate.Delta = deltaSum / (gridSize * gridSize);
                deltaSum = 0;

                //confront candidate to other (may the odd be ever in his favor)
                _incubator.Add(candidate);
            }

        }


        /// <summary>
        /// Meetic : who fucks who ?
        /// each algorithm choose randomly another algorithm to mate with
        /// ,it loop until number of couple is back to the max population
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<(Algorithm,Algorithm)> Meetic(List<Algorithm> p)
        {  
            var AlgorithmCouples = new List<(Algorithm, Algorithm)>();
            var random = new Random();
            foreach (var elmt in p)
            {
                for (int i = 0; i <= p.Count; i++)
                {
                    int index = random.Next(p.Count);
                    AlgorithmCouples.Add((elmt, p[index]));
                    if(AlgorithmCouples.Count==maxPopulation) return AlgorithmCouples;
                }
            }
            return AlgorithmCouples;
        }

        public Algorithm runSimulation()
        {
            int generation = 1;

            //Create initial population
            /*
             * Inject strings :only x et y as identifier, const unlimited
             * example : "x+1/2"
             * exception
             */

            do
            {
                NaturalSelection(_population, generation);
                _population.Clear();

                List<(Algorithm, Algorithm)> couples = Meetic(_incubator.ToList());
                

                // Shuffle genome between A and B
                /* choose a method randomly => creationnnnn
                 * 1 :  choose a leaf from A randomly and substitute it with the tree from B
                 * 2 : 
                 */

                //Mutation : 
                /* visitor who travel the tree 
                 * 20% chance to mutate a node
                 * the node will be substituate with another node chosed randomly among the 5 potential nodes
                 * a binary node have to choose randomly between the 4 operation
                 * then fill the leaf with a constant node OR a identifier
                 * the constant node will be a random number
                 */
                 //optimizer

                //DO IT AGAIN :)

                generation++;

            } while (generation < maxGeneration);

            //evaluateAt last

            return _incubator.RemoveMax();
        }

        
    }
}
