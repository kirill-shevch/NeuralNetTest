using NeuralNetApi;
using NeuralNetDomainService.DomainObjects;
using NeuralNetDomainService.Services;
using System;
using System.Linq;

namespace NeuralNetDomain.Services
{
    public class NeuralNetworkService : INeuralNetworkService
    {
        /// <summary>
        /// Считаем выходное значение проходом вперёд
        /// </summary>
        /// <param name="neuralNet"></param>
        private void Calculate(NeuralNetworkDomain neuralNet)
        {
            //Обсчитываем скрытый слой и слои вывода
            var neurons = neuralNet.Neurons
                .Where(n => n.NeuronType != NeuronType.InputNeuronType)
                .OrderBy(n => n.NeuronType);

            foreach (var neuron in neurons)
            {
                neuron.DataIn = 0;
                foreach (var synapse in neuron.InputSynapses)
                {
                    neuron.DataIn += synapse.InputNeuron.DataOut * synapse.Weight;
                }
                neuron.DataOut = Sigmoid(neuron.DataIn);
            }
        }

        /// <summary>
        /// Считаем дельта-отклонение, пересчитываем веса нейронов
        /// </summary>
        /// <param name="answer"></param>
        private double CalculateDeviation(NeuralNetworkDomain neuralNet, double answer)
        {
            //Считаем значение средней квадратичной ошибки
            var outNeuron = neuralNet.Neurons.Single(n => n.NeuronType == NeuronType.OutputNeuronType);
            neuralNet.MSEcounter++;
            neuralNet.ErrorMSE += Math.Pow((answer - outNeuron.DataOut), 2);

            //Считаем дельта-отклонение для слоя вывода
            outNeuron.DeltaDeviation = DeltaOutput(answer, outNeuron.DataOut);

            CalculateHiddenLayerDeltaDeviation(neuralNet, NeuronType.SecondLayerHiddenNeuronType);

            CalculateHiddenLayerDeltaDeviation(neuralNet, NeuronType.FirstLayerHiddenNeuronType);

            //Считаем изменение веса синапсов слоя ввода
            foreach (var neuron in neuralNet.Neurons.Where(n => n.NeuronType == NeuronType.InputNeuronType))
            {
                //градиент для градиентного спуска
                double grad = 0;

                foreach (var synapse in neuron.OutputSynapses)
                {
                    var targetDeviation = synapse.OutputNeuron.DeltaDeviation;
                    grad = targetDeviation * neuron.DataOut;
                    synapse.DeltaWeight = neuralNet.LearningSpeed * grad + neuralNet.Moment * synapse.DeltaWeight;
                    synapse.Weight += synapse.DeltaWeight;
                }
            }
            return neuralNet.ErrorMSE / neuralNet.MSEcounter;
        }

        /// <summary>
        /// Считаем дельта-отклонение для указанного слоя скрытых нейронов и изменяем веса синапсов
        /// </summary>
        /// <param name="hiddenLayerType"></param>
        private void CalculateHiddenLayerDeltaDeviation(NeuralNetworkDomain neuralNet, NeuronType hiddenLayerType)
        {
            foreach (var neuron in neuralNet.Neurons.Where(n => n.NeuronType == hiddenLayerType))
            {
                //сумма произведения всех исходящих весов и дельта-отклонения нейрона, с которым связан синапс
                double sum = 0;
                //градиент для градиентного спуска
                double grad = 0;

                foreach (var synapse in neuron.OutputSynapses)
                {
                    var targetDeviation = synapse.OutputNeuron.DeltaDeviation;
                    sum += targetDeviation * synapse.Weight;
                    grad = targetDeviation * neuron.DataOut;
                    synapse.DeltaWeight = neuralNet.LearningSpeed * grad + neuralNet.Moment * synapse.DeltaWeight;
                    synapse.Weight += synapse.DeltaWeight;
                }
                neuron.DeltaDeviation = SigmoidDiff(neuron.DataOut) * sum;
            }
        }

        /// <summary>
        /// Производная функции активации для метода обратного распространения ошибки (МОР)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double SigmoidDiff(double x)
        {
            return x * (1 - x);
        }

        /// <summary>
        /// Дельта отклонение для выходных нейронов
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="dataOut"></param>
        /// <returns></returns>
        private double DeltaOutput(double answer, double dataOut)
        {
            return (answer - dataOut) * SigmoidDiff(dataOut);
        }

        /// <summary>
        /// Функция активации: Сигмоид
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public double Reckon(NeuralNetworkDomain neuralNet)
        {
            Calculate(neuralNet);
            return neuralNet.Neurons.Single(n => n.NeuronType == NeuronType.OutputNeuronType).DataOut;
        }

        public CalibrationResult Calibrate(NeuralNetworkDomain neuralNet, double answer)
        {
            Calculate(neuralNet);
            var error = CalculateDeviation(neuralNet, answer);
            return new CalibrationResult
            {
                Error = error,
                Result = neuralNet.Neurons.Single(n => n.NeuronType == NeuronType.OutputNeuronType).DataOut
            };
        }
    }
}
