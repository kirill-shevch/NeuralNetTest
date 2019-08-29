using NeuralNetDomainService.DomainObjects;

namespace NeuralNetDomainService.Services
{
    public interface INeuralNetworkService
    {
        double Reckon(NeuralNetworkDomain neuralNet);

        CalibrationResultDomain Calibrate(NeuralNetworkDomain neuralNet, double answer);
    }
}
