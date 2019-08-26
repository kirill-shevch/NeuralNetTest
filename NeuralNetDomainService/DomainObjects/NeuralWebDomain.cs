using System.Collections.Generic;

namespace NeuralNetDomainService.DomainObjects
{
    public class NeuralWebDomain
    {
        public int Id { get; private set; }

        public IDictionary<int, NeuronDomain> Neurons { get; set; }

        public IDictionary<int, SynapseDomain> Synapses { get; set; }

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
