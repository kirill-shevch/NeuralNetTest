using System.Collections.Generic;

namespace NeuralNetApi
{
    public class CalibrationResponse
    {
        public CalibrationResult Result { get; set; }

        public IList<InputNeuronDto> InputNeuronDtos { get; set; }
    }
}
