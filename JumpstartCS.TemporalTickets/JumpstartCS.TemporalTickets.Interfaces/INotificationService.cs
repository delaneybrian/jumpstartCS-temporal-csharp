namespace JumpstartCS.TemporalTickets.Interfaces
{
    public interface INotificationService
    {
        public Task SendPushNotification(Guid userId, string message);
    }
}
