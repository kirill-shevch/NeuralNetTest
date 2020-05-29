using AutoMapper;
using NeuralNetApi;
using NeuralNetDomainService.DomainObjects;
using NeuralNetDomainService.Services;
using NeuralNetInfrastructure;
using NeuralNetInfrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeuralNetApplicationServices
{
    public class NeuralNetworkApplicationService : INeuralNetworkApplicationService
    {
        private readonly INeuralNetworkService _neuralNetworkService;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public NeuralNetworkApplicationService(INeuralNetworkService neuralNetworkService,
            IRepository repository,
            IMapper mapper)
        {
            _neuralNetworkService = neuralNetworkService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<CalibrationResult>> Calibrate(int neuralNetId, IList<InputNeuronCalibrationDto> inputNeuronCalibrationDto)
        {
            var neuralNet = await _repository.GetNeuralNet(neuralNetId);
            var neuralNetDomain = ConvertNeuralNetFromEntity(neuralNet);
            var result = new List<CalibrationResult>();

            foreach (var item in inputNeuronCalibrationDto)
            {
                neuralNetDomain.SetInputData(item.Inputs);
                var calibrationResult = _neuralNetworkService.Calibrate(neuralNetDomain, item.Answer);
                result.Add(_mapper.Map<CalibrationResult>(calibrationResult));
            }
            neuralNet = _mapper.Map<NeuralNet>(neuralNetDomain);
            neuralNet.Neurons = _mapper.Map<List<Neuron>>(neuralNetDomain.Neurons);
            neuralNet.Synapses = _mapper.Map<List<Synapse>>(neuralNetDomain.Synapses);

            await _repository.UpdateNeuralNet(neuralNet);
            return result;
        }

        public async Task<int> Create(NeuralNetDto neuralNetDto)
        {
            var neuralNet = ConvertNeuralNetDtoToEntity(neuralNetDto);
            await _repository.AddNeuralNet(neuralNet);
            return neuralNet.Id;
        }

        public async Task Delete(int neuralNetId)
        {
            var neuralNet = await _repository.GetNeuralNet(neuralNetId);
            await _repository.RemoveNeuralNet(neuralNet);
        }

        public async Task<NeuralNetDto> Get(int neuralNetId)
        {
            var neuralNet = await _repository.GetNeuralNet(neuralNetId);
            var neuralNetDto = _mapper.Map<NeuralNetDto>(neuralNet);
            neuralNetDto.Neurons = _mapper.Map<List<NeuronDto>>(neuralNet.Neurons);
            neuralNetDto.Synapses = _mapper.Map<List<SynapseDto>>(neuralNet.Synapses);
            return neuralNetDto;
        }

        public async Task<IList<double>> Reckon(int neuralNetId, IList<InputNeuronReckonDto> inputNeuronReckonDto)
        {
            var neuralNet = await _repository.GetNeuralNet(neuralNetId);
            var neuralNetDomain = ConvertNeuralNetFromEntity(neuralNet);
            var result = new List<double>();

            foreach (var item in inputNeuronReckonDto)
            {
                neuralNetDomain.SetInputData(item.Inputs);
                result.Add(_neuralNetworkService.Reckon(neuralNetDomain));
            }
            return result;
        }

        private NeuralNet ConvertNeuralNetDtoToEntity(NeuralNetDto neuralNetDto)
        {
            var neuralNet = _mapper.Map<NeuralNet>(neuralNetDto);
            neuralNet.Neurons = _mapper.Map<List<Neuron>>(neuralNetDto.Neurons);
            neuralNet.Synapses = _mapper.Map<List<Synapse>>(neuralNetDto.Synapses);
            foreach (var neuron in neuralNet.Neurons)
            {
                neuron.NeuralNet = neuralNet;
                neuron.InputSynapses = neuralNet.Synapses.Where(s => s.NeuronIdOutput == neuron.Id).ToList();
                foreach (var synapse in neuron.InputSynapses)
                {
                    synapse.OutputNeuron = neuron;
                }
                neuron.OutputSynapses = neuralNet.Synapses.Where(s => s.NeuronIdInput == neuron.Id).ToList();
                foreach (var synapse in neuron.OutputSynapses)
                {
                    synapse.InputNeuron = neuron;
                }
            }
            return neuralNet;
        }

        private NeuralNetworkDomain ConvertNeuralNetFromEntity(NeuralNet neuralNet)
        {
            var neuralNetDomain = _mapper.Map<NeuralNetworkDomain>(neuralNet);
            neuralNetDomain.Neurons = _mapper.Map<List<NeuronDomain>>(neuralNet.Neurons);
            neuralNetDomain.Synapses = new List<SynapseDomain>();

            foreach (var synapse in neuralNet.Synapses)
            {
                var inputNeuron = neuralNetDomain.Neurons.Single(n => n.IdNeuron == synapse.NeuronIdInput);
                var outputNeuron = neuralNetDomain.Neurons.Single(n => n.IdNeuron == synapse.NeuronIdOutput);
                var domainSynapse = new SynapseDomain(inputNeuron, outputNeuron, synapse.Weight);
                domainSynapse.IdSynapse = synapse.Id;
                neuralNetDomain.Synapses.Add(domainSynapse);
            }
            return neuralNetDomain;
        }
    }
}