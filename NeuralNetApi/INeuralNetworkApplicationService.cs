using NeuralNetApi.DTO;
using NeuralNetApi.Requests;
using NeuralNetDomainService.DTO;
using System.Collections.Generic;

namespace NeuralNetApi
{
    public interface INeuralNetworkApplicationService
    {
        IList<CalibrationResponse> Calibrate(int neuralWebId, IList<InputNeuronCalibrationDto> inputNeuronCalibrationDto);

        IList<ReckonResponse> Reckon(int neuralWebId, IList<InputNeuronReckonDto> inputNeuronReckonDto);

        NeuralWebDto GetNeuralWebInformation(int neuralWebId);

        int CreateNeuralWeb(NeuralWebCreatingRequest request);

        void CreateNeurons(IList<NeuronCreatingRequest> request);

        void CreateSynapses(IList<SynapseCreatingRequest> request);
    }
}