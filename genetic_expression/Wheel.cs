using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace genetic_expression
{
    /*
     * This class helps choose genotypes with a
     * probability proportional to their fitness.
     */
    class Wheel
    {
        private ArrayList probabilityPool;
        private Generation generation;
        private int previousChoice = -1;

        public Wheel(Generation generation)
        {
            probabilityPool = new ArrayList();
            this.generation = generation;
            Generate();
        }

        public void Generate()
        {
            int size, i = 0;

            foreach (Chromosome chr in generation.Genotypes)
            {
                size = chr.Fitness;

                // size number of elements that point to the index of the
                // current chromosome. The larger is size, the higher the
                // probability to be chosen later.
                probabilityPool.AddRange(Enumerable.Repeat(i, size).ToArray());
                i++;
            }
        }

        public Chromosome Choice()
        {
            int indexInPool;

            if (previousChoice == -1)
                indexInPool = Chromosome.random.Next(probabilityPool.Count);
            else
                do
                {
                    indexInPool = Chromosome.random.Next(probabilityPool.Count);
                }
                while (previousChoice == indexInPool);

            previousChoice = indexInPool;

            int indexInGeneration = (int) probabilityPool[indexInPool];
            return (Chromosome) generation.Genotypes[indexInGeneration];
        }
    }
}
