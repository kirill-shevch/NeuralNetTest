using AutoMapper;
using NeuralNetApi;
using NeuralNetApi.DTO;
using NeuralNetApi.Requests;
using NeuralNetDomainService.DomainObjects;
using NeuralNetDomainService.DTO;
using NeuralNetDomainService.Services;
using NeuralNetInfrastructure;
using NeuralNetInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetApplicationServices
{
    public class NeuralNetworkApplicationService : INeuralNetworkApplicationService
    {
        private readonly INeuralNetworkService _neuralNetworkService;
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public NeuralNetworkApplicationService(INeuralNetworkService neuralNetworkService,
            ApplicationContext applicationContext,
            IMapper mapper)
        {
            _neuralNetworkService = neuralNetworkService;
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public IList<ReckonResponse> Reckon(int neuralWebId, IList<InputNeuronReckonDto> inputNeuronCalculationDto)
        {
            var neuralWeb = _applicationContext.NeuralWebs.Find(neuralWebId);
            if (neuralWeb == null)
            {
                throw new Exception("Can't find neuralWeb");
            }
            var neuralWebDomain = _mapper.Map<NeuralWebDomain>(neuralWeb);
            var result = new List<ReckonResponse>(); 
            foreach (var item in inputNeuronCalculationDto)
            {
                foreach (var neuronDto in item.InputNeuronDtos)
                {
                    neuralWebDomain.SetNeuronDataOut(neuronDto.Id, neuronDto.DataOut);
                }
                result.Add(new ReckonResponse
                {
                    InputNeuronDtos = item.InputNeuronDtos,
                    Result = _neuralNetworkService.Reckon(neuralWebDomain)
                });
            }
            neuralWeb = _mapper.Map<NeuralWeb>(neuralWebDomain);
            return result;
        }

        public IList<CalibrationResponse> Calibrate(int neuralWebId, IList<InputNeuronCalibrationDto> inputNeuronCalibrationDto)
        {
            var neuralWeb = _applicationContext.NeuralWebs.Find(neuralWebId);
            if (neuralWeb == null)
            {
                throw new Exception("Can't find neuralWeb");
            }
            var neuralWebDomain = _mapper.Map<NeuralWebDomain>(neuralWeb);
            var result = new List<CalibrationResponse>();
            foreach (var item in inputNeuronCalibrationDto)
            {
                foreach (var neuronDto in item.InputNeuronDtos)
                {
                    neuralWebDomain.SetNeuronDataOut(neuronDto.Id, neuronDto.DataOut);
                }
                result.Add(new CalibrationResponse
                {
                    InputNeuronDtos = item.InputNeuronDtos,
                    Result = _neuralNetworkService.Calibrate(neuralWebDomain, item.Answer)
                });
            }
            neuralWeb = _mapper.Map<NeuralWeb>(neuralWebDomain);
            return result;
        }

        public int CreateNeuralWeb(NeuralWebCreatingRequest request)
        {
            var neuralWeb = _mapper.Map<NeuralWeb>(request);
            _applicationContext.NeuralWebs.Add(neuralWeb);
            _applicationContext.SaveChanges();
            return neuralWeb.Id;
        }

        public void CreateNeurons(IList<NeuronCreatingRequest> request)
        {
            foreach (var neuron in request)
            {
                _applicationContext.Neurons.Add(_mapper.Map<Neuron>(request)); 
            }
            _applicationContext.SaveChanges();
        }

        public void CreateSynapses(IList<SynapseCreatingRequest> request)
        {
            foreach (var synapse in request)
            {
                _applicationContext.Synapses.Add(_mapper.Map<Synapse>(request));
            }
            _applicationContext.SaveChanges();
        }
    }
}