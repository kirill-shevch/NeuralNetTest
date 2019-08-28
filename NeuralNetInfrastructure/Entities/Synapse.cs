using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuralNetDomain.Entities
{
    public class Synapse
    {
        /// <summary>
        /// Id  синапса
        /// </summary>
        [Key]
        public int Id { get; private set; }

        /// <summary>
        /// Id входного нейрона
        /// </summary>
        [ForeignKey(nameof(Neuron))]
        public int NeuronIdInput { get; set; }

        /// <summary>
        /// Id выходного нейрона
        /// </summary>
        [ForeignKey(nameof(Neuron))]
        public int NeuronIdOutput { get; set; }

        /// <summary>
        /// Id нейросети
        /// </summary>
        [ForeignKey(nameof(NeuralWeb))]
        public int NeuralWebId { get; set; }

        /// <summary>
        /// Вес синапса
        /// </summary>
        public double Weight { get; set; }
    }
}
