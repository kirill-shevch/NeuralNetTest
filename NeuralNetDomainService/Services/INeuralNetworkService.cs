using NeuralNetApi;
using NeuralNetDomainService.DomainObjects;

namespace NeuralNetDomainService.Services
{
    public interface INeuralNetworkService
    {
        double Reckon(NeuralNetDomain neuralNet);

        CalibrationResult Calibrate(NeuralNetDomain neuralNet, double answer);
    }
}
