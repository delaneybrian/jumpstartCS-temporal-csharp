using JumpstartCS.TemporalTickets.Activities;
using JumpstartCS.TemporalTickets.Definitions;
using JumpstartCS.TemporalTickets.Definitions.Exceptions;
using Temporalio.Common;
using Temporalio.Workflows;

namespace JumpstartCS.TemporalTickets.Workflows
{
    [Workflow]
    public class PurchaseTicketsWorkflow
    {
        private const decimal TicketCost = 25;

        [WorkflowRun]
        public async Task<ICollection<Ticket>> Run(Guid customerId, Guid eventId, int numberOfTickets)
        {
            var activityOptions = new ActivityOptions
            {
                StartToCloseTimeout = TimeSpan.FromSeconds(30),
                RetryPolicy = new RetryPolicy
                {
                    NonRetryableErrorTypes = new List<string> {
                        typeof(InsufficientFundsException).Name,
                        typeof(InsufficientTicketsAvaibableException).Name,
                        typeof(InvalidEventException).Name,
                        typeof(InvalidCustomerException).Name,
                    },
                    MaximumAttempts = 5
                }
            };

            var totalCost = TicketCost * numberOfTickets;   

            await Workflow.ExecuteActivityAsync(
                 (TicketPurchaseActivities ticketPurchaseActivities) =>
                 ticketPurchaseActivities.HoldTickets(customerId, eventId, numberOfTickets),
                 activityOptions);

            await Workflow.ExecuteActivityAsync(
                 (PaymentActivities paymentActivities) =>
                 paymentActivities.MakePayment(customerId, totalCost),
                 activityOptions);

            var tickets = await Workflow.ExecuteActivityAsync(
                 (TicketPurchaseActivities ticketPurchaseActivities) =>
                 ticketPurchaseActivities.ReserveTickets(customerId, eventId),
                 activityOptions);

            return tickets;
        }
    }
}
