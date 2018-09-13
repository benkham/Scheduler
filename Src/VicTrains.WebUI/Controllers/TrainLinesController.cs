using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VicTrains.Application.TrainLines.Models;
using VicTrains.Application.TrainLines.Queries;

namespace VicTrains.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainLinesController : Controller
    {
        private readonly IMediator _mediator;

        public TrainLinesController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET api/trainlines
        [HttpGet]
        public Task<List<TrainLineListModel>> Get()
        {
            return _mediator.Send(new GetTrainLineListQuery());
        }
    }
}