namespace NeuralNetDomain.Entities
{
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
}
