using NeuralNetApi.DTO;
using System.Collections.Generic;

namespace NeuralNetDomainService.DTO
{
    public class ReckonResponse
    {
        public double Result { get; set; }

        public IList<InputNeuronDto> InputNeuronDtos { get; set; }
    }
}