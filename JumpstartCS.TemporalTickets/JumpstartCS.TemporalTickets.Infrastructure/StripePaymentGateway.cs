using System.Collections.Concurrent;
using JumpstartCS.TemporalTickets.Definitions.Configuration;
using JumpstartCS.TemporalTickets.Definitions.Exceptions;
using JumpstartCS.TemporalTickets.Interfaces;
using Microsoft.Extensions.Options;

namespace JumpstartCS.TemporalTickets.Infrastructure
{
    public class StripePaymentGateway : IPaymentGateway
    {
        private readonly IOptions<StripePaymentGatewayOptions> _options;
        private readonly Random _random;

        private readonly ConcurrentDictionary<Guid, decimal> BalanceByCustomerId = new()
        {
            [Guid.Parse("b073f08f-a3af-4b32-85c6-f2c5121291d6")] = 100,
            [Guid.Parse("9266a6d5-3218-4dc1-b4ba-88e8eca2dc58")] = 100000,
            [Guid.Parse("7ec58b17-2648-46d8-94cd-a3e17cdfda74")] = 100,
            [Guid.Parse("195ba17a-48ea-47d8-affc-2feebc3aa772")] = 100,
            [Guid.Parse("ec9439e2-1eaa-4c81-a952-f14ee26f66d7")] = 100
        };

        public StripePaymentGateway(IOptions<StripePaymentGatewayOptions> options)
        {
            _random = new Random();
            _options = options;
        }

        public Task CreditCustomer(Guid customerId, decimal amount)
        {
            if (IsTransientFailure())
                throw new TransientPaymentException();

            if (!BalanceByCustomerId.ContainsKey(customerId))
                throw new InvalidCustomerException();

            var currentBalance = BalanceByCustomerId[customerId];

            BalanceByCustomerId[customerId] = currentBalance + amount;

            return Task.CompletedTask;
        }

        public Task DebitCustomer(Guid customerId, decimal amount)
        {
            if (IsTransientFailure())
                throw new TransientPaymentException();

            if (!BalanceByCustomerId.ContainsKey(customerId))
                throw new InvalidCustomerException();

            var currentBalance = BalanceByCustomerId[customerId];

            if (currentBalance < amount)
                throw new InsufficientFundsException();

            BalanceByCustomerId[customerId] = currentBalance - amount;

            return Task.CompletedTask;
        }

        private bool IsTransientFailure()
        {
            return _random.NextDouble() < _options.Value.FailureRatio;
        }
    }
}
