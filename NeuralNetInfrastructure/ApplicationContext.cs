using Microsoft.EntityFrameworkCore;
using NeuralNetInfrastructure.Entities;

namespace NeuralNetInfrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<NeuralNet> NeuralNets { get; set; }
        public DbSet<Neuron> Neurons { get; set; }
        public DbSet<Synapse> Synapses { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-6U034AU;Database=NeuralNetTest;User Id=sa;Password=testpass");
        }
    }
}
