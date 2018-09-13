using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Threading;
using System.Threading.Tasks;
using VicTrains.Domain.Common;
using VicTrains.Domain.Model;
using VicTrains.Domain.Model.ScheduleAggregate;
using VicTrains.Persistence.Extensions;

namespace VicTrains.Persistence
{
    public class VicTrainsDbContext : DbContext, IUnitOfWork
    {
        public VicTrainsDbContext(DbContextOptions<VicTrainsDbContext> options)
            : base(options)
        {
        }

        public DbSet<TrainLine> TrainLines { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            var result = await SaveChangesAsync();

            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // apply configurations using ModelBuilder extension method
            modelBuilder.ApplyConfigurations();

            // add seed data using ModelBuilder extension method
            modelBuilder.Seed();
        }

        public class VicTrainsContextFactory : IDesignTimeDbContextFactory<VicTrainsDbContext>
        {
            // enable design-time migrations to this context
            public VicTrainsDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<VicTrainsDbContext>()
                    .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=VicTrainsDb;Trusted_Connection=True;")
                    .EnableSensitiveDataLogging();

                return new VicTrainsDbContext(optionsBuilder.Options);
            }
        }
    }
}
