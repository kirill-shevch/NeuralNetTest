using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuralNetDomain.Entities
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

        [ForeignKey(nameof(NeuralWeb))]
        public int NeuralWebId { get; set; }
    }
}
