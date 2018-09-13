using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VicTrains.Application.Schedules.Commands;
using VicTrains.Application.Schedules.Models;
using VicTrains.Application.Schedules.Queries;

namespace VicTrains.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : Controller
    {
        private readonly IMediator _mediator;

        public SchedulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/schedules
        [HttpGet]
        public Task<List<ScheduleListModel>> Get()
        {
            return _mediator.Send(new GetScheduleListQuery());
        }

        // GET api/schedules/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetScheduleDetailQuery { Id = id };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        // POST api/schedules
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateScheduleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/schedules/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateScheduleCommand command)
        {
            if (command == null || command.Id != id)
            {
                return BadRequest();
            }

            return Ok(await _mediator.Send(command));
        }

        // DELETE api/schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteScheduleCommand { Id = id }));
        }
    }
}