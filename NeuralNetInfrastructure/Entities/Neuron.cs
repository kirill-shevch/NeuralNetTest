namespace NeuralNetInfrastructure.Entities
{
    public class Neuron
    {
        public const string TableName = "Neuron";

        /// <summary>
        /// Id нейрона
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Тип нейрона: входной, скрытый, выходной, NeuronTypeConst
        /// </summary>
        public byte NeuronType { get; set; }

        public int NeuralNetId { get; set; }

        public NeuralNet NeuralNet { get; set; }

        public Neuron()
        {
        }
    }
}
