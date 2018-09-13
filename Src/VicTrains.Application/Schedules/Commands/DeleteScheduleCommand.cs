using MediatR;

namespace VicTrains.Application.Schedules.Commands
{
    public class DeleteScheduleCommand : IRequest
    {
        public int Id { get; set; }
    }
}
