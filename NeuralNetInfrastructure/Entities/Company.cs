namespace NeuralNetInfrastructure.Entities
{
    public class Company
    {
        public const string TableName = "Company";

        public int Id { get; set; }

        public string Name { get; set; }

        public string Signature { get; set; }
        
        public Company()
        {
        }
    }
}
