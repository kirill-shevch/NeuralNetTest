using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetTest
{
    public class StockInstance : AbstractInstance
    {
        public override void CreateInstance()
        {
            var r = new Random();
            NeuralNetwork = new NeuralNetwork(0.9, 0.5);
            var in1 = NeuralNetwork.SetNeuron(NeuronTypeConst.InputNeuronType);
            var in2 = NeuralNetwork.SetNeuron(NeuronTypeConst.InputNeuronType);
            var ou1 = NeuralNetwork.SetNeuron(NeuronTypeConst.OutputNeuronType);
            var synapses = new List<int>();
            var hiddens2 = new List<int>();
            var hiddens1 = new List<int>();
            for (int i = 0; i < 50; i++)
            {
                var index = NeuralNetwork.SetNeuron(NeuronTypeConst.FirstLayerHiddenNeuronType);
                hiddens1.Add(index);
                if (i < 10)
                {
                    synapses.Add(NeuralNetwork.SetSynapse(in1, index, GetNext(r)));
                }
                else if (i > 39)
                {
                    synapses.Add(NeuralNetwork.SetSynapse(in2, index, GetNext(r)));
                }
                else
                {
                    synapses.Add(NeuralNetwork.SetSynapse(in1, index, GetNext(r)));
                    synapses.Add(NeuralNetwork.SetSynapse(in2, index, GetNext(r)));
                }
            }
            for (int i = 0; i < 49; i++)
            {
                var index = NeuralNetwork.SetNeuron(NeuronTypeConst.SecondLayerHiddenNeuronType);
                hiddens2.Add(index);
                synapses.Add(NeuralNetwork.SetSynapse(hiddens1[i], index, GetNext(r)));
                synapses.Add(NeuralNetwork.SetSynapse(hiddens1[i + 1], index, GetNext(r)));
                synapses.Add(NeuralNetwork.SetSynapse(index, ou1, GetNext(r)));
            }

            var directory = @"D:\PrivateRepos\NeuralWeb\NeuralNetTest\StockData\MSFT.txt";
            var stockData = new List<double>();
            using (StreamReader sr = new StreamReader(directory))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    double d;
                    if (Double.TryParse(line, out d))
                    {
                        stockData.Add(d / 1000);
                    }
                }
            }

            double error = 0;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Counter-{3}    y1:{0}, y2:{1}, prediction:{2}, answer:{4} error:{5}",
                       stockData[0],
                       stockData[1],
                       NeuralNetwork.Calculate(stockData[0], in1, stockData[1], in2, out error, stockData[2]),
                       i,
                       stockData[2],
                       error);
            }

            for (int i = 2; i < stockData.Count; i++)
            {
                Console.WriteLine("Counter-{3}    y1:{0}, y2:{1}, prediction:{2}, answer:{4} error:{5}",
                       stockData[i - 2],
                       stockData[i - 1],
                       NeuralNetwork.Calculate(stockData[i - 2], in1, stockData[i - 1], in2, out error, stockData[i]),
                       i - 2,
                       stockData[i],
                       error);
            }
               
            //for (int j = 0; j < 20; j++)
            //{
            //    for (int i = 2; i < stockData.Count; i++)
            //    {
            //        for (int k = 0; k < 3; k++)
            //        {

            //        }

            //    }
            //}
        }

        private double GetNext(Random r)
        {
            return Math.Round((r.NextDouble() - 0.5) * 0.5, 2);
        }
    }
}
