using MediatR;
using System.Collections.Generic;
using VicTrains.Application.Schedules.Models;

namespace VicTrains.Application.Schedules.Queries
{
    public class GetScheduleListQuery : IRequest<List<ScheduleListModel>>
    {
    }
}
