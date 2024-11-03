namespace JumpstartCS.TemporalTickets.Interfaces
{
    public interface IPaymentGateway
    {
        Task DebitCustomer(Guid customerId, decimal amount);

        Task CreditCustomer(Guid customerId, decimal amount);
    }
}
