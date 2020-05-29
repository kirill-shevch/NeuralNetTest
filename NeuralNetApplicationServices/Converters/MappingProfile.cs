using AutoMapper;
using NeuralNetApi;
using NeuralNetApi.DTO.Prices;
using NeuralNetDomainService.Constants;
using NeuralNetDomainService.DomainObjects;
using NeuralNetInfrastructure.Entities;

namespace NeuralNetApplicationServices.Converters
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NeuralNet, NeuralNetworkDomain>()
                .ForMember(dest => dest.Synapses, opt => opt.Ignore())
                .ForMember(dest => dest.Neurons, opt => opt.Ignore());

            CreateMap<NeuralNetworkDomain, NeuralNet>()
                .ForMember(dest => dest.Synapses, opt => opt.Ignore())
                .ForMember(dest => dest.Neurons, opt => opt.Ignore());

            CreateMap<NeuronDomain, Neuron>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.IdNeuron))
                .ForMember(dest => dest.NeuronType, opt => opt.MapFrom(c => (byte)c.NeuronType))
                .ForMember(dest => dest.InputSynapses, opt => opt.Ignore())
                .ForMember(dest => dest.OutputSynapses, opt => opt.Ignore());

            CreateMap<SynapseDomain, Synapse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.IdSynapse));

            CreateMap<Neuron, NeuronDomain>()
                .ForMember(dest => dest.IdNeuron, opt => opt.MapFrom(c => c.Id))
                .ForMember(dest => dest.NeuronType, opt => opt.MapFrom(c =>  (NeuronType)c.NeuronType))
                .ForMember(dest => dest.InputSynapses, opt => opt.Ignore())
                .ForMember(dest => dest.OutputSynapses, opt => opt.Ignore());

            CreateMap<SynapseDomain, Synapse>()
                .ForMember(dest => dest.NeuronIdInput, opt => opt.MapFrom(c => c.InputNeuron.IdNeuron))
                .ForMember(dest => dest.NeuronIdInput, opt => opt.MapFrom(c => c.OutputNeuron.IdNeuron))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.IdSynapse));

            CreateMap<NeuralNetDto, NeuralNet>()
                .ForMember(dest => dest.Synapses, opt => opt.Ignore())
                .ForMember(dest => dest.Neurons, opt => opt.Ignore());

            CreateMap<NeuralNet, NeuralNetDto>()
                .ForMember(dest => dest.Neurons, opt => opt.Ignore())
                .ForMember(dest => dest.Synapses, opt => opt.Ignore());

            CreateMap<NeuronDto, Neuron>()
                .ForMember(dest => dest.NeuralNet, opt => opt.Ignore())
                .ForMember(dest => dest.InputSynapses, opt => opt.Ignore())
                .ForMember(dest => dest.OutputSynapses, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<SynapseDto, Synapse>()
                .ForMember(dest => dest.NeuralNet, opt => opt.Ignore())
                .ForMember(dest => dest.InputNeuron, opt => opt.Ignore())
                .ForMember(dest => dest.OutputNeuron, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CalibrationResultDomain, CalibrationResult>().ReverseMap();

            CreateMap<Historical, Price>()
                .ForMember(dest => dest.PriceValue, opt => opt.MapFrom(c => c.Open));
        }
    }
}
