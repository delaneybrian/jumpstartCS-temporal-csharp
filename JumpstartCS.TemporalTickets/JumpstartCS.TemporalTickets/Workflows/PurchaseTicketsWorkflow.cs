using JumpstartCS.TemporalTickets.Activities;
using Temporalio.Workflows;

namespace JumpstartCS.TemporalTickets.Workflows
{
    [Workflow]
    public class PurchaseTicketsWorkflow
    {
        private const decimal TicketCost = 25;

        [WorkflowInit]
        public PurchaseTicketsWorkflow()
        {
        }

        [WorkflowRun]
        public async Task Run(Guid customerId, Guid eventId, int numberOfTickets)
        {
            var activityOptions = new ActivityOptions();

            await Workflow.ExecuteActivityAsync(
                   (TicketPurchaseActivities ticketPurchaseActivities) =>
                   ticketPurchaseActivities.HoldTickets(),
                   activityOptions);

            await Workflow.ExecuteActivityAsync(
                 (PaymentActivities paymentActivities) =>
                 paymentActivities.MakePayment(),
                 activityOptions);

            await Workflow.ExecuteActivityAsync(
                 (TicketPurchaseActivities ticketPurchaseActivities) =>
                 ticketPurchaseActivities.ReserveTickets(),
                 activityOptions);
        }
    }
}
