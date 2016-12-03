using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace СШІ3
{
     class S
    {
        public double Plus { get; set; }
        public double Minus { get; set; }

        public S(double plus, double negative)
        {
            Plus = plus;
            Minus = negative;
        }

        public double Calc(int positive, double ph)
        {
            if (positive == 0)
            {
                return Unknown(ph);
            }
            return positive == 1 ? Postive(ph) : Negative(ph);
        }
        public double Postive(double ph)
        {
            return (Plus * ph) / (Plus * ph + Minus * (1 - ph));
        }
        public double Negative(double ph)
        {
            return ((1 - Plus) * ph) / (1 - Plus * ph - Minus * (1 - ph));
        }

        public double Unknown(double ph)
        {
            return ph;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            var m = new[]
            {
                new S[]{
                    new S(0.8, 0.4),
                    new S(0.4, 0.5),
                    new S(0.3, 0.7),
                },
                new S[]
                {
                    new S(0.4, 0.8),
                    new S(0.5, 0.4),
                    new S(0.7, 0.3),
                },
                new S[]
                {
                    new S(0.6, 0.8),
                    new S(0.8, 0.3),
                    new S(0.1, 0.6),
                }
            };
            for (var i = 0; i < m.Length; i++)
            {
                var str1 = string.Join(" ", m[i].Select(x => x.Plus));
                var str2 = string.Join(" ", m[i].Select(x => x.Minus));

                Console.WriteLine("{0}\t{1}", str1, str2);
            }

            var phs = new[] { 0.6, 0.3, 0.1 };

            var pMax = new double[phs.Length];

            for (var i = 0; i < phs.Length; i++)
            {
                pMax[i] = GetPMax(m[i], phs[i]);
            }

            Console.WriteLine(string.Join(Environment.NewLine, pMax.Select((x, i) => "Pmax[" + i + "]: " + x)));

            var result = phs.ToArray();

            for (var i = 0; i < result.Length; i++)
            {
                Console.WriteLine("Do you have S" + i + 1 + "?");
                var has = ReadAnswer();

                for (var j = 0; j < phs.Length; j++)
                {
                    var t = has;

                    if (has == 2)
                    {
                        t = 1;
                    }
                    if (has == -2)
                    {
                        t = -1;
                    }

                    var orign = m[j][i].Calc(t, result[j]);
                    var temp = orign;

                    if (has == 1 || has == -1)
                    {
                        var delta = (Math.Abs(temp - result[j]))/2*has;
                        temp = result[j] + delta;

                        Console.WriteLine("New = [" + temp.ToString("F2") + "],Was = [" + result[j].ToString("F2") + "]" );
                    }
                    else
                    {
                        Console.WriteLine("New = [" + orign.ToString("F2") + "], Was = ["  + result[j].ToString("F2") + "]");
                    }
                    result[j] = temp;
                }
            }

            for (int i = 0; i < result.Length; i++)
            {
                WriteResult(pMax[i], result[i]);
            }
            Console.ReadKey();
        }

        protected static void WriteResult(double pmax, double r)
        {
            var u = pmax * 0.8;
            var d = pmax * 0.2;
            string msg = "Need more detail";
            if (r >= u)
            {
                msg = "Yes you have P";
            }
            if (r <= d)
            {
                msg = "No, you have not P";
            }
            Console.WriteLine("Result: " + msg + "  " + r.ToString("F2") +  "");
        }

        private static int ReadAnswer()
        {
            var result = Console.ReadLine();

            switch (result)
            {
                case "2":
                case "y":
                case "yes":
                    return 2;
                case "-2":
                case "n":
                case "no":
                    return -2;
                case "1":
                case "yn":
                    return 1;
                case "-1":
                case "ny":
                    return -1;
                default:
                    return 0;

            }
        }

        private static double GetPMax(S[] sym, double ph)
        {
            var r = ph;
            foreach (var s in sym)
            {

                if (s.Minus > s.Plus)
                {
                    r = s.Negative(r);
                }
                else
                {
                    r = s.Postive(r);
                }
            }
            return r;
        }
    }
}
