﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NN_lr5
{
    //Neural Network 
    [Serializable]
    public class NN
    {
        public double LearnRate { get; set; }
        public double Momentum { get; set; }
        public List<N> InputLayer { get; set; }
        public List<N> OutputLayer { get; set; }
        public List<N> HiddenLayer { get; set; }

        public NN(int inputSize, int hiddenSize, int outputSize)
        {
            LearnRate = .9;
            Momentum = .04;
            InputLayer = new List<N>();
            HiddenLayer = new List<N>();
            OutputLayer = new List<N>();

            for (int i = 0; i < inputSize; i++)
                InputLayer.Add(new N());
            for (int i = 0; i < hiddenSize; i++)
                HiddenLayer.Add(new N(InputLayer));
            for (int i = 0; i < outputSize; i++)
                OutputLayer.Add(new N(HiddenLayer));
        }

        public void TrainNN(params double[] inputs)
        {
            int i = 0;
            InputLayer.ForEach(x => x.y = inputs[i++]);
            HiddenLayer.ForEach(x => x.getY());
            OutputLayer.ForEach(x => x.getY());
        }

        public double[] Compute(params double[] inputs)
        {
            TrainNN(inputs);
            return OutputLayer.Select(x => x.y).ToArray();
        }

        public double CalculateError(params double[] targets)
        {
            int i = 0;
            return OutputLayer.Sum(a => Math.Abs(a.getErr(targets[i++])));
        }

        public void BackPropagate(params double[] vars)
        {
            int i = 0;
            OutputLayer.ForEach(x => x.getGrad(vars[i++]));
            HiddenLayer.ForEach(x => x.getGrad());
            HiddenLayer.ForEach(x => x.rebuildW(LearnRate, Momentum));
            OutputLayer.ForEach(x => x.rebuildW(LearnRate, Momentum));
        }

        static Random random = new Random();

        public static double GetRand()
        {
            return 2 * random.NextDouble() - 1;
        }

        public static double SigmoidFunction(double x)
        {
            //if (x < -45.0) return 0.0;
            //else if (x > 45.0) return 1.0;
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        public static double SigmoidDerivative(double f)
        {
            return f * (1 - f);
        }
    }

    //Neuron : neuron instance
    [Serializable]
    public class N
    {
        public List<S> InputS { get; set; }
        public List<S> OutputS { get; set; }

        public double bias { get; set; }
        public double biasd { get; set; }
        public double grad { get; set; }
        public double y { get; set; }

        public N()
        {
            InputS = new List<S>();
            OutputS = new List<S>();
            bias = NN.GetRand();
        }

        public N(List<N> inputNeurons)
            : this()
        {
            foreach (var n in inputNeurons)
            {
                var s = new S(n, this);
                n.OutputS.Add(s);
                InputS.Add(s);
            }
        }

        public double getY()
        {
            return y = NN.SigmoidFunction(InputS.Sum(x => x.w * x.InputN.y) + bias);
        }

        public double getDerivative()
        {
            return NN.SigmoidDerivative(y);
        }

        public double getErr(double right)
        {
            return right - y;
        }

        public double getGrad(double right)
        {
            return grad = getErr(right) * getDerivative();
        }

        public double getGrad()
        {
            return grad = OutputS.Sum(x => x.OutputN.grad * x.w) * getDerivative();
        }

        public void rebuildW(double learnRate, double momentum)
        {
            var prevDelta = biasd;
            biasd = learnRate * grad;
            bias += biasd + momentum * prevDelta;

            foreach (var s in InputS)
            {
                prevDelta = s.wd;
                s.wd = learnRate * grad * s.InputN.y;
                s.w += s.wd + momentum * prevDelta;
            }

        }

    }

    //Synapse : relation between neurons
    [Serializable]
    public class S
    {
        public N InputN { get; set; }
        public N OutputN { get; set; }
        public double w { get; set; }
        public double wd { get; set; }

        public S(N inputN, N outputN)
        {
            InputN = inputN;
            OutputN = outputN;
            w = NN.GetRand();
        }

    }



    class Program
    {

        static void Main(string[] args)
        {
            var network = new NN(15, 15, 10);
            const int iterations = 100;

            List<double[]> teachSet = new List<double[]>{
               new double[15]{1,1,1,1,0,1,1,0,1,1,0,1,1,1,1},
               new double[15]{0,0,1,0,0,1,0,0,1,0,0,1,0,0,1},
               new double[15] {1,1,1,0,0,1,1,1,1,1,0,0,1,1,1},
               new double[15] {1,1,1,0,0,1,1,1,1,0,0,1,1,1,1},
               new double[15] {1,0,1,1,0,1,1,1,1,0,0,1,0,0,1},
               new double[15] {1,1,1,1,0,0,1,1,1,0,0,1,1,1,1},
               new double[15] {1,1,1,1,0,0,1,1,1,1,0,0,1,1,1},
               new double[15]{1,1,1,0,0,1,0,0,1,0,1,0,1,0,0},
               new double[15] {1,1,1,1,0,1,1,1,1,1,0,1,1,1,1},
               new double[15] {1,1,1,1,0,1,1,1,1,0,0,1,1,1,1}
            };

            List<double[]> teachAnswers = new List<double[]>{
                new double[10] {1,0,0,0,0,0,0,0,0,0},
                new double[10] {0,1,0,0,0,0,0,0,0,0},
                new double[10] {0,0,1,0,0,0,0,0,0,0},
                new double[10] {0,0,0,1,0,0,0,0,0,0},
                new double[10] {0,0,0,0,1,0,0,0,0,0},
                new double[10] {0,0,0,0,0,1,0,0,0,0},
                new double[10] {0,0,0,0,0,0,1,0,0,0},
                new double[10] {0,0,0,0,0,0,0,1,0,0},
                new double[10] {0,0,0,0,0,0,0,0,1,0},
                new double[10] {0,0,0,0,0,0,0,0,0,1},
            };

            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < teachSet.Count; j++)
                {
                    network.TrainNN(teachSet[j]);
                    network.BackPropagate(teachAnswers[j]);
                }
            }

            Console.WriteLine("Количество итераций : " + iterations);
            Console.WriteLine();

            for (;;)
            {
                Console.Write("Введите цифру = ");
                int a = int.Parse(Console.ReadLine());

                var output = network.Compute(teachSet[a]);
                Console.WriteLine();
                Console.WriteLine("Вероятности");
                double max = double.MinValue;
                int ind = -1;

                for (int i = 0; i < output.Length; i++)
                {
                    Console.WriteLine("Вероятность цифры " + i + ": " + output[i]);
                    if (output[i] > max)
                    {
                        max = output[i];
                        ind = i;
                    }
                }

                Console.WriteLine();
                Console.Write("Это цифра: " + ind);
                Console.WriteLine();

            }
            Console.ReadKey();

        }
    }
}
