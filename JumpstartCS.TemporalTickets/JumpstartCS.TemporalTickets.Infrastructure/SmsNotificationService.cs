using JumpstartCS.TemporalTickets.Definitions.Configuration;
using JumpstartCS.TemporalTickets.Interfaces;
using Microsoft.Extensions.Options;

namespace JumpstartCS.TemporalTickets.Infrastructure
{
    public class SmsNotificationService : INotificationService
    {
        private readonly IOptions<SmsNotificationServiceOptions> _options;
        private readonly Random _random;    

        public SmsNotificationService(IOptions<SmsNotificationServiceOptions> options)
        {
            _options = options;
            _random = new Random();
        }

        public async Task SendPushNotification(Guid userId, string message)
        {
            var delayMilliSeconds = _random.NextInt64(_options.Value.MinDelayMillis, _options.Value.MaxDelayMillis);

            await Task.Delay(TimeSpan.FromMilliseconds(delayMilliSeconds));
        }
    }
}
