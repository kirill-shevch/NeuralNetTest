using Microsoft.EntityFrameworkCore;
using NeuralNetInfrastructure.Configurations;
using NeuralNetInfrastructure.Entities;

namespace NeuralNetInfrastructure
{
    public class ApplicationContext : DbContext
    {
		private readonly string _connectionString;

		public ApplicationContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		public DbSet<Price> Prices { get; set; }      
        public DbSet<Company> Companies { get; set; }
        public DbSet<NeuralNet> NeuralNets { get; set; }
        public DbSet<Neuron> Neurons { get; set; }
        public DbSet<Synapse> Synapses { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			base.OnConfiguring(builder);

			builder
				.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
				.UseMySql(_connectionString);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfiguration(new PriceConfiguration());
			builder.ApplyConfiguration(new CompanyConfiguration());
			builder.ApplyConfiguration(new NeuralNetConfiguration());
			builder.ApplyConfiguration(new NeuronConfiguration());
			builder.ApplyConfiguration(new SynapseConfiguration());
		}
	}
}
