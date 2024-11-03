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
        public async Task MakePayment(Guid customerId, decimal amount)
        {
            await _paymentGateway.DebitCustomer(customerId, amount);
        }

        [Activity]
        public Task RefundPayment(Guid customerId, decimal amount)
        {
            return Task.CompletedTask;
        }
    }
}
