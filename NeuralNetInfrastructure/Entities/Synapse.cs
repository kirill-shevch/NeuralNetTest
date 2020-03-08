namespace NeuralNetInfrastructure.Entities
{
    public class Synapse
    {
        public const string TableName = "Synapse";

        /// <summary>
        /// Id  синапса
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id входного нейрона
        /// </summary>
        public int NeuronIdInput { get; set; }

        public Neuron InputNeuron { get; set; }

        /// <summary>
        /// Id выходного нейрона
        /// </summary>
        public int NeuronIdOutput { get; set; }

        public Neuron OutputNeuron { get; set; }

        /// <summary>
        /// Id нейросети
        /// </summary>
        public int NeuralNetId { get; set; }

        public NeuralNet NeuralNet { get; set; }

        /// <summary>
        /// Вес синапса
        /// </summary>
        public double Weight { get; set; }

        public Synapse()
        {
        }
    }
}
