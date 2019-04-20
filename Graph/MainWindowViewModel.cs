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
            double x = 0, y = 1;
            var result = new List<Point>();

            while (x < 30)
            {
                result.Add(new Point(x, y));
                x += 0.01;
                y = (1 + Math.Cos(x)) / 2;
            }

            return result;
        }

        private IEnumerable<Point> GetGeneratedPoints()
        {
            var sine = new SineInstance();
            sine.LoadInstance(@"E:\Repositories\NeuralWeb\NeuralNetTest\BadSine.txt");
            var inputs = sine.NeuralNetwork.Neurons.Where(n => n.Value.NeuronType == (byte)NeuronTypeConst.InputNeuronType).ToList();

            double x = 0, y1 = 0, y2 = 0;
            var result = new List<Point>();

            x += 0.01;
            y1 = (1 + Math.Cos(x)) / 2;

            while (x < 30)
            {
                x += 0.01;
                y2 = (1 + Math.Cos(x)) / 2;
                result.Add(new Point(x, sine.NeuralNetwork.Calculate(y1, inputs[0].Key, y2, inputs[1].Key)));
                y1 = y2;
            }
            return result;
        }
    }
}
