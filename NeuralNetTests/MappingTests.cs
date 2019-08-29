using AutoMapper;
using NeuralNetApplicationServices.Converters;
using NeuralNetDomainService.Constants;
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
        public void NeuralNetConvert_FromEntity_ToDomainObject()
        {
            var neuralnet = new NeuralNet
            {
                Id = 1,
                Neurons = new List<Neuron>
                {
                    new Neuron
                    {
                        Id = 1,
                        NeuronType = (byte)NeuronType.InputNeuronType,
                        NeuralNetId = 1
                    },
                    new Neuron
                    {
                        Id = 2,
                        NeuronType = (byte)NeuronType.InputNeuronType,
                        NeuralNetId = 1
                    },

                },
                Synapses = new List<Synapse>
                {
                    new Synapse
                    {
                        Id = 1,
                        NeuralNetId = 1,
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

            var neuralNetDomain = _mapper.Map<NeuralNetworkDomain>(neuralnet);
            Assert.AreEqual(neuralnet.LearningSpeed, neuralNetDomain.LearningSpeed);
            Assert.AreEqual(neuralnet.Id, neuralNetDomain.Id);
            Assert.AreEqual(neuralnet.Moment, neuralNetDomain.Moment);
            Assert.AreEqual(neuralnet.MSEcounter, neuralNetDomain.MSEcounter);
            Assert.AreEqual(neuralNetDomain.Neurons.Count, 2);
        }
    }
}