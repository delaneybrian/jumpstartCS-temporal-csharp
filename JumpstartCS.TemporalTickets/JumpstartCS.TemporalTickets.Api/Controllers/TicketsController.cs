using Microsoft.AspNetCore.Mvc;
using Temporalio.Client;

namespace JumpstartCS.TemporalTickets.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITemporalClient _temporalClient;

        public TicketsController(ITemporalClient temporalClient)
        {
            _temporalClient = temporalClient;s
        }

        [HttpPost()]
        public async Task<IActionResult> ReserveTickets(
            Guid customerId, 
            Guid eventId, 
            int numberOfTickets)
        {
            return Ok();
        }
    }
}
