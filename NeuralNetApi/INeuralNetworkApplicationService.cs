using System.Collections.Generic;

namespace NeuralNetApi
{
    public interface INeuralNetworkApplicationService
    {
        IList<CalibrationResult> Calibrate(int neuralNetId, IList<InputNeuronCalibrationDto> inputNeuronCalibrationDto);

        IList<double> Reckon(int neuralNetId, IList<InputNeuronReckonDto> inputNeuronReckonDto);
    }
}