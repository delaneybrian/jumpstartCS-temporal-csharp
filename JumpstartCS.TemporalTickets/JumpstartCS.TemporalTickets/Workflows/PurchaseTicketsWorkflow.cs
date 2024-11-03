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

        private readonly ActivityOptions ActivityOptions = new ActivityOptions
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

        [WorkflowRun]
        public async Task<ICollection<Ticket>> Run(Guid customerId, Guid eventId, int numberOfTickets)
        {          
            var totalCost = TicketCost * numberOfTickets;

            var rollbackTasks = new List<Func<Task>>();

            try
            {
                await Workflow.ExecuteActivityAsync(
                 (TicketPurchaseActivities ticketPurchaseActivities) =>
                 ticketPurchaseActivities.HoldTickets(customerId, eventId, numberOfTickets),
                 ActivityOptions);

                var releaseTicketsCompensation = async () =>
                {
                    await Workflow.ExecuteActivityAsync(
                           (TicketPurchaseActivities accountActivities) => accountActivities.ReleaseTickets(customerId, eventId),
                           ActivityOptions);
                };

                rollbackTasks.Add(releaseTicketsCompensation);

                await Workflow.ExecuteActivityAsync(
                     (PaymentActivities paymentActivities) =>
                     paymentActivities.MakePayment(customerId, totalCost),
                     ActivityOptions);

                var refundCustomerCompensation = async () =>
                {
                    await Workflow.ExecuteActivityAsync(
                           (PaymentActivities paymentActivities) => paymentActivities.RefundPayment(customerId, totalCost),
                           ActivityOptions);
                };

                rollbackTasks.Add(refundCustomerCompensation);

                var tickets = await Workflow.ExecuteActivityAsync(
                     (TicketPurchaseActivities ticketPurchaseActivities) =>
                     ticketPurchaseActivities.ReserveTickets(customerId, eventId),
                     ActivityOptions);

                return tickets;
            }
            catch (Exception ex)
            {
                foreach(var t in rollbackTasks)
                {
                    await t();
                }

                throw;
            }
        }
    }
}
