using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VicTrains.Application.Schedules.Models;
using VicTrains.Persistence;

namespace VicTrains.Application.Schedules.Queries
{
    public class GetScheduleListQueryHandler : IRequestHandler<GetScheduleListQuery, List<ScheduleListModel>>
    {
        private readonly VicTrainsDbContext _context;

        public GetScheduleListQueryHandler(VicTrainsDbContext context)
        {
            _context = context;
        }

        public Task<List<ScheduleListModel>> Handle(GetScheduleListQuery request, CancellationToken cancellationToken)
        {
            return _context.Schedules.Select(s =>
                new ScheduleListModel
                {
                    Id = s.Id,
                    TrainLineId = s.TrainLineId,
                    TrainLine = s.TrainLine.Line,
                    TrainLineDescription = s.TrainLine.Description,
                    DepartureStation = s.DepartureStation,
                    DepartureDateTime = s.DepartureDateTime,
                    ArrivalStation = s.ArrivalStation,
                    ArrivalDateTime = s.ArrivalDateTime

                }).ToListAsync(cancellationToken);
        }
    }
}
