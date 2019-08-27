using NeuralNetDomainService.DTO;
using System.Collections.Generic;

namespace NeuralNetApi.DTO
{
    public class CalibrationResponse
    {
        public CalibrationResult Result { get; set; }

        public IList<InputNeuronDto> InputNeuronDtos { get; set; }
    }
}
