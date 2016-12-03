using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sshi_lab_2
{
    class Program
    {
        static double[] aprior_h = { 0.6, 0.3, 0.1 };
        static double[] p_plus = { 0.8, 0.4, 0.3 };
        static double[] p_minus = { 0.4, 0.5, 0.7 };
        static string[] simptoms = { "высокая температура"};
        static int n = 3;

        static void Main(string[] args)
        {
            Console.WriteLine("У вас есть симптом: " + simptoms[0] + "?");
            Console.WriteLine("\"y\" - \"да\", \"n\" - \"нет\", \"x\" - \"не знаю\"");
            double[] result = new double[n];
            result = calculate(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(aprior_h[i].ToString() + "\t|\t" + result[i]);
            }
            Console.ReadKey();
        }

        static double[] calculate(string w)
        {
            double[] new_h = new double[n];
            switch (w)
            {
                case "y":
                    for (int i = 0; i < n; i++)
                    {
                        new_h[i] = (p_plus[i] * aprior_h[i]) / ((p_plus[i] * aprior_h[i]) + p_minus[i] * (1 - aprior_h[i]));
                    }
                    break;
                case "n":
                    for (int i = 0; i < n; i++)
                    {
                        new_h[i] = ((1 - p_plus[i]) * aprior_h[i]) / (1 - (p_plus[i] * aprior_h[i]) - p_minus[i] * (1 - aprior_h[i]));
                    }
                    break;
                case "x":
                    new_h = aprior_h;
                    break;
            }
            return new_h;
        }
    }
}