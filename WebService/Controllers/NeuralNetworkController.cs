using Microsoft.AspNetCore.Mvc;
using NeuralNetApi;
using System.Collections.Generic;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeuralNetworkController : ControllerBase
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
        public int Post([FromBody] NeuralNetDto neuralNetDto)
        {
            return _neuralNetworkApplicationService.Create(neuralNetDto);
        }

        [HttpGet("{id}", Name = "Get")]
        public NeuralNetDto Get(int id)
        {
            return _neuralNetworkApplicationService.Get(id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _neuralNetworkApplicationService.Delete(id);
        }
    }
}