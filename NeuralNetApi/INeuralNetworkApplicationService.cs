using System.Collections.Generic;

namespace NeuralNetApi
{
    public interface INeuralNetworkApplicationService
    {
        IList<CalibrationResponse> Calibrate(int neuralNetId, IList<InputNeuronCalibrationDto> inputNeuronCalibrationDto);

        IList<ReckonResponse> Reckon(int neuralNetId, IList<InputNeuronReckonDto> inputNeuronReckonDto);

        NeuralNetDto GetNeuralNetInformation(int neuralNetId);

        int CreateNeuralNet(NeuralNetCreatingRequest request);

        void CreateNeurons(IList<NeuronCreatingRequest> request);

        void CreateSynapses(IList<SynapseCreatingRequest> request);
    }
}