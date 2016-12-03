using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СШІ4
{
    public class Neuron
    {

        public int[] x;
        public float[] w;
        public bool isOdd;
        public int y;

        public Neuron(int[] neuronData, int bl, Random random)
        {

            this.y = bl;
            x = neuronData;
            w = new float[x.Length];

            for (int i = 0; i < x.Length; i++)
                w[i] = (float)random.NextDouble();
        }

        public float GetNETWithCustomInputData(string numberInput)
        {
            int[] numberData = numberInput.Select(x => int.Parse(x.ToString())).ToArray();
            float NET = 0f;
            for (int i = 0; i < w.Length; i++)
                NET += w[i] * numberData[i];
            return NET;
        }

        public float GetBaseNET()
        {
            float NET = 0f;
            for (int i = 0; i < w.Length; i++)
                NET += w[i] * x[i];
            return NET;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] latter = new string[10]{
                "a",
                "e",
                "y",
                "i",
                "o",
                "r",
                "t",
                "s",
                "d",
                ""
            };
            var random = new Random();
            string[] numbers = new string[10] {
            "111001111100111",
            "001011101001001",
            "111001111100111",
            "111001111001111",
            "101101111001001",
            "111100111001111",
            "111100111101111",
            "111001001010100",
            "111101111101111",
            "111101111001111" };
            int[] isParne = new int[10]{
                0,
                1,
                0,
                1,
                0,
                1,
                0,
                1,
                0,
                1
            };

            Neuron main = new Neuron(numbers[0].Select(x => int.Parse(x.ToString())).ToArray(), 0, random);
            List<Neuron> neurons = new List<Neuron>();
            List<int> glasnie = new List<int>() { 0, 4, 7, 10, 15 };
            int isOdd = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                isOdd = isParne[i];
                Neuron n = new Neuron(numbers[i].Select(x => int.Parse(x.ToString())).ToArray(), isOdd, random);
                isOdd = isOdd == 0 ? 1 : 0;
                neurons.Add(n);
            }
            bool isEpoha = true;
            int countEpohs = 0;
            bool isBreak = false;
            int y = -1;
            float delta = 0f;
            float nu = 0.1f;
            float teta = 0.5f;
            do
            {
                countEpohs++;
                isEpoha = true;
                foreach (var n in neurons)
                {
                    do
                    {
                        float NET = 0f;
                        for (int i = 0; i < main.w.Length; i++)
                            NET += main.w[i] * n.x[i];
                        if (NET < teta)
                            y = 0;
                        else
                            y = 1;
                        delta = n.y - y;
                        if (delta != 0 && !isBreak)
                        {
                            isEpoha = false;
                            for (int i = 0; i < n.w.Length; i++)
                                main.w[i] = main.w[i] + (delta * nu * n.x[i]);
                        }
                    } while (delta != 0);
                }
            } while (!isEpoha);
            Console.WriteLine("Количество эпох = " + countEpohs);
            for (;;)

            {
                Console.Write("Введите цифру = ");
                int temp = 0;
                bool t = int.TryParse(Console.ReadLine(), out temp);
                if (!t || temp >9)
                    return ;
                string input = numbers[temp];

                var nda = input.Select(x => int.Parse(x.ToString())).ToArray();
                float NET1 = 0f;
                for (int i = 0; i < main.w.Length; i++)
                    NET1 += main.w[i] * nda[i];

                if (NET1 < teta)
                    y = 0;
                else
                    y = 1;
                if (y == 0)
                    Console.Write("парная");
                else if (y == 1)
                    Console.Write("не парная");
                Console.WriteLine();

            }            
        }
    }
}
