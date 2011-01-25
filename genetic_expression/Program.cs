using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace genetic_expression
{
    class Program
    {
        public static bool cont = true;

        static void Main(string[] args)
        {
            Generation gen = new Generation(42);
            Console.Write(gen);

            while (cont)
            {
                gen = gen.NextGeneration();
                Console.Write(gen);
            }
        }
    }
}
