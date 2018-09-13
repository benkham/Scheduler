using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VicTrains.Application.Exceptions;
using VicTrains.Application.Schedules.Models;
using VicTrains.Domain.Model.ScheduleAggregate;
using VicTrains.Persistence;

namespace VicTrains.Application.Schedules.Queries
{
    public class GetScheduleDetailQueryHandler : IRequestHandler<GetScheduleDetailQuery, ScheduleDetailModel>
    {
        private readonly VicTrainsDbContext _context;

        public GetScheduleDetailQueryHandler(VicTrainsDbContext context)
        {
            _context = context;
        }

        public async Task<ScheduleDetailModel> Handle(GetScheduleDetailQuery request, CancellationToken cancellationToken)
        {
            var entity =  await _context.Schedules
                .Include(i => i.TrainLine)
                .FirstOrDefaultAsync(i => i.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Schedule), request.Id);
            }

            return new ScheduleDetailModel
            {
                Id = entity.Id,
                TrainLineId = entity.TrainLineId,
                TrainLine = entity.TrainLine.Line,
                TrainLineDescription = entity.TrainLine.Description,
                DepartureStation = entity.DepartureStation,
                DepartureDateTime = entity.DepartureDateTime,
                ArrivalStation = entity.ArrivalStation,
                ArrivalDateTime = entity.ArrivalDateTime
            };
        }
    }
}
