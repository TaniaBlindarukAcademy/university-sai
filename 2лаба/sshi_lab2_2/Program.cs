using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sshi_lab_2
{
    class Program
    {
        static double aprior_h = 0.6;
        static double[] p_plus = { 0.8, 0.4, 0.3 };
        static double[] p_minus = { 0.4, 0.5, 0.7 };
        static string[] simptoms = { "высокая температура", "головная боль", "кашель" };
        static int n = 3;

        static void Main(string[] args)
        {
            double h = aprior_h;
            Console.WriteLine(h.ToString());
            int i = 0;
            foreach (string s in simptoms)
            {                
                Console.WriteLine("У вас есть симптом: " + s + "?");
                Console.WriteLine("\"y\" - \"да\", \"n\" - \"нет\", \"x\" - \"не знаю\"");                
                double result = calculate(Console.ReadLine(), i, h);
                h = result;
                i++;
                Console.WriteLine(h.ToString());
            }
            if (h > 0.8 && h < 1)
                Console.WriteLine("Диагноз подтверждён.");
            else if ((h > 0 && h < 0.2))
                Console.WriteLine("Диагноз опровержен.");
            else
                Console.WriteLine("Диагноз неизвестен.");
            /*for (int i = 0; i < n; i++)
            {
                Console.WriteLine(aprior_h.ToString() + "\t|\t" + result[i]);
            }*/
            Console.ReadKey();
        }

        static double calculate(string w, int i, double h)
        {
            double new_h = 0;
            switch (w)
            {
                case "y":
                    
                        new_h = (p_plus[i] * h) / ((p_plus[i] * h) + p_minus[i] * (1 - h));
                    
                    break;
                case "n":
                    
                        new_h = ((1 - p_plus[i]) * h) / (1 - (p_plus[i] * h) - p_minus[i] * (1 - h));
                    
                    break;
                case "x":
                    new_h = h;
                    break;
            }
            return new_h;
        }
    }
}