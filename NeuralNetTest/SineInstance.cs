using System;

namespace NeuralNetTest
{
    public class SineInstance : AbstractInstance
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

            double x = 0, y1 = 0, y2 = 0, answer = 0;

            x += 0.01;
            y1 = (1 + Math.Cos(x)) / 2;

            x += 0.01;
            y2 = (1 + Math.Cos(x)) / 2;

            while (x < 10000)
            {
                x += 0.01;
                answer = (1 + Math.Cos(x)) / 2;
                Console.WriteLine("Counter-{3}    y1:{0}, y2:{1}, next:{2}", y1, y2, NeuralNetwork.Calculate(y1, in1, y2, in2, answer, true), x);
                y1 = y2;
                y2 = answer;
            }

        }
    }
}
