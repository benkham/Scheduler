using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VicTrains.Application.Exceptions;
using VicTrains.Application.Schedules.Models;
using VicTrains.Domain.Model.ScheduleAggregate;
using VicTrains.Persistence;

namespace VicTrains.Application.Schedules.Commands
{
    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, ScheduleDetailModel>
    {
        private readonly VicTrainsDbContext _context;
        private readonly ILogger<UpdateScheduleCommandHandler> _logger;

        public UpdateScheduleCommandHandler(VicTrainsDbContext context, ILogger<UpdateScheduleCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ScheduleDetailModel> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Schedules
                .Include(s => s.TrainLine)
                .AsNoTracking()
                .Where(s => s.Id == request.Id)
                .FirstOrDefault();

            if (entity == null)
            {
                throw new NotFoundException(nameof(Schedule), request.Id);
            }

            entity = new Schedule(request.Id, request.TrainLineId, request.DepartureStation,
                request.DepartureDateTime, request.ArrivalStation, request.ArrivalDateTime);

            _context.Schedules.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($":: A schedule with id {request.Id} has been updated ::");

            return await Task.FromResult(ScheduleDetailModel.Create(entity));
        }
    }
}
