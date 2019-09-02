using System.ComponentModel.DataAnnotations;

namespace NeuralNetInfrastructure.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Signature { get; set; }
    }
}
