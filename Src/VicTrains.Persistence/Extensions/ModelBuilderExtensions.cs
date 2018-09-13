using Microsoft.EntityFrameworkCore;
using System;
using VicTrains.Domain.Model;
using VicTrains.Domain.Model.ScheduleAggregate;
using VicTrains.Persistence.Configurations;

namespace VicTrains.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TrainLineConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrainLine>()
                .HasData(new TrainLine(1, "Ballarat Line", "Wendouree to Melbourne via Ballarat"),
                    new TrainLine(2, "Bendigo Line", "Bendigo to Melbourne"),
                    new TrainLine(3, "Geelong Line", "Melbourne to Geelong"),
                    new TrainLine(4, "Traralgon Line", "Traralgon to Melbourne"),
                    new TrainLine(5, "Seymour Line", "Melbourne to Seymour"));

            modelBuilder.Entity<Schedule>()
                .HasData(new Schedule(1, 1, "WENDOUREE", DateTime.Parse("2018-09-03 05:15 AM"), "SOUTHERN CROSS STATION", DateTime.Parse("2018-09-03 06:35 AM")),
                    new Schedule(2, 2, "EPSOM STATION", DateTime.Parse("2018-09-05 05:58 AM"), "SOUTHERN CROSS STATION", DateTime.Parse("2018-09-05 08:06 AM")),
                    new Schedule(3, 3, "SOUTHERN CROSS STATION", DateTime.Parse("2018-09-07 06:00 AM"), "WAURN PONDS STATION", DateTime.Parse("2018-09-07 07:20 AM")),
                    new Schedule(4, 4, "TRARALGON STATION", DateTime.Parse("2018-09-10 04:35 AM"), "SOUTHERN CROSS STATION", DateTime.Parse("2018-09-10 06:58 AM")),
                    new Schedule(5, 5, "SOUTHERN CROSS STATION", DateTime.Parse("2018-09-12 06:12 AM"), "SEYMOUR", DateTime.Parse("2018-09-12 07:44 AM")));
        }
    }
}
