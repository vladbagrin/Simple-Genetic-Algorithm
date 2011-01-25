using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace genetic_expression
{
    class ExpressionParser
    {
        /*
         * Normalizes the expression contained
         * in a string to be syntactically correct.
         */
        public static string Normalize(string exp)
        {
            int i = 0, j;

            exp = ValidStartExpression(exp);

            while (i < exp.Length)
            {
                // Ignores all operators following
                // an operator considered valid
                if (exp[i] == '+' || exp[i] == '-' ||
                    exp[i] == '*' || exp[i] == '/')
                {
                    j = NextDigit(exp, i + 1);

                    // No more digits after this
                    // means everything else will be
                    // ignored
                    if (j >= 0)
                    {
                        exp = exp.Substring(0, i + 1) + exp.Substring(j);
                        i = i + 1;
                    }
                    else
                    {
                        exp = exp.Substring(0, i);
                        break;
                    }
                }
                else
                {
                    j = NextOperator(exp, i + 1);
                    if (j < 0)
                        break;
                    else
                        i = j;
                }
            }

            return exp;
        }

        public static string ValidStartExpression(string exp)
        {
            int i = ValidStart(exp);

            // In case no valid start is found
            if (i < 0)
                return "";

            // In case the beginning needs to be
            // ignored
            if (i > 0)
            {
                exp = exp.Substring(i);
            }

            return exp;
        }

        public static int ValidStart(string exp)
        {
            return exp.IndexOfAny("123456789-".ToCharArray());
        }

        public static int NextDigit(string exp, int i)
        {
            return exp.IndexOfAny("123456789".ToCharArray(), i);
        }

        public static int NextOperator(string exp, int i)
        {
            return exp.IndexOfAny("+-*/".ToCharArray(), i);
        }

        /*
         * Recursively evaluate an expression
         * contained in a string
         */
        public static double Evaluate(string exp)
        {
            int partitionIndex = exp.IndexOfAny("+".ToCharArray());
            double left, right;

            // There are sums in the expression.
            // The sum operation is the most favoured in
            // precedence rules.
            if (partitionIndex >= 0)
            {
                left = Evaluate(exp.Substring(0, partitionIndex));
                right = Evaluate(exp.Substring(partitionIndex + 1));

                return left + right;
            }

            partitionIndex = exp.IndexOfAny("-".ToCharArray());

            // There are subtractions in the expression
            if (partitionIndex >= 0)
            {
                left = Evaluate(exp.Substring(0, partitionIndex));
                right = Evaluate(exp.Substring(partitionIndex + 1));

                return left - right;
            }

            partitionIndex = exp.IndexOfAny("*/".ToCharArray());

            // There are multiplications or divisions in the expression
            if (partitionIndex >= 0)
            {
                left = Evaluate(exp.Substring(0, partitionIndex));
                right = Evaluate(exp.Substring(partitionIndex + 1));

                return exp[partitionIndex] == '*' ? left * right : left / right;
            }

            // These instructions are executed if no more
            // operations are left in the expression
            if (exp.Length > 0)
                return Double.Parse(exp);
            else
                return 0;
        }
    }
}
