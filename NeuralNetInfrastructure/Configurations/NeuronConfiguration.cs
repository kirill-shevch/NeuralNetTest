using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeuralNetInfrastructure.Entities;
using System;

namespace NeuralNetInfrastructure.Configurations
{
    public class NeuronConfiguration : IEntityTypeConfiguration<Neuron>
    {
        public void Configure(EntityTypeBuilder<Neuron> builder)
        {
            builder.ToTable(Neuron.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NeuronType);
            builder.Property(x => x.NeuralNetId);

            builder.HasOne(x => x.NeuralNet)
                .WithMany(x => x.Neurons)
                .HasForeignKey(x => x.NeuralNetId);
        }
    }
}
