using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace genetic_expression
{
    class Generation
    {
        ArrayList genotypes;
        private static int populationSize = 20;
        private double target;
        private static double crossoverProbability = 0.7;

        // Keep track of the number of generations
        private static int generationNumber = 0;

        public Generation(double target)
        {
            genotypes = new ArrayList();
            generationNumber++;
            this.target = target;
            Randomize();
        }

        public Generation(double target, ArrayList genotypes)
        {
            this.genotypes = genotypes;
            generationNumber++;
            this.target = target;
        }

        public void Randomize()
        {
            Chromosome chromosome;

            for (int i = 1; i <= populationSize; i++)
            {
                chromosome = new Chromosome();

                chromosome.ComputeFitness(target);
                genotypes.Add(chromosome);
            }
        }

        public override string ToString()
        {
            string exp, norm;
            double val;
            string str = "\n\nGeneration " + Number + ":\n";

            foreach (Chromosome chr in genotypes)
            {
                exp = chr.ToString();
                norm = ExpressionParser.Normalize(exp);
                val = ExpressionParser.Evaluate(norm);

                if (val == target)
                {
                    str += "-----------------------------------------------------\n";
                    Program.cont = false;
                }

                str += "[" + exp + "] " + norm + " = " + val + " [" + chr.Fitness + "]\n";

                if (val == target)
                {
                    str += "-----------------------------------------------------\n";
                }
            }

            return str;
        }

        public Chromosome Recombine(Chromosome a, Chromosome b)
        {
            if (a == b)
                return null;

            // Chance to recombine means 2 fenotypes that meet
            // don't always create a new one.
            if (Chromosome.random.NextDouble() > crossoverProbability)
                return null;

            int partitionIndex = Chromosome.random.Next(Math.Min(a.Genes.Count, b.Genes.Count));
            Gene gene = new Gene((Gene) a.Genes[partitionIndex], (Gene) b.Genes[partitionIndex]);
            ArrayList list = new ArrayList();

            for (int i = 0; i < partitionIndex; i++)
            {
                list.Add(a.Genes[i]);
            }
            list.Add(gene);
            for (int i = partitionIndex + 1; i < b.Genes.Count; i++)
            {
                list.Add(b.Genes[i]);
            }

            Chromosome returnValue = new Chromosome(list);
            returnValue.Mutate();
            returnValue.ComputeFitness(target);

            return returnValue;
        }

        public Generation NextGeneration()
        {
            ArrayList list = new ArrayList();
            Chromosome a, b, c;
            int size = 0;
            Wheel wheel = new Wheel(this);

            while (size < populationSize)
            {
                a = wheel.Choice();
                b = wheel.Choice();
                c = Recombine(a, b);

                if (c != null)
                {
                    list.Add(c);
                    size++;
                }
            }

            return new Generation(target, list);
        }

        public int Number
        {
            get { return generationNumber; }
        }

        public ArrayList Genotypes
        {
            get { return genotypes; }
        }
    }
}
