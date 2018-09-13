using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VicTrains.Domain.Model.ScheduleAggregate;

namespace VicTrains.Persistence.Configurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(e => e.Id);

            // sequence to autogenerate the key
            builder.Property(p => p.Id)
                .ForSqlServerUseSequenceHiLo("ScheduleSeq");

            // rename id to scheduleid in schema
            builder.Property(e => e.Id)
                .HasColumnName("ScheduleID");

            builder.Property(e => e.TrainLineId)
                .HasColumnName("TrainLineID");

            builder.Property(e => e.DepartureStation)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(e => e.DepartureDateTime)
                .HasColumnType("datetime");

            builder.Property(e => e.ArrivalStation)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(e => e.ArrivalDateTime)
                .HasColumnType("datetime");

            // foreignkey configuration
            builder.HasOne(d => d.TrainLine)
                .WithMany(s => s.Schedules)
                .HasForeignKey(d => d.TrainLineId)
                .HasConstraintName("FK_Schedules_TrainLines");
        }
    }
}
