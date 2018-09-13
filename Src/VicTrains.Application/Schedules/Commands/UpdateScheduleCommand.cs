using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using VicTrains.Application.Schedules.Models;

namespace VicTrains.Application.Schedules.Commands
{
    public class UpdateScheduleCommand : IRequest<ScheduleDetailModel>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int TrainLineId { get; set; }

        [Required]
        [MaxLength(40)]
        public string DepartureStation { get; set; }

        [Required]
        public DateTime DepartureDateTime { get; set; }

        [Required]
        [MaxLength(40)]
        public string ArrivalStation { get; set; }

        [Required]
        public DateTime ArrivalDateTime { get; set; }
    }
}
