using AutoMapper;
using NeuralNetApi;
using NeuralNetApi.Requests;
using NeuralNetDomain.Entities;
using NeuralNetDomainService.DomainObjects;
using NeuralNetDomainService.DTO;
using NeuralNetDomainService.Services;
using NeuralNetInfrastructure;
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

        public CalculationResult Calculate(int neuralWebId, double? answer = null)
        {
            var neuralWeb = _applicationContext.NeuralWebs.Find(neuralWebId);
            if (neuralWeb == null)
            {
                throw new Exception("Can't find neuralWeb");
            }
            var neuralWebDomain = _mapper.Map<NeuralWebDomain>(neuralWeb);
            var result = _neuralNetworkService.Calculate(neuralWebDomain, answer);
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