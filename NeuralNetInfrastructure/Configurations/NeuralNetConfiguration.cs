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
            builder.ToTable(NeuralNet.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ErrorMSE);
            builder.Property(x => x.MSEcounter);
            builder.Property(x => x.LearningSpeed);
            builder.Property(x => x.Moment);
        }
    }
}