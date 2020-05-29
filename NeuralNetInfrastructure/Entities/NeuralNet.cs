using System.Collections.Generic;

namespace NeuralNetInfrastructure.Entities
{
    public class NeuralNet
    {
        public const string TableName = "NeuralNet";

        public int Id { get; set; }

        public double ErrorMSE { get; set; }

        public long MSEcounter { get; set; }

        /// <summary>
        /// Скорость обучения
        /// </summary>
        public double LearningSpeed { get; set; }

        /// <summary>
        /// Значение момента для градиентного спуска
        /// </summary>
        public double Moment { get; set; }

        public List<Neuron> Neurons { get; set; }

        public List<Synapse> Synapses { get; set; }

        public NeuralNet()
        {
            Neurons = new List<Neuron>();
            Synapses = new List<Synapse>();
        }
    }
}
