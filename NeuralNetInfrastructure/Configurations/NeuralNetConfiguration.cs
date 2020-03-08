using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeuralNetInfrastructure.Entities;
using System;

namespace NeuralNetInfrastructure.Configurations
{
    public class NeuralNetConfiguration : IEntityTypeConfiguration<NeuralNet>
    {
        public void Configure(EntityTypeBuilder<NeuralNet> builder)
        {
            throw new NotImplementedException();
        }
    }
}