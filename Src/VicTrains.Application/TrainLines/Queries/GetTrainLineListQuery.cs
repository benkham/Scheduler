using MediatR;
using System.Collections.Generic;
using VicTrains.Application.TrainLines.Models;

namespace VicTrains.Application.TrainLines.Queries
{
    public class GetTrainLineListQuery : IRequest<List<TrainLineListModel>>
    {
    }
}
