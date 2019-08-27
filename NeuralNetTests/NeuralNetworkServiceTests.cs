using NeuralNetDomain.Constants;
using NeuralNetDomain.Services;
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
            Assert.AreEqual(0.49, Math.Round(neuralNet.Synapses[1].Weight, 2));
            Assert.AreEqual(0.73, Math.Round(neuralNet.Synapses[2].Weight, 2));
            Assert.AreEqual(-0.12, Math.Round(neuralNet.Synapses[3].Weight, 2));
            Assert.AreEqual(0.13, Math.Round(neuralNet.Synapses[4].Weight, 2));
            Assert.AreEqual(1.56, Math.Round(neuralNet.Synapses[5].Weight, 2));
            Assert.AreEqual(-2.23, Math.Round(neuralNet.Synapses[6].Weight, 2));

            //Снова пересчитаем веса нейросети, увидим корректное изменение ошибки и улучшенный результат
            result = neuralNetworkService.Calibrate(neuralNet, 1);

            Assert.AreEqual(0.37, Math.Round(result.Result, 2));
            Assert.AreEqual(0.42, Math.Round(result.Error, 2));
        }

        private NeuralWebDomain GetNeuralNet()
        {
            return new NeuralWebDomain
            {
                Id = 1,
                LearningSpeed = 0.7,
                Moment = 0.3,
                MSEcounter = 0,
                ErrorMSE = 0,
                Neurons = new Dictionary<int, NeuronDomain>
                {
                    { 1 ,new NeuronDomain
                        {
                            IdNeuron = 1,
                            DataOut = 1,
                            NeuralWebId = 1,
                            NeuronType = (byte)NeuronTypeConst.InputNeuronType
                        }
                    },
                    { 2, new NeuronDomain
                        {
                            IdNeuron = 2,
                            DataOut = 0,
                            NeuralWebId = 1,
                            NeuronType = (byte)NeuronTypeConst.InputNeuronType
                        }
                    },
                    { 3, new NeuronDomain
                        {
                            IdNeuron = 3,
                            NeuralWebId = 1,
                            NeuronType = (byte)NeuronTypeConst.FirstLayerHiddenNeuronType
                        }
                    },
                    { 4, new NeuronDomain
                        {
                            IdNeuron = 4,
                            NeuralWebId = 1,
                            NeuronType = (byte)NeuronTypeConst.FirstLayerHiddenNeuronType
                        }
                    },
                    { 5, new NeuronDomain
                        {
                            IdNeuron = 5,
                            NeuralWebId = 1,
                            NeuronType = (byte)NeuronTypeConst.OutputNeuronType
                        }
                    },
                },
                Synapses = new Dictionary<int, SynapseDomain>
                {
                    { 1, new SynapseDomain
                        {
                            IdSynapse = 1,
                            NeuralWebId = 1,
                            IdInput = 1,
                            IdOutput = 3,
                            Weight = 0.45
                        }
                    },
                    { 2, new SynapseDomain
                        {
                            IdSynapse = 2,
                            NeuralWebId = 1,
                            IdInput = 1,
                            IdOutput = 4,
                            Weight = 0.78
                        }
                    },
                    { 3, new SynapseDomain
                        {
                            IdSynapse = 3,
                            NeuralWebId = 1,
                            IdInput = 2,
                            IdOutput = 3,
                            Weight = -0.12
                        }
                    },
                    { 4, new SynapseDomain
                        {
                            IdSynapse = 4,
                            NeuralWebId = 1,
                            IdInput = 2,
                            IdOutput = 4,
                            Weight = 0.13
                        }
                    },
                    { 5, new SynapseDomain
                        {
                            IdSynapse = 5,
                            NeuralWebId = 1,
                            IdInput = 3,
                            IdOutput = 5,
                            Weight = 1.5
                        }
                    },
                    { 6, new SynapseDomain
                        {
                            IdSynapse = 6,
                            NeuralWebId = 1,
                            IdInput = 4,
                            IdOutput = 5,
                            Weight = -2.3
                        }
                    },
                }

            };
        }
    }
}