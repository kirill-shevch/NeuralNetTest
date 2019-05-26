using System;
using System.Collections.Generic;

namespace NeuralNetTest
{
    public class SineInstance : AbstractInstance
    {
        public override void CreateInstance()
        {
            var r = new Random();
            //NeuralNetwork = new NeuralNetwork(0.0009, 0.0005);
            NeuralNetwork = new NeuralNetwork(0.7, 0.3);


            var in1 = NeuralNetwork.SetNeuron(NeuronTypeConst.InputNeuronType);
            var in2 = NeuralNetwork.SetNeuron(NeuronTypeConst.InputNeuronType);
            var h1 = NeuralNetwork.SetNeuron(NeuronTypeConst.FirstLayerHiddenNeuronType);
            var h2 = NeuralNetwork.SetNeuron(NeuronTypeConst.FirstLayerHiddenNeuronType);
            var h3 = NeuralNetwork.SetNeuron(NeuronTypeConst.FirstLayerHiddenNeuronType);
            var h4 = NeuralNetwork.SetNeuron(NeuronTypeConst.FirstLayerHiddenNeuronType);
            var ou1 = NeuralNetwork.SetNeuron(NeuronTypeConst.OutputNeuronType);

            var s1 = NeuralNetwork.SetSynapse(in1, h1, GetNext(r));
            var s2 = NeuralNetwork.SetSynapse(in1, h2, GetNext(r));
            var s3 = NeuralNetwork.SetSynapse(in1, h3, GetNext(r));
            var s4 = NeuralNetwork.SetSynapse(in2, h2, GetNext(r));
            var s5 = NeuralNetwork.SetSynapse(in2, h3, GetNext(r));
            var s6 = NeuralNetwork.SetSynapse(in2, h4, GetNext(r));
            var s7 = NeuralNetwork.SetSynapse(h1, ou1, GetNext(r));
            var s8 = NeuralNetwork.SetSynapse(h2, ou1, GetNext(r));
            var s9 = NeuralNetwork.SetSynapse(h3, ou1, GetNext(r));
            var s10 = NeuralNetwork.SetSynapse(h4, ou1, GetNext(r));

            #region commented
            //var in1 = NeuralNetwork.SetNeuron(NeuronTypeConst.InputNeuronType);
            //var in2 = NeuralNetwork.SetNeuron(NeuronTypeConst.InputNeuronType);
            //var ou1 = NeuralNetwork.SetNeuron(NeuronTypeConst.OutputNeuronType);
            //var synapses = new List<int>();
            //var hiddens2 = new List<int>();
            //var hiddens1 = new List<int>();
            //for (int i = 0; i < 95; i++)
            //{
            //    var index = NeuralNetwork.SetNeuron(NeuronTypeConst.FirstLayerHiddenNeuronType);
            //    hiddens1.Add(index);
            //    if (i < 20)
            //    {
            //        synapses.Add(NeuralNetwork.SetSynapse(in1, index, GetNext(r)));
            //    }
            //    else if (i > 74)
            //    {
            //        synapses.Add(NeuralNetwork.SetSynapse(in2, index, GetNext(r)));
            //    }
            //    else
            //    {
            //        synapses.Add(NeuralNetwork.SetSynapse(in1, index, GetNext(r)));
            //        synapses.Add(NeuralNetwork.SetSynapse(in2, index, GetNext(r)));
            //    }
            //}
            //for (int i = 0; i < 94; i++)
            //{
            //    var index = NeuralNetwork.SetNeuron(NeuronTypeConst.SecondLayerHiddenNeuronType);
            //    hiddens2.Add(index);
            //    synapses.Add(NeuralNetwork.SetSynapse(hiddens1[i], index, GetNext(r)));
            //    synapses.Add(NeuralNetwork.SetSynapse(hiddens1[i + 1], index,  GetNext(r)));
            //    synapses.Add(NeuralNetwork.SetSynapse(index, ou1, GetNext(r)));
            //}
            #endregion commented


            double x = 0, y1 = 0, y2 = 0, answer = 0;
            double error = 0;

            x += 0.1;
            y1 = (1 + Math.Cos(x)) / 2;

            x += 0.1;
            y2 = (1 + Math.Cos(x)) / 2;

            while (x < 50000)
            {
                x += 0.1;
                answer = (1 + Math.Cos(x)) / 2;
                Console.WriteLine("Counter-{3}    y1:{0}, y2:{1}, prediction:{2}, answer:{4} error:{5}", y1, y2, NeuralNetwork.Calculate(y1, in1, y2, in2, out error, answer), x, answer, error);
                y1 = y2;
                y2 = answer;
            }

        }

        private double GetNext(Random r)
        {
            return Math.Round((r.NextDouble() - 0.5) * 4, 2);
        }
    }
}
