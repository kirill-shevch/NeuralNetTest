using System.Collections.Generic;

namespace NeuralNetApi
{
    public class ReckonResponse
    {
        public double Result { get; set; }

        public IList<InputNeuronDto> InputNeuronDtos { get; set; }
    }
}