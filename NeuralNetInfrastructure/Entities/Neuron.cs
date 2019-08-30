using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuralNetInfrastructure.Entities
{
    public class Neuron
    {
        /// <summary>
        /// Id нейрона
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Тип нейрона: входной, скрытый, выходной, NeuronTypeConst
        /// </summary>
        public byte NeuronType { get; set; }

        [ForeignKey(nameof(NeuralNet))]
        public int NeuralNetId { get; set; }
    }
}
