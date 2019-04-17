using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetTest
{
    public class NeuralNetwork
    {
        public double Calculate(double first, int firstId, double second, int secondId, double answer)
        {
            #region Forward pass

            //Чистим нейроны и синапсы
            ClearAll();

            //Заполняем входные нейроны
            Neurons.Single(n => n.IdNeuron == firstId).DataIn = first;
            Neurons.Single(n => n.IdNeuron == firstId).DataOut = first;
            Neurons.Single(n => n.IdNeuron == secondId).DataIn = second;
            Neurons.Single(n => n.IdNeuron == secondId).DataOut = second;

            //Обсчитываем скрытый слой и слой вывода
            foreach (var neuron in Neurons.Where(n => n.NeuronType != (byte)NeuronTypeConst.InputNeuronType))
            {
                foreach (var synapse in Synapses.Where(s => s.IdOutput == neuron.IdNeuron))
                {
                    neuron.DataIn += Neurons.Single(n => n.IdNeuron == synapse.IdInput).DataOut * synapse.Weight;
                }
                neuron.DataOut = Sigmoid(neuron.DataIn);
            }

            //Считаем значение средней квадратичной ошибки
            var outNeuron = Neurons.Single(n => n.NeuronType == (byte)NeuronTypeConst.OutputNeuronType);
            MSEcounter++;
            ErrorMSE += Math.Pow((answer - outNeuron.DataOut), 2);
            ErrorMSE = ErrorMSE / MSEcounter;

            #endregion Forward pass

            #region Delta deviation

            //Считаем дельта-отклонение для слоя вывода
            outNeuron.DeltaDeviation = DeltaOutput(answer, outNeuron.DataOut);

            //Считаем дельта-отклонение для скрытого слоя и изменяем веса синапсов
            foreach (var neuron in Neurons.Where(n => n.NeuronType == (byte)NeuronTypeConst.HiddenNeuronType))
            {
                //сумма произведения всех исходящих весов и дельта-отклонения нейрона, с которым связан синапс
                double sum = 0;
                //градиент для градиентного спуска
                double grad = 0;

                foreach (var synapse in Synapses.Where(s => s.IdInput == neuron.IdNeuron))
                {
                    var targetDeviation = Neurons.Single(n => n.IdNeuron == synapse.IdOutput).DeltaDeviation;
                    sum += targetDeviation * synapse.Weight;
                    grad = targetDeviation * neuron.DataOut;
                    synapse.DeltaWeight = E * grad + M * synapse.DeltaWeight;
                    synapse.Weight += synapse.DeltaWeight;
                }
                neuron.DeltaDeviation = SigmoidDiff(neuron.DataOut) * sum;
            }

            //Считаем изменение веса синапсов слоя ввода
            foreach (var neuron in Neurons.Where(n => n.NeuronType == (byte)NeuronTypeConst.InputNeuronType))
            {
                //градиент для градиентного спуска
                double grad = 0;

                foreach (var synapse in Synapses.Where(s => s.IdInput == neuron.IdNeuron))
                {
                    var targetDeviation = Neurons.Single(n => n.IdNeuron == synapse.IdOutput).DeltaDeviation;
                    grad = targetDeviation * neuron.DataOut;
                    synapse.DeltaWeight = E * grad + M * synapse.DeltaWeight;
                    synapse.Weight += synapse.DeltaWeight;
                }
            }

            #endregion Delta deviation

            return outNeuron.DataOut;
        }

        /// <summary>
        /// Производная функции активации для метода обратного распространения ошибки (МОР)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static double SigmoidDiff(double x)
        {
            return x * (1 - x);
        }

        /// <summary>
        /// Дельта отклонение для выходных нейронов
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="dataOut"></param>
        /// <returns></returns>
        static double DeltaOutput(double answer, double dataOut)
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

        private void ClearAll()
        {
            foreach (var neuron in Neurons)
            {
                neuron.DataIn = 0;
                neuron.DataOut = 0;
                neuron.DeltaDeviation = 0;
            }
        }

        public List<Neuron> Neurons { get; private set; }

        public List<Synapse> Synapses { get; private set; }

        public double E { get; private set; }

        public double M { get; private set; }

        public double ErrorMSE { get; private set; }

        public Int64 MSEcounter { get; private set; }

        private NeuralNetwork()
        {
        }

        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="e">Скорость обучения</param>
        /// <param name="m">Момент</param>
        /// <param name="answer">Правильный ответ</param>
        public NeuralNetwork(double e, double m)
        {
            Neurons = new List<Neuron>();
            Synapses = new List<Synapse>();
            E = e;
            M = m;
            ErrorMSE = 0;
            MSEcounter = 0;
        }

        public int SetNeuron(NeuronTypeConst neuronType)
        {
            int id = Neurons.Any() ? Neurons.Max(n => n.IdNeuron) : 0;
            id++;
            var neuron = new Neuron(id, (byte)neuronType);
            neuron.DataIn = 0;
            neuron.DataOut = 0;
            neuron.DeltaDeviation = 0;
            Neurons.Add(neuron);
            return id;
        }

        public int SetSynapse(int inputId, int outputId, double weight)
        {
            int id = Synapses.Any() ? Synapses.Max(s => s.IdSynapse) : 0;
            id++;
            var synapse = new Synapse(id);
            synapse.IdInput = inputId;
            synapse.IdOutput = outputId;
            synapse.Weight = weight;
            Synapses.Add(synapse);
            return id;
        }
    }

    public class Neuron
    {
        /// <summary>
        /// Id нейрона
        /// </summary>
        public int IdNeuron { get; private set; }

        /// <summary>
        /// Входное значение
        /// </summary>
        public double DataIn { get; set; }

        /// <summary>
        /// Значение на выход после применения функции активации
        /// </summary>
        public double DataOut { get; set; }

        /// <summary>
        /// Дельта отклонение
        /// </summary>
        public double DeltaDeviation { get; set; }

        /// <summary>
        /// Тип нейрона: входной, скрытый, выходной, NeuronTypeConst
        /// </summary>
        public byte NeuronType { get; private set; }

        public override bool Equals(object obj)
        {
            var neuron = obj as Neuron;
            return neuron != null &&
                   IdNeuron == neuron.IdNeuron &&
                   DataIn == neuron.DataIn &&
                   DataOut == neuron.DataOut &&
                   DeltaDeviation == neuron.DeltaDeviation &&
                   NeuronType == neuron.NeuronType;
        }

        public override int GetHashCode()
        {
            var hashCode = 1569351438;
            hashCode = hashCode * -1521134295 + IdNeuron.GetHashCode();
            hashCode = hashCode * -1521134295 + DataIn.GetHashCode();
            hashCode = hashCode * -1521134295 + DataOut.GetHashCode();
            hashCode = hashCode * -1521134295 + DeltaDeviation.GetHashCode();
            hashCode = hashCode * -1521134295 + NeuronType.GetHashCode();
            return hashCode;
        }

        private Neuron()
        {

        }

        public Neuron(int id, byte neuronType)
        {
            IdNeuron = id;
            NeuronType = neuronType;
        }
    }

    public class Synapse
    {
        /// <summary>
        /// Id  синапса
        /// </summary>
        public int IdSynapse { get; private set; }

        /// <summary>
        /// Id входного нейрона
        /// </summary>
        public int IdInput { get; set; }

        /// <summary>
        /// Id выходного нейрона
        /// </summary>
        public int IdOutput { get; set; }

        /// <summary>
        /// Вес синапса
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Изменение веса нейрона
        /// </summary>
        public double DeltaWeight { get; set; }

        public override bool Equals(object obj)
        {
            var synapse = obj as Synapse;
            return synapse != null &&
                   IdSynapse == synapse.IdSynapse &&
                   IdInput == synapse.IdInput &&
                   IdOutput == synapse.IdOutput &&
                   Weight == synapse.Weight &&
                   DeltaWeight == synapse.DeltaWeight;
        }

        public override int GetHashCode()
        {
            var hashCode = -393186522;
            hashCode = hashCode * -1521134295 + IdSynapse.GetHashCode();
            hashCode = hashCode * -1521134295 + IdInput.GetHashCode();
            hashCode = hashCode * -1521134295 + IdOutput.GetHashCode();
            hashCode = hashCode * -1521134295 + Weight.GetHashCode();
            hashCode = hashCode * -1521134295 + DeltaWeight.GetHashCode();
            return hashCode;
        }

        private Synapse()
        {

        }

        public Synapse(int id)
        {
            IdSynapse = id;
        }
    }

    public enum NeuronTypeConst
    {
        InputNeuronType,
        HiddenNeuronType,
        OutputNeuronType
    }
}
