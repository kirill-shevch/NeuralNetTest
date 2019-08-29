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

        [HttpPost("/calibrate")]
        public IList<CalibrationResult> Calibrate(int neuralNetId, IList<InputNeuronCalibrationDto> inputNeuronCalibrationDto)
        {
            return _neuralNetworkApplicationService.Calibrate(neuralNetId, inputNeuronCalibrationDto);
        }

        [HttpPost("/reckon")]
        public IList<double> Reckon(int neuralNetId, IList<InputNeuronReckonDto> inputNeuronReckonDto)
        {
            return _neuralNetworkApplicationService.Reckon(neuralNetId, inputNeuronReckonDto);
        }

        // POST: api/NeuralNetwork
        [HttpPost]
        public void Post([FromBody] NeuralNetDto value)
        {
        }

        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}