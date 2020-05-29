using System.Collections.Generic;

namespace NeuralNetApi
{
    public class NeuralNetDto
    {
        public int Id { get; set; }

        public List<NeuronDto> Neurons { get; set; }

        public List<SynapseDto> Synapses { get; set; }

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
