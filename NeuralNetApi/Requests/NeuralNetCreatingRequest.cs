using System.Collections.Generic;

namespace NeuralNetApi
{
    public class NeuralNetCreatingRequest
    {
        /// <summary>
        /// Скорость обучения
        /// </summary>
        public double LearningSpeed { get; set; }

        /// <summary>
        /// Значение момента для градиентного спуска
        /// </summary>
        public double Moment { get; set; }

        IList<NeuronDto> Neurons { get; set; }

        IList<SynapseDto> Synapses { get; set; }
    }
}