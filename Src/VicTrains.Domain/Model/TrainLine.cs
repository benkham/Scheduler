using System.Collections.Generic;
using VicTrains.Domain.Common;
using VicTrains.Domain.Exceptions;
using VicTrains.Domain.Model.ScheduleAggregate;

namespace VicTrains.Domain.Model
{
    public class TrainLine : Entity
    {
        public TrainLine()
        {
            Schedules = new HashSet<Schedule>();
        }

        public string Line { get; private set; }
        public string Description { get; private set; }
        public ICollection<Schedule> Schedules { get; private set; }

        public TrainLine(int id, string line, string description)
        {
            Id = id > 0 ? id : throw new ScheduleException(nameof(id));

            Line = !string.IsNullOrWhiteSpace(line) ? line : throw new ScheduleException(nameof(line));

            Description = description;
        }
    }
}
