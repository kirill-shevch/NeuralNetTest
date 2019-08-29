using AutoMapper;
using NeuralNetApi;
using NeuralNetDomainService.DomainObjects;
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

        public IList<CalibrationResult> Calibrate(int neuralNetId, IList<InputNeuronCalibrationDto> inputNeuronCalibrationDto)
        {
            var neuralNet = _applicationContext.NeuralNets.Find(neuralNetId);
            var neuralNetDomain = ConvertNeuralNetFromEntity(neuralNet);
            var result = new List<CalibrationResult>();

            foreach (var item in inputNeuronCalibrationDto)
            {
                neuralNetDomain.SetInputData(item.Inputs);
                var calibrationResult = _neuralNetworkService.Calibrate(neuralNetDomain, item.Answer);
                result.Add(_mapper.Map<CalibrationResult>(calibrationResult));
            }
            neuralNet = _mapper.Map<NeuralNet>(neuralNetDomain);
            _applicationContext.NeuralNets.Update(neuralNet);
            _applicationContext.SaveChanges();
            return result;
        }

        public IList<double> Reckon(int neuralNetId, IList<InputNeuronReckonDto> inputNeuronReckonDto)
        {
            var neuralNet = _applicationContext.NeuralNets.Find(neuralNetId);
            var neuralNetDomain = ConvertNeuralNetFromEntity(neuralNet);
            var result = new List<double>();

            foreach (var item in inputNeuronReckonDto)
            {
                neuralNetDomain.SetInputData(item.Inputs);
                result.Add(_neuralNetworkService.Reckon(neuralNetDomain));
            }
            return result;
        }

        private NeuralNetworkDomain ConvertNeuralNetFromEntity(NeuralNet neuralNet)
        {
            var neuralNetDomain = _mapper.Map<NeuralNetworkDomain>(neuralNet);

            foreach (var synapse in neuralNet.Synapses)
            {
                var inputNeuron = neuralNetDomain.Neurons.Single(n => n.IdNeuron == synapse.NeuronIdInput);
                var outputNeuron = neuralNetDomain.Neurons.Single(n => n.IdNeuron == synapse.NeuronIdOutput);
                var domainSynapse = new SynapseDomain(inputNeuron, outputNeuron, synapse.Weight);
                neuralNetDomain.Synapses.Add(domainSynapse);
            }
            return neuralNetDomain;
        }
    }
}