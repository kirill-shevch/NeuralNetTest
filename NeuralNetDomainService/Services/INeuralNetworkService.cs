using NeuralNetDomainService.DomainObjects;
using NeuralNetDomainService.DTO;

namespace NeuralNetDomainService.Services
{
    public interface INeuralNetworkService
    {
        double Reckon(NeuralWebDomain neuralNet);

        CalibrationResult Calibrate(NeuralWebDomain neuralNet, double answer);
    }
}
