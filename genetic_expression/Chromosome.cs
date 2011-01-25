using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace genetic_expression
{
    class Chromosome
    {
        private static int cMaxGeneNumber = 15;
        private ArrayList genes;
        private int fitness;

        // Static random number generator.
        // It has to be static so that it
        // does not generate the same sequence
        // for new chromosomes, in case of duplicate
        // seed value.
        public static Random random = new Random();

        /*
         * Constructor without parameters.
         * Generates a random set of genes.
         */
        public Chromosome()
        {
            //int geneNumber = random.Next(1, cMaxGeneNumber);
            int geneNumber = cMaxGeneNumber;

            genes = new ArrayList();

            for (int i = 1; i <= geneNumber; i++)
            {
                genes.Add(new Gene((byte) random.Next(Gene.SymbolsNumber)));
            }
        }

        public Chromosome(ArrayList genes)
        {
            this.genes = genes;
        }

        public void Mutate()
        {
            foreach (Gene gene in genes)
            {
                gene.Mutate();
            }
        }

        /*
         * Returns the ArrayList of genes.
         */
        public ArrayList Genes
        {
            get { return (ArrayList) genes.Clone(); }
        }

        public int Fitness
        {
            get { return fitness; }
        }

        public override string ToString()
        {
            string str = "";

            foreach (Gene gene in genes)
            {
                str += gene;
            }

            return str;
        }

        public void ComputeFitness(double target)
        {
            double delta = Math.Abs(target - ExpressionParser.Evaluate(ExpressionParser.Normalize(ToString())));

            // Found a solution
            if (delta == 0)
            {
                fitness = 0;
            }
            else
                // Fitness is inversely proportional to the difference between
                // the target result and the one evaluated from the fenotype.
                fitness = (int)Math.Ceiling(1 / delta * 1000);

            // Trying to limit memory usage
            if (fitness > 5000 || fitness < 0)
                fitness = 5000;
        }
    }
}