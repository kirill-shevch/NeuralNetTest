using System;

namespace NeuralNetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region xor staff

            //var xor = new XoRInstance();
            ////xor.CreateInstance();
            ////xor.SaveInstance(@"E:\Repositories\NeuralWeb\NeuralNetTest\XoR.txt");
            //xor.LoadInstance(@"E:\Repositories\NeuralWeb\NeuralNetTest\XoR.txt");
            //var inputs = xor.NeuralNetwork.Neurons.Where(n => n.Value.NeuronType == (byte)NeuronTypeConst.InputNeuronType).ToList();
            //Console.WriteLine("{0} xor {1} = {2}", 0, 0, xor.NeuralNetwork.Calculate(0, inputs[0].Key, 0, inputs[1].Key));
            //Console.WriteLine("{0} xor {1} = {2}", 0, 1, xor.NeuralNetwork.Calculate(0, inputs[0].Key, 1, inputs[1].Key));
            //Console.WriteLine("{0} xor {1} = {2}", 1, 0, xor.NeuralNetwork.Calculate(1, inputs[0].Key, 0, inputs[1].Key));
            //Console.WriteLine("{0} xor {1} = {2}", 1, 1, xor.NeuralNetwork.Calculate(1, inputs[0].Key, 1, inputs[1].Key));
            //Console.ReadKey();

            #endregion xor staff

            #region sine staff

            //var sine = new SineInstance();
            //sine.CreateInstance();
            //sine.SaveInstance(@"C:\ApperSine.txt");
            //sine.SaveInstance(@"E:\Repositories\NeuralWeb\NeuralNetTest\Sine.txt");
            //sine.LoadInstance(@"E:\Repositories\NeuralWeb\NeuralNetTest\ApperSine.txt");
            //Console.ReadKey();

            #endregion sine staff

            #region stock staff

            var stock = new StockInstance();
            stock.CreateInstance();
            stock.SaveInstance(@"D:\PrivateRepos\NeuralWeb\NeuralNetTest\SavedNets\MSFT-2.txt");
            Console.ReadKey();

            #endregion stock staff
        }
    }
}

