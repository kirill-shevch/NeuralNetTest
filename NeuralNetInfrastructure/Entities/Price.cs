using System;

namespace NeuralNetInfrastructure.Entities
{
    public class Price
    {
        public const string TableName = "Price";

        public int Id { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public DateTime Date { get; set; }

        public double PriceValue { get; set; }
        
        public bool IsPredicted { get; set; }

        public Price()
        {
        }
    }
}
