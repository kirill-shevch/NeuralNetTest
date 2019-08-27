using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NeuralNetApi;
using NeuralNetApi.DTO;
using NeuralNetApi.Requests;
using NeuralNetDomainService.DTO;

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

        public IList<CalibrationResponse> Calibrate(int neuralWebId, IList<InputNeuronCalibrationDto> inputNeuronCalibrationDto)
        {
            return _neuralNetworkApplicationService.Calibrate(neuralWebId, inputNeuronCalibrationDto);
        }

        public int CreateNeuralWeb(NeuralWebCreatingRequest request)
        {
            return _neuralNetworkApplicationService.CreateNeuralWeb(request);
        }

        public void CreateNeurons(IList<NeuronCreatingRequest> request)
        {
            _neuralNetworkApplicationService.CreateNeurons(request);
        }

        public void CreateSynapses(IList<SynapseCreatingRequest> request)
        {
            _neuralNetworkApplicationService.CreateSynapses(request);
        }

        public NeuralWebDto GetNeuralWebInformation(int neuralWebId)
        {
            return _neuralNetworkApplicationService.GetNeuralWebInformation(neuralWebId);
        }

        public IList<ReckonResponse> Reckon(int neuralWebId, IList<InputNeuronReckonDto> inputNeuronReckonDto)
        {
            return _neuralNetworkApplicationService.Reckon(neuralWebId, inputNeuronReckonDto);
        }
    }
}