using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VicTrains.Domain.Model;

namespace VicTrains.Persistence.Configurations
{
    public class TrainLineConfiguration : IEntityTypeConfiguration<TrainLine>
    {
        public void Configure(EntityTypeBuilder<TrainLine> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("TrainLineID");

            builder.Property(e => e.Line)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(e => e.Description)
                .HasMaxLength(100);
        }
    }
}
