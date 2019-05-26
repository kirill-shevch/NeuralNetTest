using NeuralNetTest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Graph
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
        }

        public IEnumerable<Point> MainFunctionPoints
        {
            get
            {
                return GetMainFunctionPoints();
            }
        }

        public IEnumerable<Point> GeneratedPoints
        {
            get
            {
                return GetGeneratedPoints();
            }
        }

        private List<Point> GetMainFunctionPoints()
        {
            var result = new List<Point>
            {
                new Point { X = 0, Y = 123.870003 },
                new Point { X = 1, Y = 124.260002 },
                new Point { X = 2, Y = 126.75 },
                new Point { X = 3, Y = 128.309998 },
                new Point { X = 4, Y = 126.519997 },
                new Point { X = 5, Y = 127.43 },
                new Point { X = 6, Y = 126.620003 },
                new Point { X = 7, Y = 126.199997 },
                new Point { X = 8, Y = 126.910004 },
            };


            //var result = new List<Point>
            //{
            //    new Point { X = 2, Y = 42.220001 },
            //    new Point { X = 3, Y = 42.330002 },
            //    new Point { X = 4, Y = 42.509998 },
            //};






            return result;
        }

        private IEnumerable<Point> GetGeneratedPoints()
        {
            #region sine
            //var sine = new SineInstance();
            //sine.LoadInstance(@"E:\Repositories\NeuralWeb\NeuralNetTest\Sine.txt");
            //var inputs = sine.NeuralNetwork.Neurons.Where(n => n.Value.NeuronType == (byte)NeuronTypeConst.InputNeuronType).ToList();
            //double error = 0;
            //double x = 0, y1 = 0, y2 = 0, y3 = 0;
            //var result = new List<Point>();

            //y1 = (1 + Math.Cos(x)) / 2;
            //result.Add(new Point(x, y1));
            //x += 0.1;

            //y2 = (1 + Math.Cos(x)) / 2;
            //result.Add(new Point(x, y2));
            //x += 0.1;


            //while (x < 10)
            //{
            //    y3 = sine.NeuralNetwork.Calculate(y1, inputs[0].Key, y2, inputs[1].Key, out error);
            //    result.Add(new Point(x, y3));
            //    x += 0.1;
            //    y1 = y2;
            //    y2 = y3;
            //}
            //return result;
            #endregion sine
            
            #region stock
            var stock = new SineInstance();
            stock.LoadInstance(@"D:\PrivateRepos\NeuralWeb\NeuralNetTest\SavedNets\MSFT-2.txt");
            var inputs = stock.NeuralNetwork.Neurons.Where(n => n.Value.NeuronType == (byte)NeuronTypeConst.InputNeuronType).ToList();
            double error = 0;
            var cords = new List<double>
            {
                124.910004,
                124.110001,
                123.870003,
                124.260002,
                126.75,
                128.309998,
                126.519997,
                127.43,
                126.620003,
                126.199997,
                126.910004
            };
            //var cords = new List<double>
            //{
            //    41.369999,
            //    41.700001,
            //    42.220001,
            //    42.330002,
            //    42.509998,
            //};
            var result = new List<Point>();

            for (int i = 2; i < cords.Count; i++)
            {
                var nextCord = stock.NeuralNetwork.Calculate(cords[i - 2] / 1000, inputs[0].Key, cords[i - 1] / 1000, inputs[1].Key, out error, cords[i] / 1000);
                result.Add(new Point(i - 2, nextCord * 1000));
            }
            return result;
            #endregion stock
        }
    }
}
