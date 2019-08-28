using NeuralNetApi.Requests;
using NeuralNetDomainService.DTO;
using System.Collections.Generic;

namespace NeuralNetApi
{
    public interface INeuralNetworkApplicationService
    {
        CalculationResult Calculate(int neuralWebId, double? answer = null);

        int CreateNeuralWeb(NeuralWebCreatingRequest request);

        void CreateNeurons(IList<NeuronCreatingRequest> request);

        void CreateSynapses(IList<SynapseCreatingRequest> request);
    }
}