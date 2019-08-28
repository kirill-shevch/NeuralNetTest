using System.Collections.Generic;

namespace NeuralNetDomainService.DomainObjects
{
    public class NeuralNetworkDomain
    {
        public int Id { get; set; }

        public IList<NeuronDomain> Neurons { get; set; }

        public IList<SynapseDomain> Synapses { get; set; }

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

        //public void SetNeuronDataOut(int id, double dataOut)
        //{
        //    if (Neurons.ContainsKey(id))
        //    {
        //        Neurons[id].DataOut = dataOut;
        //    }
        //}
    }
}
