using System;
using VicTrains.Domain.Common;
using VicTrains.Domain.Exceptions;

namespace VicTrains.Domain.Model.ScheduleAggregate
{
    public class Schedule : Entity, IAggregateRoot
    {
        public int TrainLineId { get; private set; }
        public TrainLine TrainLine { get; private set; }
        public string DepartureStation { get; private set; }
        public DateTime DepartureDateTime { get; private set; }
        public string ArrivalStation { get; private set; }
        public DateTime ArrivalDateTime { get; private set; }

        public Schedule(int trainLineId, string departureStation,
            DateTime departureDateTime, string arrivalStation, DateTime arrivalDateTime)
        {
            TrainLineId = trainLineId > 0 ? trainLineId :
                throw new ScheduleException(nameof(trainLineId));

            DepartureStation = !string.IsNullOrWhiteSpace(departureStation) ? departureStation :
                throw new ScheduleException(nameof(departureStation));

            DepartureDateTime = departureDateTime;

            ArrivalStation = !string.IsNullOrWhiteSpace(arrivalStation) ? arrivalStation :
                throw new ScheduleException(nameof(arrivalStation));

            // arrival datetime should not be less than the departure datetime
            if (arrivalDateTime < departureDateTime)
            {
                throw new ScheduleException(nameof(arrivalDateTime));
            }
            ArrivalDateTime = arrivalDateTime;
        }

        // added schedule with id to seed database
        public Schedule(int id, int trainLineId, string departureStation,
            DateTime departureDateTime, string arrivalStation, DateTime arrivalDateTime)
        {
            Id = id > 0 ? id : throw new ScheduleException(nameof(id));

            TrainLineId = trainLineId > 0 ? trainLineId :
                throw new ScheduleException(nameof(trainLineId));

            DepartureStation = !string.IsNullOrWhiteSpace(departureStation) ? departureStation :
                throw new ScheduleException(nameof(departureStation));

            DepartureDateTime = departureDateTime;

            ArrivalStation = !string.IsNullOrWhiteSpace(arrivalStation) ? arrivalStation :
                throw new ScheduleException(nameof(arrivalStation));

            // arrival datetime should not be less than the departure datetime
            if (arrivalDateTime < departureDateTime)
            {
                throw new ScheduleException(nameof(arrivalDateTime));
            }
            ArrivalDateTime = arrivalDateTime;
        }
    }
}
