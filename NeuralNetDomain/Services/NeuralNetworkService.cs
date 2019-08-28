using NeuralNetDomain.Constants;
using NeuralNetDomainService.DomainObjects;
using NeuralNetDomainService.DTO;
using NeuralNetDomainService.Services;
using System;
using System.Linq;

namespace NeuralNetDomain
{
    public class NeuralNetworkService : INeuralNetworkService
    {
        public CalculationResult Calculate(NeuralWebDomain neuralWeb, double? answer = null)
        {
            var result = new CalculationResult();

            //Обсчитываем скрытый слой и слои вывода
            var neurons = neuralWeb.Neurons
                .Select(n => n.Value)
                .Where(n => n.NeuronType != (byte)NeuronTypeConst.InputNeuronType)
                .OrderBy(n => n.NeuronType);

            foreach (var neuron in neurons)
            {
                foreach (var synapse in neuralWeb.Synapses.Select(n => n.Value).Where(s => s.IdOutput == neuron.IdNeuron))
                {
                    neuron.DataIn += neuralWeb.Neurons[synapse.IdInput].DataOut * synapse.Weight;
                }
                neuron.DataOut = Sigmoid(neuron.DataIn);
            }

            if (answer.HasValue)
            {
                result.Error = CalculateDeviation(neuralWeb, answer.Value);
            }
            else
            {
                result.Error = 0;
            }

            result.Result = neuralWeb.Neurons.Single(n => n.Value.NeuronType == (byte)NeuronTypeConst.OutputNeuronType).Value.DataOut;
            return result;
        }

        /// <summary>
        /// Считаем дельта-отклонение, пересчитываем веса нейронов
        /// </summary>
        /// <param name="answer"></param>
        private double CalculateDeviation(NeuralWebDomain neuralWeb, double answer)
        {
            //Считаем значение средней квадратичной ошибки
            var outNeuron = neuralWeb.Neurons.Single(n => n.Value.NeuronType == (byte)NeuronTypeConst.OutputNeuronType).Value;
            neuralWeb.MSEcounter++;
            neuralWeb.ErrorMSE += Math.Pow((answer - outNeuron.DataOut), 2);

            //Считаем дельта-отклонение для слоя вывода
            outNeuron.DeltaDeviation = DeltaOutput(answer, outNeuron.DataOut);

            CalculateHiddenLayerDeltaDeviation(neuralWeb, (byte)NeuronTypeConst.SecondLayerHiddenNeuronType);

            CalculateHiddenLayerDeltaDeviation(neuralWeb, (byte)NeuronTypeConst.FirstLayerHiddenNeuronType);

            //Считаем изменение веса синапсов слоя ввода
            foreach (var neuron in neuralWeb.Neurons.Select(n => n.Value).Where(n => n.NeuronType == (byte)NeuronTypeConst.InputNeuronType))
            {
                //градиент для градиентного спуска
                double grad = 0;

                foreach (var synapse in neuralWeb.Synapses.Select(s => s.Value).Where(s => s.IdInput == neuron.IdNeuron))
                {
                    var targetDeviation = neuralWeb.Neurons[synapse.IdOutput].DeltaDeviation;
                    grad = targetDeviation * neuron.DataOut;
                    synapse.DeltaWeight = neuralWeb.LearningSpeed * grad + neuralWeb.Moment * synapse.DeltaWeight;
                    synapse.Weight += synapse.DeltaWeight;
                }
            }
            return neuralWeb.ErrorMSE / neuralWeb.MSEcounter;
        }

        /// <summary>
        /// Считаем дельта-отклонение для указанного слоя скрытых нейронов и изменяем веса синапсов
        /// </summary>
        /// <param name="hiddenLayerType"></param>
        private void CalculateHiddenLayerDeltaDeviation(NeuralWebDomain neuralWeb, byte hiddenLayerType)
        {
            foreach (var neuron in neuralWeb.Neurons.Select(n => n.Value).Where(n => n.NeuronType == hiddenLayerType))
            {
                //сумма произведения всех исходящих весов и дельта-отклонения нейрона, с которым связан синапс
                double sum = 0;
                //градиент для градиентного спуска
                double grad = 0;

                foreach (var synapse in neuralWeb.Synapses.Select(s => s.Value).Where(s => s.IdInput == neuron.IdNeuron))
                {
                    var targetDeviation = neuralWeb.Neurons[synapse.IdOutput].DeltaDeviation;
                    sum += targetDeviation * synapse.Weight;
                    grad = targetDeviation * neuron.DataOut;
                    synapse.DeltaWeight = neuralWeb.LearningSpeed * grad + neuralWeb.Moment * synapse.DeltaWeight;
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
    }
}
