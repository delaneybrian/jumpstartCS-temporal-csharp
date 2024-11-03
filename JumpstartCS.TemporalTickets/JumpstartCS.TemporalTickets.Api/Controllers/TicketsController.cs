using JumpstartCS.TemporalTickets.Workflows;
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
            _temporalClient = temporalClient;
        }

        [HttpPost("{eventId}")]
        public async Task<IActionResult> ReserveTickets(
            Guid customerId, 
            Guid eventId, 
            int numberOfTickets)
        {
            var tickets = await _temporalClient.ExecuteWorkflowAsync(
                (PurchaseTicketsWorkflow workflow) => workflow.Run(customerId, eventId, numberOfTickets), new WorkflowOptions
                {
                    Id = Guid.NewGuid().ToString(),
                    TaskQueue = "ticket-purchase-task-queue"
                });

            return Ok(tickets);
        }
    }
}
