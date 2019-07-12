using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeneticTerrain
{
    public class GeneticParameters
    {
        int maxPopulation;
        int maxGeneration;
        double startAcceptanceRatio;
        int gridSize;
         double mutationChance;

        /// <summary>
        /// The parameter payload for the genetic algorithm
        /// </summary>
        public GeneticParameters()
        {
            this.maxPopulation = 200;
            this.maxGeneration = 10;
            this.startAcceptanceRatio = 0.5;
            this.gridSize = 20;
            this.mutationChance = 0.2;
        }

        /// <summary>
        /// Gets or sets the maximum population.
        /// </summary>
        /// <value>
        /// The maximum population.
        /// </value>
        /// <exception cref="ArgumentException">The max population must be higher than 0 - maxPopulation</exception>
        public int MaxPopulation { get => maxPopulation;
            set {
                if (value <= 0) throw new ArgumentException("The max population must be higher than 0", nameof(maxPopulation));
                maxPopulation = value;
            }
        }
        /// <summary>
        /// Gets or sets the maximum generation.
        /// </summary>
        /// <value>
        /// The maximum generation.
        /// </value>
        /// <exception cref="ArgumentException">Generations must be higher than 0 - maxGeneration</exception>
        public int MaxGeneration { get => maxGeneration;
            set
            {
                if (maxGeneration <= 0) throw new ArgumentException("Generations must be higher than 0", nameof(maxGeneration));

                maxGeneration = value;
            }
        }
        /// <summary>
        /// Gets or sets the acceptance ratio in the incubator.
        /// </summary>
        /// <value>
        /// The start acceptance ratio.
        /// </value>
        /// <exception cref="ArgumentException">Acceptance ratio must be higher than 0 - startAcceptanceRatio</exception>
        public double StartAcceptanceRatio { get => startAcceptanceRatio;
            set
            {
                if (startAcceptanceRatio <= 0) throw new ArgumentException("Acceptance ratio must be higher than 0", nameof(startAcceptanceRatio));

                startAcceptanceRatio = value;
            }
        }
        /// <summary>
        /// Gets or sets the size of the grid.
        /// </summary>
        /// <value>
        /// The size of the grid.
        /// </value>
        /// <exception cref="ArgumentException">The grid size must be higher than 0 - gridSize</exception>
        public int GridSize { get => gridSize;
            set
            {
                if (gridSize <= 0) throw new ArgumentException("The grid size must be higher than 0", nameof(gridSize));

                gridSize = value;
            }
        }
        /// <summary>
        /// Gets or sets the mutation chance.
        /// </summary>
        /// <value>
        /// The mutation chance.
        /// </value>
        /// <exception cref="ArgumentException">The mutation chance size must be higher than 0 - mutationChance</exception>
        public double MutationChance { get => mutationChance;
            set
            {
                if (mutationChance <= 0) throw new ArgumentException("The mutation chance size must be higher than 0", nameof(mutationChance));

                mutationChance = value;
            }
        }

        public override string ToString()
        {
            StringBuilder _buffer = new StringBuilder();
            _buffer.Append('\n');
            foreach (PropertyInfo p in this.GetType().GetProperties().ToList())
            {
                _buffer.Append("  " + p.Name + " : " + (this.GetType().GetProperty(p.Name).GetValue(this, null) ?? "none").ToString() + "\n");
            }
            return _buffer.ToString(); ;
        }
    }
}
