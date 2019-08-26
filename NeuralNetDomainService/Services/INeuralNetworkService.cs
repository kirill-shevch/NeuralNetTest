using NeuralNetDomainService.DomainObjects;
using NeuralNetDomainService.DTO;

namespace NeuralNetDomainService.Services
{
    public interface INeuralNetworkService
    {
        CalculationResult Calculate(NeuralWebDomain neuralNet, double? answer = null);
    }
}
