using AutoMapper;
using NeuralNetApplicationServices.Converters;
using NeuralNetDomain.Constants;
using NeuralNetDomainService.DomainObjects;
using NeuralNetInfrastructure.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class MappingTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            _mapper = mappingConfig.CreateMapper();
        }

        [Test]
        public void NeuralWebConvert_FromEntity_ToDomainObject()
        {
            var neuralnet = new NeuralWeb
            {
                Id = 1,
                Neurons = new List<Neuron>
                {
                    new Neuron
                    {
                        Id = 1,
                        NeuronType = (byte)NeuronTypeConst.InputNeuronType,
                        NeuralWebId = 1
                    }
                },
                Synapses = new List<Synapse>
                {
                    new Synapse
                    {
                        Id = 1,
                        NeuralWebId = 1,
                        NeuronIdInput = 1,
                        NeuronIdOutput = 2,
                        Weight = 1
                    }
                },
                LearningSpeed = 0.1,
                Moment = 0.1,
                ErrorMSE = 1,
                MSEcounter = 1
            };
            var neuralWebDomain = _mapper.Map<NeuralWebDomain>(neuralnet);
            Assert.AreEqual(neuralnet.LearningSpeed, neuralWebDomain.LearningSpeed);
            Assert.AreEqual(neuralnet.Id, neuralWebDomain.Id);
            Assert.AreEqual(neuralnet.Moment, neuralWebDomain.Moment);
            Assert.AreEqual(neuralnet.MSEcounter, neuralWebDomain.MSEcounter);

            Assert.IsTrue(neuralWebDomain.Neurons is IDictionary<int, NeuronDomain>);
            Assert.IsNotNull(neuralWebDomain.Neurons[1]);
            Assert.IsTrue(neuralWebDomain.Synapses is IDictionary<int, SynapseDomain>);
            Assert.IsNotNull(neuralWebDomain.Synapses[1]);
        }
    }
}