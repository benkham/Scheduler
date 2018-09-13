using System;
using System.Linq.Expressions;
using VicTrains.Domain.Model.ScheduleAggregate;

namespace VicTrains.Application.Schedules.Models
{
    public class ScheduleDetailModel
    {
        public int Id { get; set; }
        public int TrainLineId { get; set; }
        public string TrainLine { get; set; }
        public string TrainLineDescription { get; set; }
        public string DepartureStation { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime ArrivalDateTime { get; set; }

        public static Expression<Func<Schedule, ScheduleDetailModel>> Projection
        {
            get
            {
                return schedule => new ScheduleDetailModel
                {
                    Id = schedule.Id,
                    TrainLineId = schedule.TrainLineId,
                    DepartureStation = schedule.DepartureStation,
                    DepartureDateTime = schedule.DepartureDateTime,
                    ArrivalStation = schedule.ArrivalStation,
                    ArrivalDateTime = schedule.ArrivalDateTime
                };
            }
        }

        public static ScheduleDetailModel Create(Schedule schedule)
        {
            return Projection.Compile().Invoke(schedule);
        }
    }
}
