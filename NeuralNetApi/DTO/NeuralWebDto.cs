using System.Collections.Generic;

namespace NeuralNetApi.DTO
{
    public class NeuralWebDto
    {
        public int Id { get; set; }

        public IEnumerable<NeuronDto> Neurons { get; set; }

        public IEnumerable<SynapseDto> Synapses { get; set; }

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
