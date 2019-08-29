using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NeuralNetInfrastructure.Entities
{
    public class NeuralNet
    {
        [Key]
        public int Id { get; set; }

        public IList<Neuron> Neurons { get; set; }

        public IList<Synapse> Synapses { get; set; }

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
    }
}
