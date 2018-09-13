using System.Threading.Tasks;
using VicTrains.Domain.Common;

namespace VicTrains.Domain.Model.ScheduleAggregate
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        Schedule AddSchedule(Schedule schedule);

        void UpdateSchedule(Schedule schedule);

        Task<Schedule> GetScheduleAsync(int scheduleId);

        Task<bool> DeleteScheduleAsync(int scheduleId);
    }
}
