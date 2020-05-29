using Microsoft.AspNetCore.Mvc;
using NeuralNetApi;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IList<CalibrationResult>> Calibrate(int neuralNetId, IList<InputNeuronCalibrationDto> inputNeuronCalibrationDto)
        {
            return await _neuralNetworkApplicationService.Calibrate(neuralNetId, inputNeuronCalibrationDto);
        }

        [HttpPost("/reckon")]
        public async Task<IList<double>> Reckon(int neuralNetId, IList<InputNeuronReckonDto> inputNeuronReckonDto)
        {
            return await _neuralNetworkApplicationService.Reckon(neuralNetId, inputNeuronReckonDto);
        }

        // POST: api/NeuralNetwork
        [HttpPost]
        public async Task<int> Post([FromBody] NeuralNetDto neuralNetDto)
        {
            return await _neuralNetworkApplicationService.Create(neuralNetDto);
        }


        /// <summary>
        /// Get neural network
        /// </summary>
        /// <response code="200">If start success</response>
        /// <response code="400">If bad request</response>
        /// <response code="403">If access denied</response>
        /// <response code="500">If internal server error</response>
        [SwaggerOperation("Get")]
        [HttpGet("{id}", Name = "Get")]
        public async Task<NeuralNetDto> Get(int id)
        {
            return await _neuralNetworkApplicationService.Get(id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _neuralNetworkApplicationService.Delete(id);
        }
    }
}