using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeuralNetInfrastructure.Entities;

namespace NeuralNetInfrastructure.Configurations
{
    public class SynapseConfiguration : IEntityTypeConfiguration<Synapse>
    {
        public void Configure(EntityTypeBuilder<Synapse> builder)
        {
            builder.ToTable(Synapse.TableName);
        }
    }
}
