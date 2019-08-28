using AutoMapper;
using NeuralNetApi;
using NeuralNetDomainService.DomainObjects;
using NeuralNetDomainService.Services;
using NeuralNetInfrastructure;
using NeuralNetInfrastructure.Entities;
using System;
using System.Collections.Generic;

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

        public IList<ReckonResponse> Reckon(int neuralNetId, IList<InputNeuronReckonDto> inputNeuronCalculationDto)
        {
            var neuralNet = _applicationContext.NeuralNets.Find(neuralNetId);
            if (neuralNet == null)
            {
                throw new Exception("Can't find neuralNet");
            }
            var neuralNetDomain = _mapper.Map<NeuralNetDomain>(neuralNet);
            var result = new List<ReckonResponse>(); 
            foreach (var item in inputNeuronCalculationDto)
            {
                foreach (var neuronDto in item.InputNeuronDtos)
                {
                    neuralNetDomain.SetNeuronDataOut(neuronDto.Id, neuronDto.DataOut);
                }
                result.Add(new ReckonResponse
                {
                    InputNeuronDtos = item.InputNeuronDtos,
                    Result = _neuralNetworkService.Reckon(neuralNetDomain)
                });
            }
            neuralNet = _mapper.Map<NeuralNet>(neuralNetDomain);
            return result;
        }

        public IList<CalibrationResponse> Calibrate(int neuralNetId, IList<InputNeuronCalibrationDto> inputNeuronCalibrationDto)
        {
            var neuralNet = _applicationContext.NeuralNets.Find(neuralNetId);
            if (neuralNet == null)
            {
                throw new Exception("Can't find neuralNet");
            }
            var neuralNetDomain = _mapper.Map<NeuralNetDomain>(neuralNet);
            var result = new List<CalibrationResponse>();
            foreach (var item in inputNeuronCalibrationDto)
            {
                foreach (var neuronDto in item.InputNeuronDtos)
                {
                    neuralNetDomain.SetNeuronDataOut(neuronDto.Id, neuronDto.DataOut);
                }
                result.Add(new CalibrationResponse
                {
                    InputNeuronDtos = item.InputNeuronDtos,
                    Result = _neuralNetworkService.Calibrate(neuralNetDomain, item.Answer)
                });
            }
            neuralNet = _mapper.Map<NeuralNet>(neuralNetDomain);
            return result;
        }

        public int CreateNeuralNet(NeuralNetCreatingRequest request)
        {
            var neuralNet = _mapper.Map<NeuralNet>(request);
            _applicationContext.NeuralNets.Add(neuralNet);
            _applicationContext.SaveChanges();
            return neuralNet.Id;
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

        public NeuralNetDto GetNeuralNetInformation(int neuralNetId)
        {
            var neuralNet = _applicationContext.NeuralNets.Find(neuralNetId);
            if (neuralNet == null)
            {
                throw new Exception("Can't find neuralWeb");
            }
            return _mapper.Map<NeuralNetDto>(neuralNet);
        }
    }
}