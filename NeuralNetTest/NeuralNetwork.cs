using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeuralNetTest
{
    public class NeuralNetwork
    {
        public double Calculate(double first, int firstId, double second, int secondId, double? answer = null, bool calculateDeviation = false)
        {
            //Чистим нейроны и синапсы
            ClearAll();

            //Заполняем входные нейроны
            Neurons[firstId].DataIn = first;
            Neurons[firstId].DataOut = first;
            Neurons[secondId].DataIn = second;
            Neurons[secondId].DataOut = second;

            //Обсчитываем скрытый слой и слой вывода
            foreach (var neuron in Neurons.Select(n => n.Value).Where(n => n.NeuronType != (byte)NeuronTypeConst.InputNeuronType))
            {
                foreach (var synapse in Synapses.Select(n => n.Value).Where(s => s.IdOutput == neuron.IdNeuron))
                {
                    neuron.DataIn += Neurons[synapse.IdInput].DataOut * synapse.Weight;
                }
                neuron.DataOut = Sigmoid(neuron.DataIn);
            }

            if (calculateDeviation)
            {
                CalculateDeviation(answer.Value);
            }

            return Neurons.Single(n => n.Value.NeuronType == (byte)NeuronTypeConst.OutputNeuronType).Value.DataOut;
        }

        /// <summary>
        /// Считаем дельта-отклонение, пересчитываем веса нейронов
        /// </summary>
        /// <param name="answer"></param>
        private void CalculateDeviation(double answer)
        {
            //Считаем значение средней квадратичной ошибки
            var outNeuron = Neurons.Single(n => n.Value.NeuronType == (byte)NeuronTypeConst.OutputNeuronType).Value;
            MSEcounter++;
            ErrorMSE += Math.Pow((answer - outNeuron.DataOut), 2);
            ErrorMSE = ErrorMSE / MSEcounter;

            //Считаем дельта-отклонение для слоя вывода
            outNeuron.DeltaDeviation = DeltaOutput(answer, outNeuron.DataOut);

            CalculateHiddenLayerDeltaDeviation((byte)NeuronTypeConst.SecondLayerHiddenNeuronType);

            CalculateHiddenLayerDeltaDeviation((byte)NeuronTypeConst.FirstLayerHiddenNeuronType);

            //Считаем изменение веса синапсов слоя ввода
            foreach (var neuron in Neurons.Select(n => n.Value).Where(n => n.NeuronType == (byte)NeuronTypeConst.InputNeuronType))
            {
                //градиент для градиентного спуска
                double grad = 0;

                foreach (var synapse in Synapses.Select(s => s.Value).Where(s => s.IdInput == neuron.IdNeuron))
                {
                    var targetDeviation = Neurons[synapse.IdOutput].DeltaDeviation;
                    grad = targetDeviation * neuron.DataOut;
                    synapse.DeltaWeight = E * grad + M * synapse.DeltaWeight;
                    synapse.Weight += synapse.DeltaWeight;
                }
            }
        }

        /// <summary>
        /// Считаем дельта-отклонение для указанного слоя скрытых нейронов и изменяем веса синапсов
        /// </summary>
        /// <param name="hiddenLayerType"></param>
        private void CalculateHiddenLayerDeltaDeviation(byte hiddenLayerType)
        {
            foreach (var neuron in Neurons.Select(n => n.Value).Where(n => n.NeuronType == hiddenLayerType))
            {
                //сумма произведения всех исходящих весов и дельта-отклонения нейрона, с которым связан синапс
                double sum = 0;
                //градиент для градиентного спуска
                double grad = 0;

                foreach (var synapse in Synapses.Select(s => s.Value).Where(s => s.IdInput == neuron.IdNeuron))
                {
                    var targetDeviation = Neurons[synapse.IdOutput].DeltaDeviation;
                    sum += targetDeviation * synapse.Weight;
                    grad = targetDeviation * neuron.DataOut;
                    synapse.DeltaWeight = E * grad + M * synapse.DeltaWeight;
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
            foreach (var neuron in Neurons.Select(n => n.Value))
            {
                neuron.DataIn = 0;
                neuron.DataOut = 0;
                neuron.DeltaDeviation = 0;
            }
        }

        public Dictionary<int, Neuron> Neurons { get; private set; }

        public Dictionary<int, Synapse> Synapses { get; private set; }

        public double E { get; private set; }

        public double M { get; private set; }

        public double ErrorMSE { get; private set; }

        public Int64 MSEcounter { get; private set; }

        public NeuralNetwork()
        {
        }

        /// <summary>
        /// Конструктор для предзагрузки нейросети
        /// </summary>
        /// <param name="directory">Путь к файлу с параметрами сети</param>
        public NeuralNetwork(string directory)
        {
            LoadFromFile(directory);
        }

        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="e">Скорость обучения</param>
        /// <param name="m">Момент</param>
        /// <param name="answer">Правильный ответ</param>
        public NeuralNetwork(double e, double m)
        {
            Neurons = new Dictionary<int, Neuron>();
            Synapses = new Dictionary<int, Synapse>();
            E = e;
            M = m;
            ErrorMSE = 0;
            MSEcounter = 0;
        }

        public int SetNeuron(NeuronTypeConst neuronType)
        {
            int id = Neurons.Any() ? Neurons.Max(n => n.Value.IdNeuron) : 0;
            id++;
            var neuron = new Neuron(id, (byte)neuronType);
            Neurons.Add(id, neuron);
            return id;
        }

        private void SetNeuron(int neuronId, byte neuronType)
        {
            var neuron = new Neuron(neuronId, neuronType);
            Neurons.Add(neuronId, neuron);
        }

        public int SetSynapse(int inputId, int outputId, double weight)
        {
            int id = Synapses.Any() ? Synapses.Max(s => s.Value.IdSynapse) : 0;
            id++;
            var synapse = new Synapse(id);
            synapse.IdInput = inputId;
            synapse.IdOutput = outputId;
            synapse.Weight = weight;
            Synapses.Add(id, synapse);
            return id;
        }

        private void SetSynapse(int synapseId, int inputId, int outputId, double weight)
        {
            var synapse = new Synapse(synapseId);
            synapse.IdInput = inputId;
            synapse.IdOutput = outputId;
            synapse.Weight = weight;
            Synapses.Add(synapseId, synapse);
        }

        public void SaveToFile(string directory)
        {
            FileInfo fi = new FileInfo(directory);
            using (StreamWriter sr = new StreamWriter(fi.Open(FileMode.Truncate)))
            {
                sr.WriteLine("e;{0}", E);
                sr.WriteLine("m;{0}", M);
                foreach (var neuron in Neurons)
                {
                    sr.WriteLine("n;{0};{1}", neuron.Value.IdNeuron, neuron.Value.NeuronType);
                }
                foreach (var synapse in Synapses)
                {
                    sr.WriteLine("s;{0};{1};{2};{3};", synapse.Value.IdSynapse, synapse.Value.IdInput, synapse.Value.IdOutput, synapse.Value.Weight);
                }
            }
        }

        private void LoadFromFile(string directory)
        {
            ErrorMSE = 0;
            MSEcounter = 0;
            Neurons = new Dictionary<int, Neuron>();
            Synapses = new Dictionary<int, Synapse>();
            using (StreamReader sr = new StreamReader(directory))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(';');
                    if (parts.Length > 1)
                    {
                        switch (parts.First())
                        {
                            case "e": E = Convert.ToDouble(parts[1]); break;
                            case "m": M = Convert.ToDouble(parts[1]); break;
                            case "n":
                                {
                                    SetNeuron(Convert.ToInt32(parts[1]), Convert.ToByte(parts[2]));
                                    break;
                                }
                            case "s":
                                {
                                    SetSynapse(Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]), Convert.ToInt32(parts[3]), Convert.ToDouble(parts[4]));
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                }
            }
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

        public override string ToString()
        {
            return IdNeuron.ToString();
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

        public override string ToString()
        {
            return Weight.ToString();
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
        FirstLayerHiddenNeuronType,
        SecondLayerHiddenNeuronType,
        OutputNeuronType
    }
}