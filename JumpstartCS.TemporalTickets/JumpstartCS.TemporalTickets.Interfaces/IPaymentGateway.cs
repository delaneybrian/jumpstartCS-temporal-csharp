namespace JumpstartCS.TemporalTickets.Interfaces
{
    public interface IPaymentGateway
    {
        Task DebitUser(Guid userId, decimal amount);

        Task CreditUser(Guid userId, decimal amount);
    }
}
