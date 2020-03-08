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
            throw new NotImplementedException();
        }
    }
}
