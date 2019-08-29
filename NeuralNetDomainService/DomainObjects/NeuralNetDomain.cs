using NeuralNetDomainService.Constants;
using System.Collections.Generic;
using System.Linq;

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

        public void SetInputData(IList<double> inputs)
        {
            //Важен правильный порядок входных нейронов и инпутов!
            var inputNeurons = Neurons.Where(n => n.NeuronType == NeuronType.InputNeuronType).ToList();
            for (int i = 0; i < inputs.Count; i++)
            {
                inputNeurons[i].DataOut = inputs[i];
            }
        }
    }
}
