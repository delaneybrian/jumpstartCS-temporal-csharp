using Temporalio.Activities;

namespace JumpstartCS.TemporalTickets.Activities
{
    public class PaymentActivities
    {
        [Activity]
        public Task MakePayment()
        {
            return Task.CompletedTask;
        }

        [Activity]
        public Task RefundPayment()
        {
            return Task.CompletedTask;
        }
    }
}
