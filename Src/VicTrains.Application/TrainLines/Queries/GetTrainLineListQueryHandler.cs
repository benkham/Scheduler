using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VicTrains.Application.TrainLines.Models;
using VicTrains.Persistence;

namespace VicTrains.Application.TrainLines.Queries
{
    public class GetTrainLineListQueryHandler : IRequestHandler<GetTrainLineListQuery, List<TrainLineListModel>>
    {
        private readonly VicTrainsDbContext _context;
        public GetTrainLineListQueryHandler(VicTrainsDbContext context)
        {
            _context = context;
        }

        public Task<List<TrainLineListModel>> Handle(GetTrainLineListQuery request, CancellationToken cancellationToken)
        {
            return _context.TrainLines.Select(t =>
                new TrainLineListModel
                {
                    Id = t.Id,
                    Line = t.Line,
                    Description = t.Description
                }).ToListAsync(cancellationToken);
        }
    }
}
