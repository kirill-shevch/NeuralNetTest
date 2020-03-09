using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeuralNetInfrastructure.Entities;

namespace NeuralNetInfrastructure.Configurations
{
    public class PriceConfiguration : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.ToTable(Price.TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date);
            builder.Property(x => x.PriceValue);
            builder.Property(x => x.IsPredicted);

            builder.HasOne(x => x.Company)
                .WithMany(x => x.Prices)
                .HasForeignKey(x => x.CompanyId);
        }
    }
}