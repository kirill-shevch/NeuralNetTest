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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Weight);

            builder.HasOne(x => x.NeuralNet)
                .WithMany(x => x.Synapses)
                .HasForeignKey(x => x.NeuralNetId);

            builder.HasOne(x => x.InputNeuron)
                .WithMany(x => x.OutputSynapses)
                .HasForeignKey(x => x.NeuronIdInput);
            
            builder.HasOne(x => x.OutputNeuron)
                .WithMany(x => x.InputSynapses)
                .HasForeignKey(x => x.NeuronIdOutput);
        }
    }
}
