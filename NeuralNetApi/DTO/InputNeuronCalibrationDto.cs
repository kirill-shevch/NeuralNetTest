using System.Collections.Generic;

namespace NeuralNetApi
{
    public class InputNeuronCalibrationDto
    {
        public IList<InputNeuronDto> InputNeuronDtos { get; set; }

        public double Answer { get; set; }
    }
}