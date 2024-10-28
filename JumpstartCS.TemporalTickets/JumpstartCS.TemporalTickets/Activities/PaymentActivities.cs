using JumpstartCS.TemporalTickets.Interfaces;
using Temporalio.Activities;

namespace JumpstartCS.TemporalTickets.Activities
{
    public class PaymentActivities
    {
        private readonly IPaymentGateway _paymentGateway;

        public PaymentActivities(
            IPaymentGateway paymentGateway)
        {
            _paymentGateway = paymentGateway;
        }

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
