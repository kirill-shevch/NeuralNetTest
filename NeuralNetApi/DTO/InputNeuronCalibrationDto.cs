using System.Collections.Generic;

namespace NeuralNetApi
{
    public class InputNeuronCalibrationDto
    {
        public IList<double> Inputs { get; set; }

        public double Answer { get; set; }
    }
}