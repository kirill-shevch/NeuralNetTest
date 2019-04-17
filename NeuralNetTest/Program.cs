using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var neuralNetwork = new NeuralNetwork(0.7, 0.3);
            var in1 = neuralNetwork.SetNeuron(NeuronTypeConst.InputNeuronType);
            var in2 = neuralNetwork.SetNeuron(NeuronTypeConst.InputNeuronType);
            var h1 = neuralNetwork.SetNeuron(NeuronTypeConst.HiddenNeuronType);
            var h2 = neuralNetwork.SetNeuron(NeuronTypeConst.HiddenNeuronType);
            var ou1 = neuralNetwork.SetNeuron(NeuronTypeConst.OutputNeuronType);

            var s1 = neuralNetwork.SetSynapse(in1, h1, 0.45);
            var s2 = neuralNetwork.SetSynapse(in1, h2, 0.78);
            var s3 = neuralNetwork.SetSynapse(in2, h1, -0.12);
            var s4 = neuralNetwork.SetSynapse(in2, h2, 0.13);
            var s5 = neuralNetwork.SetSynapse(h1, ou1, 1.5);
            var s6 = neuralNetwork.SetSynapse(h2, ou1, -2.3);

            for (int i = 0; i < 109000; i++)
            {
                Console.WriteLine(neuralNetwork.Calculate(1, in1, 0, in2, 1));
            }
            Console.ReadKey();
        }
    }
}

