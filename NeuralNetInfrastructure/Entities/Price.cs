using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuralNetInfrastructure.Entities
{
    public class Price
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public DateTime Date { get; set; }

        public double PriceValue { get; set; }
        
        public bool IsPredicted { get; set; }
    }
}
