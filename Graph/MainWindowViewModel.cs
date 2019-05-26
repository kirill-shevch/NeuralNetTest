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
            double x = 0, y = 0;
            var result = new List<Point>();

            while (x < 10)
            {
                y = (1 + Math.Cos(x)) / 2;
                result.Add(new Point(x, y));
                x += 0.1;
            }

            return result;
        }

        private IEnumerable<Point> GetGeneratedPoints()
        {
            var sine = new SineInstance();
            sine.LoadInstance(@"E:\Repositories\NeuralWeb\NeuralNetTest\Sine.txt");
            var inputs = sine.NeuralNetwork.Neurons.Where(n => n.Value.NeuronType == (byte)NeuronTypeConst.InputNeuronType).ToList();
            double error = 0;
            double x = 0, y1 = 0, y2 = 0, y3 = 0;
            var result = new List<Point>();

            y1 = (1 + Math.Cos(x)) / 2;
            result.Add(new Point(x, y1));
            x += 0.1;

            y2 = (1 + Math.Cos(x)) / 2;
            result.Add(new Point(x, y2));
            x += 0.1;


            while (x < 10)
            {
                y3 = sine.NeuralNetwork.Calculate(y1, inputs[0].Key, y2, inputs[1].Key, out error);
                result.Add(new Point(x, y3));
                x += 0.1;
                y1 = y2;
                y2 = y3;
            }
            return result;
        }
    }
}
