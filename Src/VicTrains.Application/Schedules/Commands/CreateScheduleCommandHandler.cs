using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using VicTrains.Application.Schedules.Models;
using VicTrains.Domain.Model.ScheduleAggregate;
using VicTrains.Persistence;

namespace VicTrains.Application.Schedules.Commands
{
    public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, ScheduleDetailModel>
    {
        private readonly VicTrainsDbContext _context;
        private readonly ILogger<CreateScheduleCommandHandler> _logger;

        public CreateScheduleCommandHandler(VicTrainsDbContext context, ILogger<CreateScheduleCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ScheduleDetailModel> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            var entity = new Schedule(request.TrainLineId, request.DepartureStation,
                request.DepartureDateTime, request.ArrivalStation, request.ArrivalDateTime);

            _context.Schedules.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($":: A new schedule has been created with id {entity.Id} ::");

            return await Task.FromResult(ScheduleDetailModel.Create(entity));
        }
    }
}
