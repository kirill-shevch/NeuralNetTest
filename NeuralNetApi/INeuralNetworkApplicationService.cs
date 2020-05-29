using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeuralNetApi
{
    public interface INeuralNetworkApplicationService
    {
        Task<IList<CalibrationResult>> Calibrate(int neuralNetId, IList<InputNeuronCalibrationDto> inputNeuronCalibrationDto);

        Task<IList<double>> Reckon(int neuralNetId, IList<InputNeuronReckonDto> inputNeuronReckonDto);

        Task<int> Create(NeuralNetDto neuralNetDto);

        Task Delete(int neuralNetId);

        Task<NeuralNetDto> Get(int neuralNetId);
    }
}