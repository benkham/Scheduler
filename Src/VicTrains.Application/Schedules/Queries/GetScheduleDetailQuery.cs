using MediatR;
using VicTrains.Application.Schedules.Models;

namespace VicTrains.Application.Schedules.Queries
{
    public class GetScheduleDetailQuery : IRequest<ScheduleDetailModel>
    {
        public int Id { get; set; }
    }
}
