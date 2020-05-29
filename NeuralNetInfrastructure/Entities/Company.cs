using System.Collections.Generic;

namespace NeuralNetInfrastructure.Entities
{
    public class Company
    {
        public const string TableName = "Company";

        public int Id { get; set; }

        public string Name { get; set; }

        public string Signature { get; set; }
        
        public List<Price> Prices { get; set; }

        public Company()
        {
            Prices = new List<Price>();
        }
    }
}
