using Microsoft.AspNetCore.Mvc;
using NeuralNetApi;
using System.Collections.Generic;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeuralNetworkController : ControllerBase, INeuralNetworkApplicationService
    {
        private readonly INeuralNetworkApplicationService _neuralNetworkApplicationService;
        public NeuralNetworkController(INeuralNetworkApplicationService neuralNetworkApplicationService)
        {
            _neuralNetworkApplicationService = neuralNetworkApplicationService;
        }

        public IList<CalibrationResponse> Calibrate(int neuralNetId, IList<InputNeuronCalibrationDto> inputNeuronCalibrationDto)
        {
            return _neuralNetworkApplicationService.Calibrate(neuralNetId, inputNeuronCalibrationDto);
        }

        public int CreateNeuralNet(NeuralNetCreatingRequest request)
        {
            return _neuralNetworkApplicationService.CreateNeuralNet(request);
        }

        public void CreateNeurons(IList<NeuronCreatingRequest> request)
        {
            _neuralNetworkApplicationService.CreateNeurons(request);
        }

        public void CreateSynapses(IList<SynapseCreatingRequest> request)
        {
            _neuralNetworkApplicationService.CreateSynapses(request);
        }

        public NeuralNetDto GetNeuralNetInformation(int neuralNetId)
        {
            return _neuralNetworkApplicationService.GetNeuralNetInformation(neuralNetId);
        }

        public IList<ReckonResponse> Reckon(int neuralNetId, IList<InputNeuronReckonDto> inputNeuronReckonDto)
        {
            return _neuralNetworkApplicationService.Reckon(neuralNetId, inputNeuronReckonDto);
        }
    }
}