using System;
using System.Diagnostics;

namespace NeuralNetTest
{
    public class XoRInstance : AbstractInstance
    {
        public override void CreateInstance()
        {
            NeuralNetwork = new NeuralNetwork(0.7, 0.3);
            var in1 = NeuralNetwork.SetNeuron(NeuronTypeConst.InputNeuronType);
            var in2 = NeuralNetwork.SetNeuron(NeuronTypeConst.InputNeuronType);
            var h11 = NeuralNetwork.SetNeuron(NeuronTypeConst.FirstLayerHiddenNeuronType);
            var h12 = NeuralNetwork.SetNeuron(NeuronTypeConst.FirstLayerHiddenNeuronType);
            var h13 = NeuralNetwork.SetNeuron(NeuronTypeConst.FirstLayerHiddenNeuronType);
            var h14 = NeuralNetwork.SetNeuron(NeuronTypeConst.FirstLayerHiddenNeuronType);
            var h15 = NeuralNetwork.SetNeuron(NeuronTypeConst.FirstLayerHiddenNeuronType);
            var h21 = NeuralNetwork.SetNeuron(NeuronTypeConst.SecondLayerHiddenNeuronType);
            var h22 = NeuralNetwork.SetNeuron(NeuronTypeConst.SecondLayerHiddenNeuronType);
            var h23 = NeuralNetwork.SetNeuron(NeuronTypeConst.SecondLayerHiddenNeuronType);
            var h24 = NeuralNetwork.SetNeuron(NeuronTypeConst.SecondLayerHiddenNeuronType);
            var ou1 = NeuralNetwork.SetNeuron(NeuronTypeConst.OutputNeuronType);

            var s1 = NeuralNetwork.SetSynapse(in1, h11, 0.45);
            var s2 = NeuralNetwork.SetSynapse(in1, h12, 0.78);
            var s3 = NeuralNetwork.SetSynapse(in1, h13, -0.12);
            var s4 = NeuralNetwork.SetSynapse(in1, h14, 0.13);
            var s5 = NeuralNetwork.SetSynapse(in2, h12, 1.5);
            var s6 = NeuralNetwork.SetSynapse(in2, h13, -2.3);
            var s7 = NeuralNetwork.SetSynapse(in2, h14, 0.8);
            var s8 = NeuralNetwork.SetSynapse(in2, h15, 1.7);
            var s9 = NeuralNetwork.SetSynapse(h11, h21, -2.1);
            var s10 = NeuralNetwork.SetSynapse(h12, h21, -0.3);
            var s11 = NeuralNetwork.SetSynapse(h12, h22, -1.3);
            var s12 = NeuralNetwork.SetSynapse(h13, h22, 0.41);
            var s13 = NeuralNetwork.SetSynapse(h13, h23, 0.98);
            var s14 = NeuralNetwork.SetSynapse(h14, h23, 0.19);
            var s15 = NeuralNetwork.SetSynapse(h14, h24, -1.13);
            var s16 = NeuralNetwork.SetSynapse(h15, h24, 1.43);
            var s17 = NeuralNetwork.SetSynapse(h21, ou1, 2.1);
            var s18 = NeuralNetwork.SetSynapse(h22, ou1, -1.97);
            var s19 = NeuralNetwork.SetSynapse(h23, ou1, -0.98);
            var s20 = NeuralNetwork.SetSynapse(h24, ou1, 0.13);

            var rand = new Random();
            double error = 0;
            for (int i = 0; i < 10000; i++)
            {
                switch (rand.Next(1, 5))
                {
                    case 1:
                        {
                            Debug.WriteLine("Counter-{3}    {0} xor {1} = {2}", 0, 0, NeuralNetwork.Calculate(0, in1, 0, in2, out error, 0), i); break;
                        }
                    case 2:
                        {
                            Debug.WriteLine("Counter-{3}    {0} xor {1} = {2}", 1, 0, NeuralNetwork.Calculate(1, in1, 0, in2, out error, 1), i); break;
                        }
                    case 3:
                        {
                            Debug.WriteLine("Counter-{3}    {0} xor {1} = {2}", 0, 1, NeuralNetwork.Calculate(0, in1, 1, in2, out error, 1), i); break;
                        }
                    case 4:
                        {
                            Debug.WriteLine("Counter-{3}    {0} xor {1} = {2}", 1, 1, NeuralNetwork.Calculate(1, in1, 1, in2, out error, 0), i); break;
                        }
                    default:
                        break;
                }

            }

        }
    }
}
