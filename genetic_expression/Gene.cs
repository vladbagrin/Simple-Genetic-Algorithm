using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace genetic_expression
{
    class Gene
    {
        // The chance that a bit will
        // mutate
        private static double mutationChance = 0.04;

        // The gene value is stored here.
        // Only the 4 most significant
        // bits matter.
        private byte value = 0;

        // A mapping of gene values to
        // operands or operators.
        // Symbols + and - appear twice
        // so that no invalid symbol
        // is generated after a mutation.
        private static char[] symbols = {'0', '1', '2', '3', '4', '5', '6',
                                        '7', '8', '9', '+', '-', '*', '/',
                                        '+', '-'};

        /*
         * Number of types of characters.
         */
        private static int cSymbolsNumber = 16;

        /*
         * Constructor without parameters.
         * Generates a random gene.
         * The gene will be within the usable
         * range.
         */
        public Gene()
        {
            value = (byte) Chromosome.random.Next(cSymbolsNumber);
        }

        /*
         * Constructor with 1 parameter.
         * byte value: value of the gene
         */
        public Gene(byte value)
        {
            this.value = value;
        }

        public Gene(Gene a, Gene b)
        {
            int genePartIndex = Chromosome.random.Next(4);
            byte logicHelper = 0;

            for (int i = 0; i <= genePartIndex; i++)
            {
                logicHelper += (byte) Math.Pow(2, i);
            }

            byte vala = a.Value, valb = b.Value;
            byte valc = (byte) (valb & logicHelper);

            logicHelper = (byte) (~logicHelper - 240);
            valc += (byte)(vala & logicHelper);

            this.value = valc;
        }

        public void Mutate()
        {
            // Mutate only if the opportunity occurs
            if (Chromosome.random.NextDouble() > mutationChance)
                return;

            // Bit to flip
            int bitIndex = Chromosome.random.Next(4);
            byte booleanHelper = (byte) Math.Pow(2, bitIndex);

            if ((byte)(booleanHelper & value) == booleanHelper)
                value -= booleanHelper;
            else
                value += booleanHelper;
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }

        public byte Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public char Symbol
        {
            get { return symbols[Value]; }
        }

        public static int SymbolsNumber
        {
            get { return cSymbolsNumber; }
        }
    }
}
