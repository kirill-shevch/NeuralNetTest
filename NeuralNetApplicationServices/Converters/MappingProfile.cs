using AutoMapper;
using NeuralNetDomainService.DomainObjects;
using NeuralNetInfrastructure.Entities;

namespace NeuralNetApplicationServices.Converters
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NeuralNet, NeuralNetworkDomain>()
                .ForMember(dest => dest.Synapses, opt => opt.Ignore());

            CreateMap<Neuron, NeuronDomain>()
                .ForMember(dest => dest.IdNeuron, opt => opt.MapFrom(c => c.Id))
                .ReverseMap();

            CreateMap<SynapseDomain, Synapse>()
                .ForMember(dest => dest.NeuronIdInput, opt => opt.MapFrom(c => c.InputNeuron.IdNeuron))
                .ForMember(dest => dest.NeuronIdInput, opt => opt.MapFrom(c => c.OutputNeuron.IdNeuron))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(c => c.IdSynapse));
        }
    }
}
