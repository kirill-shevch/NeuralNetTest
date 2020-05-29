using NeuralNetDomain.Services;
using NeuralNetDomainService.Constants;
using NeuralNetDomainService.DomainObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NeuralNetTests
{
    public class NeuralNetworkServiceTests
    {
        private NeuralNetworkService neuralNetworkService;

        [SetUp]
        public void Setup()
        {
            neuralNetworkService = new NeuralNetworkService();
        }

        [Test]
        public void ReckonTests()
        {
            var neuralNet = GetNeuralNet();

            var result = neuralNetworkService.Reckon(neuralNet);
            Assert.AreEqual(0.34, Math.Round(result, 2));
        }

        [Test]
        public void CalibrationTests()
        {
            var neuralNet = GetNeuralNet();

            //Пересчитаем веса нейросети, изменим веса синапсов, увидим ошибку
            var result = neuralNetworkService.Calibrate(neuralNet, 1);
            Assert.AreEqual(0.43, Math.Round(result.Error, 2));
            Assert.AreEqual(0.34, Math.Round(result.Result, 2));
            Assert.AreEqual(0.49, Math.Round(neuralNet.Synapses[0].Weight, 2));
            Assert.AreEqual(0.73, Math.Round(neuralNet.Synapses[1].Weight, 2));
            Assert.AreEqual(-0.12, Math.Round(neuralNet.Synapses[2].Weight, 2));
            Assert.AreEqual(0.13, Math.Round(neuralNet.Synapses[3].Weight, 2));
            Assert.AreEqual(1.56, Math.Round(neuralNet.Synapses[4].Weight, 2));
            Assert.AreEqual(-2.23, Math.Round(neuralNet.Synapses[5].Weight, 2));

            //Снова пересчитаем веса нейросети, увидим корректное изменение ошибки и улучшенный результат
            result = neuralNetworkService.Calibrate(neuralNet, 1);

            Assert.AreEqual(0.37, Math.Round(result.Result, 2));
            Assert.AreEqual(0.42, Math.Round(result.Error, 2));
        }

        //TODO use test fixture
        private NeuralNetworkDomain GetNeuralNet()
        {
            var neuron1 = new NeuronDomain
            {
                IdNeuron = 1,
                DataOut = 1,
                NeuralNetId = 1,
                NeuronType = NeuronType.InputNeuronType
            };
            var neuron2 = new NeuronDomain
            {
                IdNeuron = 2,
                DataOut = 0,
                NeuralNetId = 1,
                NeuronType = NeuronType.InputNeuronType
            };
            var neuron3 = new NeuronDomain
            {
                IdNeuron = 3,
                NeuralNetId = 1,
                NeuronType = NeuronType.FirstLayerHiddenNeuronType
            };
            var neuron4 = new NeuronDomain
            {
                IdNeuron = 4,
                NeuralNetId = 1,
                NeuronType = NeuronType.FirstLayerHiddenNeuronType
            };
            var neuron5 = new NeuronDomain
            {
                IdNeuron = 5,
                NeuralNetId = 1,
                NeuronType = NeuronType.OutputNeuronType
            };

            var synapse1 = new SynapseDomain(neuron1, neuron3, 0.45)
            {
                IdSynapse = 1,
                NeuralNetId = 1,
            };
            var synapse2 = new SynapseDomain(neuron1, neuron4, 0.78)
            {
                IdSynapse = 2,
                NeuralNetId = 1,
            };
            var synapse3 = new SynapseDomain(neuron2, neuron3, -0.12)
            {
                IdSynapse = 3,
                NeuralNetId = 1,
            };
            var synapse4 = new SynapseDomain(neuron2, neuron4, 0.13)
            {
                IdSynapse = 4,
                NeuralNetId = 1,
            };
            var synapse5 = new SynapseDomain(neuron3, neuron5, 1.5)
            {
                IdSynapse = 5,
                NeuralNetId = 1,
            };
            var synapse6 = new SynapseDomain(neuron4, neuron5, -2.3)
            {
                IdSynapse = 6,
                NeuralNetId = 1,
            };
            return new NeuralNetworkDomain
            {
                Id = 1,
                LearningSpeed = 0.7,
                Moment = 0.3,
                MSEcounter = 0,
                ErrorMSE = 0,
                Neurons = new List<NeuronDomain>
                {
                    neuron1,
                    neuron2,
                    neuron3,
                    neuron4,
                    neuron5,
                },
                Synapses = new List<SynapseDomain>
                {
                    synapse1,
                    synapse2,
                    synapse3,
                    synapse4,
                    synapse5,
                    synapse6
                }
            };
        }
    }
}