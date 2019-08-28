using NeuralNetApi;
using NeuralNetDomainService.DomainObjects;

namespace NeuralNetDomainService.Services
{
    public interface INeuralNetworkService
    {
        double Reckon(NeuralNetworkDomain neuralNet);

        CalibrationResult Calibrate(NeuralNetworkDomain neuralNet, double answer);
    }
}
