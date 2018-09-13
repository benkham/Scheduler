using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VicTrains.Application.Exceptions;
using VicTrains.Domain.Model.ScheduleAggregate;
using VicTrains.Persistence;

namespace VicTrains.Application.Schedules.Commands
{
    public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand>
    {
        private readonly VicTrainsDbContext _context;

        public DeleteScheduleCommandHandler(VicTrainsDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Schedules.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Schedule), request.Id);
            }

            _context.Schedules.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
