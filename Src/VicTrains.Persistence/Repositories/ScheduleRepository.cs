using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VicTrains.Domain.Common;
using VicTrains.Domain.Exceptions;
using VicTrains.Domain.Model.ScheduleAggregate;

namespace VicTrains.Persistence.Repositories
{
    // not used anymore because efcore already has implemented repository and uow patterns
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly VicTrainsDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ScheduleRepository(VicTrainsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Schedule AddSchedule(Schedule schedule)
        {
            if (schedule == null)
            {
                throw new ScheduleException(nameof(schedule));
            }

            return _context.Schedules.Add(schedule).Entity;
        }

        public void UpdateSchedule(Schedule schedule)
        {
            if (schedule == null)
            {
                throw new ScheduleException(nameof(schedule));
            }

            _context.Entry(schedule).State = EntityState.Modified;
        }

        public async Task<Schedule> GetScheduleAsync(int scheduleId)
        {
            if (scheduleId < 0)
            {
                throw new ScheduleException(nameof(scheduleId));
            }

            return await _context.Schedules
                .Include(i => i.TrainLine)
                .FirstOrDefaultAsync(i => i.Id == scheduleId);
        }

        public async Task<bool> DeleteScheduleAsync(int scheduleId)
        {
            var schedule = await _context.Schedules
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == scheduleId);

            if (schedule == null)
            {
                throw new ScheduleException(nameof(schedule));
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }
    }
}
