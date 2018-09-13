using System;

namespace VicTrains.Application.Schedules.Models
{
    public class ScheduleListModel
    {
        public int Id { get; set; }
        public int TrainLineId { get; set; }
        public string TrainLine { get; set; }
        public string TrainLineDescription { get; set; }
        public string DepartureStation { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime ArrivalDateTime { get; set; }
    }
}
